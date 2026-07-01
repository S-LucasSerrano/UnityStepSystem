using UnityEngine;

namespace LucasSerrano.StepSystem.Types
{
	/// <summary> A simple Step that just prints a message in the console. </summary>
	
	[AddComponentMenu("Step System/Miscellaneous/Debug Step")]
	public class DebugStep : Step
	{
		[Space][TextArea] public string logMessage = "Hello World!";

		protected override void OnActivate()
		{
			Debug.Log("[" + gameObject.name + "] " + logMessage, this);
			End();
		}
	}
}
