
# Step System
A package to generate chains of steps that will play one after the other. Useful to create complete linear experiences or simply to build small sequences of events inside a bigger experience.


# Setup a Step Chain
You’ll need to add to your scene a game object with the ``StepChain`` component. And add as that object’s children, other game objects with the different components of type ``Step`` that you want. **And that 's it!** When executing the chain each step will be executed, one after the other, in the order they appear in the hierarchy.


The same game object can have multiple ``Step`` components and they will all be executed in the order they appear in the inspector.


Modifying the step chain is as easy as adding ``Step`` components in the children of the step chain or rearranging the game objects in the editor.


This package includes some useful ready-to-use steps like:
|  |  |
|--|--|
| ``GameObjectStep`` | Activates or deactivates a game object.  |
| ``AnimationStep`` | Plays an animation from an Animator component. |
| ``WaitStep`` | Wait a few seconds before continuing to the next step. |
| ``WaitForButtonStep`` | Waits for the user to press a UI button. |

# Create your own Steps
To build your custom experiences you will probably need to create unique step types that solve the specific problems your project presents. For that you'll just need to:

* Create a new class that inherits from ``Step``.
* Override the following functions:
	|   |   |
	|---|---|
	| ``OnActivate()`` | To execute code at the beginning of this step. Here is where the steps usually do what they need to do. | 
	| ``OnEnd()`` | To execute code at the end of the step. |
	| ``OnRestart()`` | To execute code when resetting the step. This function should leave everything the step has changed as it was before activating it. |
* Finally, call the ``End()`` function when you want to end the step and let the chain move to the next one. If it's never called, the chain will be stuck on your step without moving forward.

## Examples
A simple step that prints a message to the console and ends immediately.
```C#
	public class DebugStep : Step
	{
		protected override void OnActivate()
		{
			Debug.Log("Hello World!", this);
			End();
		}
	}
```
Step that waits for something to happen before continuing. ``End()`` is not called immediately after activating the step, but when the event that we are waiting for is fired.
```C#
	public class WaitForSomethingStep : Step
	{
		protected override void OnActivate()
		{
			Something.AddListener(SomethingHappened);
		}


		public void SomethingHappened()
		{
			End();
		}


		protected override void OnEnd()
		{
			Something.RemoveListener(SomethingHappened);
		}
	}
```
Step that activates a game object and returns it to its previous state on restart.
```C#
	public class ActivateObjectStep : Step
	{
		public GameObject targetObject;


		private void Start()
		{
			targetObject.SetActive(false);
		}


		protected override void OnActivate()
		{
			targetObject.SetActive(true);
			End();
		}


		protected override void OnRestart()
		{
			targetObject.SetActive(false);
		}
	}
```
