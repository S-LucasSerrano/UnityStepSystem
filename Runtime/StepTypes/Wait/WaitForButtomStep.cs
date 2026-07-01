using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LucasSerrano.StepSystem.Types
{
	/// <summary> Step that waits for the player to press a UI button.</summary>

	[AddComponentMenu("Step System/Wait/Wait For Buttom Step")]
	public class WaitForButtomStep : Step
	{
		[Space] public List<Button> buttons = new List<Button>() { null };


		// ------------------------------------------------------

		protected override void OnActivate()
		{
			SetAllButtons(true);
		}

		protected override void OnEnd()
		{
			SetAllButtons(false);
		}

		protected override void OnRestart()
		{
			SetAllButtons(false);
		}


		// ------------------------------------------------------

		void SetAllButtons(bool value)
		{
			if (value)
			{
				foreach (Button but in buttons)
					but.onClick.AddListener(OnButtonClick);
			}
			else
			{
				foreach (Button but in buttons)
					but.onClick.RemoveListener(OnButtonClick);
			}
		}

		void OnButtonClick()
		{
			End();
		}
	}
}
