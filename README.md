FSceneManager v2.0
=============

SceneManager for Futile, a Unity framework.

updates
-------------

- Transitions have been added. Could always make more!
- Tilemap stuff removed. May return as its own lib.
- Updated how the HandleEnter, HandleExit, and HandleUpdate methods are called.
- Scenes have states now. TransitionOn, TransitionOff, Active, and Paused.


methods
-------------
- FSceneManager.Instance.SetScene(); Sets the new scene passed as the only scene in the stack.
- FSceneManager.Instance.PushScene(); Pushes a new scene onto the stack.
- FSceneManager.Instance.PopScene(); Removes the last scene added to the stack.
- FSceneManager.Instance.RemoveScene(); Removes a specific scene from the stack.



how-to example
-------------

[Game].cs (This is your main .cs file that loads Futile)
```csharp
FSceneManager.Instance.SetScene( new SceneGame() );
```

SceneGame.cs
```csharp
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneGame : FScene
{
	public SceneGame() : base()
	{
		Name = "Game";

		mTransitionOn = new FTransitionSlideIn(this, 0.5f, FTransitionDirection.Down);
		mTransitionOff = new FTransitionSlideOut(this, 0.5f, FTransitionDirection.Left);
	}

	public override void HandleEnter()
	{
		// Called when the Scene is added to the SceneManager
	}

	public override void HandleExit()
	{
		// Called when the Scene is removed from the SceneManager
	}

	public override void HandleUpdate()
	{
		if( mState != FSceneState.Active )
			return;


	}
```


Thanks
-------------

- @MattRix [https://github.com/MattRix] - Awesome Futile framework
- @ironpencil [https://github.com/ironpencil] - Idea's on how to improve FSceneManager
- @jfleschler [https://github.com/jfleschler] - Code to load Tilemap Atlases


Contact
-------------
- Visit [http://manaorb.com] for info and updates.
