using UnityEngine;
using UnityEngine.UI;

namespace LucasSerrano.StepSystem.Types
{
	/// <summary> Step that modifies a UI text. </summary>
 
	[AddComponentMenu("Step System/UI/Text Step")]
	public class TextStep : Step
	{
		[Space] public Text targetText = null;

		[TextArea] public string newText = "";
		string oldText = null;

		[Space] public bool additive = false;


		// ------------------------------------------------------

		protected override void OnActivate()
		{
			if (targetText != null)
			{
				oldText = targetText.text;
				if (additive)
					targetText.text += newText;
				else
					targetText.text = newText;
			}

			End();
		}

		protected override void OnRestart()
		{
			if (targetText == null || oldText == null)
				return;

			targetText.text = oldText;
			oldText = null;
		}
	}
}
