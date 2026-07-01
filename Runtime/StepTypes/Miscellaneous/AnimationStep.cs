using UnityEngine;

using UnityEditor;

namespace LucasSerrano.StepSystem.Types
{
	/// <summary> Step that plays an animation. </summary>
	
	[AddComponentMenu("Step System/Miscellaneous/Animation Step")]
	public class AnimationStep : Step
	{
		[Space][SerializeField] public Animator animator = null;
		[Space][SerializeField] public string parameter = "";
		[SerializeField] public AnimatorControllerParameterType type = AnimatorControllerParameterType.Trigger;

		/// Target value for each type of animator parameter.
		[SerializeField] public float floatValue = 0;
		[SerializeField] public int intValue = 0;
		[SerializeField] public bool boolValue = true;
		/// Value of the animation parameter before this steps modifies it.
		float prevFloat;
		int prevInt;
		bool prevBool;


		// ------------------------------------------------------

		private void Start()
		{
			// Save the initial value of the parameter.
			switch (type)
			{
				case AnimatorControllerParameterType.Float:
					prevFloat = animator.GetFloat(parameter);
					break;

				case AnimatorControllerParameterType.Int:
					prevInt = animator.GetInteger(parameter);
					break;

				case AnimatorControllerParameterType.Bool:
					prevBool = animator.GetBool(parameter);
					break;
			}
		}

		protected override void OnActivate()
		{
			// Change the value in the animator after saving its previous state.
			switch (type)
			{
				case AnimatorControllerParameterType.Float:
					prevFloat = animator.GetFloat(parameter);
					animator.SetFloat(parameter, floatValue);
					break;

				case AnimatorControllerParameterType.Int:
					prevInt = animator.GetInteger(parameter);
					animator.SetInteger(parameter, intValue);
					break;

				case AnimatorControllerParameterType.Bool:
					prevBool = animator.GetBool(parameter);
					animator.SetBool(parameter, boolValue);
					break;

				case AnimatorControllerParameterType.Trigger:
					animator.SetTrigger(parameter);
					break;
			}

			End();
		}

		protected override void OnRestart()
		{
			// Return the parameter to its previous value.
			switch (type)
			{
				case AnimatorControllerParameterType.Float:
					animator.SetFloat(parameter, prevFloat);
					break;

				case AnimatorControllerParameterType.Int:
					animator.SetInteger(parameter, prevInt);
					break;

				case AnimatorControllerParameterType.Bool:
					animator.SetBool(parameter, prevBool);
					break;
			}
		}
	}


	// ------------------------------------------------------------------------------------------------------------
	#region CustomInspector
#if UNITY_EDITOR

	/* Custom inspector that only shows the variable for the target value used of the selected parameter type. */

	[CustomEditor(typeof(AnimationStep))]
	[CanEditMultipleObjects]
	class AnimationStep_Editor : Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			DrawProperty("animator");
			DrawProperty("parameter");

			int type = DrawProperty("type").enumValueIndex;
			switch (type)
			{
				case 0:
					DrawProperty("floatValue");
					break;
				case 1:
					DrawProperty("intValue");
					break;
				case 2:
					DrawProperty("boolValue");
					break;
				default:
					break;
			}

			serializedObject.ApplyModifiedProperties();
		}

		SerializedProperty DrawProperty(string name)
		{
			SerializedProperty property = serializedObject.FindProperty(name);
			EditorGUILayout.PropertyField(property);

			return property;
		}
	}

#endif
	#endregion
}
