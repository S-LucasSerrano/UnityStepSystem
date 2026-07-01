using UnityEngine;

namespace LucasSerrano.StepSystem
{
	/// <summary>
	/// Base class for all the Steps to inherit from.</summary>

	public abstract class Step : MonoBehaviour
	{
		/// <summary> Reference to the StepChain this Step is part of. </summary>
		protected StepChain chain { get; set; } = null;
		/// <summary> TRUE while this Step is active. </summary>
		public bool active { get; private set; }


		// ------------------------------------------------------------------------------------------------------------
		#region Initialization

		/// <summary> Set the StepChain this Step is part of. </summary>
		public void SetChain(StepChain chain)
		{
			if (this.chain != null)
				return;
			this.chain = chain;
		}

		#endregion


		// ------------------------------------------------------------------------------------------------------------
		#region Step Functions

		/*	Each function has 2 parts, one public that is called from other classes and components.	*
		 *	And a virtual one for each specific Step type to override doing its own thing.			*/

		// ------------------------------------------------------

		public void Activate()
		{
			// Skip this Step if the component or the game object are disabled.
			if (!enabled || !gameObject.activeSelf || !gameObject.activeInHierarchy)
			{
				if (chain != null) chain.StepEnded(this);
				return;
			}

			active = true;
			OnActivate();
		}

		/// <summary> Override to do something at the beggining of the Step. </summary>
		protected virtual void OnActivate() { }

		// ------------------------------------------------------

		public void End()
		{
			if (active == false)
				return;

			OnEnd();

			active = false;
			if (chain != null)
				chain.StepEnded(this);
		}

		/// <summary> Override to do something at the end of the Step. </summary>
		protected virtual void OnEnd() { }

		// ------------------------------------------------------

		public void Restart()
		{
			active = false;

			OnRestart();
		}

		/// <summary> Override to do something when restarting the chain. <br/>
		/// It should leave everything as it was before executing this Step. </summary>
		protected virtual void OnRestart() { }

		#endregion


		// ------------------------------------------------------------------------------------------------------------
		#region On Disable

		// When disabling this component, end it if it was active.
		private void OnDisable()
		{
			if (_quiting)
				return;

			if (chain != null && active) End();
		}

		// Unity calls OnDisable when exiting play mode, doing this keeps some Steps from throwing errors in the editor.
		private bool _quiting = false;
		private void OnApplicationQuit() => _quiting = true;

		#endregion
	}
}
