using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LucasSerrano.StepSystem.Types
{
	/// <summary> Step that waits for the player to press a key. </summary>
	
	[AddComponentMenu("Wait/Wait For Input Key Step")]
	public class WaitForInputKeyStep : Step
	{
		[Space] public List<KeyCode> keys = new List<KeyCode>() { KeyCode.Space };


		// ------------------------------------------------------

		protected override void OnActivate()
		{
			StopWaitingRoutine();
			routine = StartCoroutine(WaitingRoutine());
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

		Coroutine routine = null;
		IEnumerator WaitingRoutine()
		{
			while (true)
			{
				foreach (KeyCode key in keys)
				{
					if (Input.GetKeyDown(key))
					{                           
						yield return new WaitForEndOfFrame();
						goto End;
					}
				}
				yield return null;
			}

			End: End();
		}

		void StopWaitingRoutine()
		{
			if (routine != null) StopCoroutine(routine);
		}
	}
}
