using UnityEngine;

namespace LucasSerrano.StepSystem.Types
{
	/// <summary> Step that changes the position of an object. </summary>
	
	[AddComponentMenu("Step System/Miscellaneous/Teleport Step")]
	public class TeleportStep : Step
	{
		[Space]
		public Transform targetObject = null;
		public Transform newPosition = null;

		Vector3 prevPos = Vector3.positiveInfinity;


		// ---------------------------------------------

		protected override void OnActivate()
		{
			prevPos = targetObject.position;
			targetObject.position = newPosition.position;

			End();
		}

		protected override void OnRestart()
		{
			if (prevPos != Vector3.positiveInfinity)
			{
				targetObject.position = prevPos;
			}
		}
	}
}
