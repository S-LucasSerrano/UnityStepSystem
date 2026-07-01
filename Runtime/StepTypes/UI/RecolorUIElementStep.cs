using UnityEngine;
using UnityEngine.UI;

namespace LucasSerrano.StepSystem.Types
{
	/// <summary> Step that changes the color of a UI graphic element. </summary>

	[AddComponentMenu("Step System/UI/Recolor UI Element Step")]
	public class RecolorUIElementStep : Step
	{
		[Space] public Graphic targetGraphic = null;

		public Color newColor = Color.yellow;
		Color prevColor = Color.white;


		// ------------------------------------------------------

		protected virtual void Start()
		{
			prevColor = targetGraphic.color;
		}

		protected override void OnActivate()
		{
			prevColor = targetGraphic.color;
			targetGraphic.color = newColor;
			End();
		}

		protected override void OnRestart()
		{
			targetGraphic.color = prevColor;
		}
	}
}
