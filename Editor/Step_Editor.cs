using UnityEngine;
using UnityEditor;

namespace LucasSerrano.StepSystem.Editor
{
	/// If the Step is active, this draws a yellow rectangle on the top of the component.

	[CustomEditor(typeof(Step), true)]
	[CanEditMultipleObjects]
	class Step_Editor : UnityEditor.Editor
	{
		Step thisStep;

		static GUIStyle labelStyle = null;


		// -------------------------------

		void OnEnable()
		{
			thisStep = target as Step;

			labelStyle = new GUIStyle()
			{
				normal = new GUIStyleState() { textColor = Color.black },
				fontStyle = FontStyle.Bold
			};
		}

		public override void OnInspectorGUI()
		{
			if (thisStep.active)
			{
				Rect rect = EditorGUILayout.GetControlRect();
				EditorGUI.DrawRect(rect, Color.yellow);
				EditorGUI.LabelField(rect, "   Active Step!", labelStyle);

				/// Force Unity to redraw the inspector window.
				EditorUtility.SetDirty(target);
			}

			DrawDefaultInspector();
		}
	}
}
