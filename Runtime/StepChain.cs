using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LucasSerrano.StepSystem
{
	/// <summary>
	/// A sequence of Steps that are executed in order. </summary>

	[AddComponentMenu("Step System/Step Chain")]
	public class StepChain : MonoBehaviour
	{
		/// <summary> List of Steps as they appear on the hierarchy. </summary>
		private List<Step> stepList = new List<Step>();
		/// <summary> Index of the Step from the list that is currently active. </summary>
		public int currentStepIndex { get; private set; } = -1;
		/// <summary> Step of the list that is currently active. </summary>
		public Step currentStep
		{
			get
			{
				if (currentStepIndex < 0 || currentStepIndex >= stepList.Count)
					return null;
				return stepList[currentStepIndex];
			}
		}

		[Space][SerializeField] private bool playOnStart = true;

		/// <summary> Is this chain currently been executed? </summary>
		public bool playing { get; private set; }


		// ------------------------------------------------------

		private void Awake()
		{
			Step[] stepComponents = GetComponentsInChildren<Step>(true);
			foreach (var step in stepComponents)
			{
				stepList.Add(step);
				step.SetChain(this);
			}
		}

		private IEnumerator Start()
		{
			// Wait one frame to make sure every Step has executed its Start.
			if (playOnStart)
			{
				yield return new WaitForEndOfFrame();
				Play();
			}
		}

		private void OnDisable()
		{
			Restart();
		}


		// ------------------------------------------------------

		/// <summary> Notify this chain that one of its Steps has ended. </summary>
		public void StepEnded(Step step)
		{
			if (playing == false)
				return;
			if (step != currentStep)
				return;

			currentStepIndex++;
			if (currentStepIndex >= stepList.Count)
			{
				playing = false;
				return;
			}
			currentStep.Activate();
		}

		/// <summary> Execute of the Steps of this chain from the first one. </summary>
		public void Play()
		{
			if (stepList.Count == 0)
				return;

			if (currentStepIndex >= 0)
				Restart();

			playing = true;
			currentStepIndex = 0;
			stepList[currentStepIndex].Activate();
		}

		/// <summary> Returns the StepChain to its initial state, stopping it if it was playing. </summary>
		public void Restart()
		{
			int i = currentStepIndex;        // Reset back to front from the current Step.
			if (i >= stepList.Count) i = stepList.Count - 1;

			while (i >= 0)
			{
				stepList[i].Restart();
				i--;
			}

			playing = false;
			currentStepIndex = -1;
		}
	}
}
