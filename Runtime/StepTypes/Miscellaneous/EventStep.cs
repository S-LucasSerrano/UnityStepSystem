using UnityEngine;
using UnityEngine.Events;

namespace LucasSerrano.StepSystem.Types
{
	/// <summary> Step that fires events at the Activate,End and Restart functions. </summary>
	
	[AddComponentMenu("Step System/Miscellaneous/Event Step")]
	public class EventStep : Step
	{
		[Space]
		public bool endImmediately = true;

		[Space]
		public UnityEvent onActivate = new UnityEvent();
		public UnityEvent onEnd = new UnityEvent();
		public UnityEvent onRestart = new UnityEvent();


		// ---------------------------------------------

		protected override void OnActivate()
		{
			onActivate.Invoke();
			if (endImmediately)
				End();
		}

		protected override void OnEnd()
		{
			onEnd.Invoke();
		}

		protected override void OnRestart()
		{
			onRestart.Invoke();
		}
	}
}
