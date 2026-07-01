using UnityEngine;

namespace LucasSerrano.StepSystem.Types
{
	/// <summary> Step that activates or desactivates a GameObject. </summary>
	
	[AddComponentMenu("Step System/Miscellaneous/Game Object Step")]
	public class GameObjectStep : Step
	{
		[Space] public GameObject gameObj = null;

		[Space][SerializeField] private Behaviour behaviour = Behaviour.Activate;
		private enum Behaviour { Activate, Deactivate }

		public bool forceInitialState = false;
		private bool prevState;


		// ------------------------------------------------------

		void Start()
		{
			if (forceInitialState)
				gameObj.SetActive(!targetState);
			prevState = gameObj.activeSelf;
		}

		protected override void OnActivate()
		{
			if (gameObj != null)
			{
				prevState = gameObj.activeSelf;
				gameObj.SetActive(targetState);
			}

			End();
		}

		protected override void OnRestart()
		{
			if (gameObj != null)
				gameObj.SetActive(prevState);
		}


		// ------------------------------------------------------

		public bool targetState
		{
			get
			{
				if (behaviour == Behaviour.Activate)
					return true;
				return false;
			}

			set
			{
				if (value == true)
					behaviour = Behaviour.Activate;
				else
					behaviour = Behaviour.Deactivate;
			}
		}
	}
}
