using System.Collections;
using UnityEngine;

namespace LucasSerrano.StepSystem.Types
{
	/// <summary> Step that waits a few seconds before moving to the next one. </summary>
	
	[AddComponentMenu("Step System/Wait/Wait Step")]
	public class WaitStep : Step
	{
		[Space][Min(0)] public float time = 1;
		Coroutine routine = null;


		// ------------------------------------------------------

		protected override void OnActivate()
		{
			StopWaitingRoutine();
			routine = StartCoroutine(WaitingRouting());
		}

		protected override void OnEnd()
		{
			StopWaitingRoutine();
		}

		protected override void OnRestart()
		{
			StopWaitingRoutine();
		}


		// ------------------------------------------------------

		IEnumerator WaitingRouting()
		{
			yield return new WaitForSeconds(time);
			End();
		}

		void StopWaitingRoutine()
		{
			if (routine != null) StopCoroutine(routine);
		}
	}
}
