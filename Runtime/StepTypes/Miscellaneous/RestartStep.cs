using UnityEngine;

namespace LucasSerrano.StepSystem.Types
{
	/// <summary> Step that restarts the chain from the beginning. </summary>

	[AddComponentMenu("Step System/Miscellaneous/Restart Step")]
	public class RestartStep : Step
	{
		protected override void OnActivate()
		{
			End();
			chain.Play();
		}
	}
}
