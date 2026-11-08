using UnityEngine;
using UnityEditor;

namespace LucasSerrano.StepSystem.Editor
{
	/// Highlights in the hierarchy the GameObjects that contain an active Step.

	[InitializeOnLoad]
	public static class StepChain_CustomHierarchy
	{
		static Color backgroundColor = Color.yellow;
		static GUIStyle labelStyle = new GUIStyle()
		{
			normal = new GUIStyleState() { textColor = Color.black },
			fontStyle = FontStyle.BoldAndItalic
		};


		// -------------------------------

		static StepChain_CustomHierarchy()
		{
			// Add listener to event that Unity fires every time it redraws an item of the hierarchy.
			EditorApplication.hierarchyWindowItemByEntityIdOnGUI += OnDrawHierarchyItem;
		}

		static void OnDrawHierarchyItem(EntityId entityId, Rect selectionRect)
		{
			// InistaceId - indicates the object for which this function was called.
			// SelectonRect - is the area in the hierarchy window that the label of that object occupies.

			GameObject hierarchyItem = EditorUtility.EntityIdToObject(entityId) as GameObject;
			if (hierarchyItem == null)
				return;
			Step[] steps = hierarchyItem.GetComponents<Step>();
			if (steps.Length == 0)
				return;

			foreach(Step step in steps)
			{
				if (step.active == true)
				{
					DrawLabel(hierarchyItem.name, selectionRect);
					return;
				}
			}
		}

		static void DrawLabel(string name, Rect rect)
		{ 
			EditorGUI.DrawRect(rect, backgroundColor);
			EditorGUI.LabelField(rect, name, labelStyle);
		}
	}
}
