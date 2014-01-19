/*
- THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
- IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
- FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
- AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
- LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
- OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
- THE SOFTWARE.
*/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum FTransitionDirection
{
	Up,
	Down,
	Left,
	Right
}

public class FTransition
{
	protected FScene mScene;
	public FScene Scene
	{
		get { return mScene; }
		set { mScene = value; }
	}

	protected float mTime = 0.0f;
	public float Time
	{
		get { return mTime; }
		set { mTime = value; }
	}

	protected bool mIsComplete = false;
	public bool IsComplete
	{
		get { return mIsComplete; }
		set { mIsComplete = value; }
	}

	protected FSceneState mNewState = FSceneState.None;
	public FSceneState NewState
	{
		get { return mNewState; }
		set { mNewState = value; }
	}

	virtual public void Start()
	{

	}

	protected void HandleComplete()
	{
		mIsComplete = true;

		if( mNewState != FSceneState.None )
			mScene.State = mNewState;
	}
}

public class FTransitionRotateInFront : FTransition
{
	public FTransitionRotateInFront( FScene _scene, float _time )
	{
		mScene = _scene;
		mTime = _time;
	}
	
	public override void Start()
	{
		mScene.alpha = 0.0f;
		mScene.scale = 10.0f;
		mScene.rotation = 360.0f;

		Go.to( mScene, mTime, new TweenConfig()
		      .setDelay( 0.0f )
		      .floatProp( "alpha", 1.0f )
		      .floatProp( "scale", 1.0f )
		      .floatProp( "rotation", 0.0f )
		      .onComplete( HandleComplete ) );
	}
}

public class FTransitionRotateOutFront : FTransition
{
	public FTransitionRotateOutFront( FScene _scene, float _time )
	{
		mScene = _scene;
		mTime = _time;
	}
	
	public override void Start()
	{
		mScene.alpha = 1.0f;
		mScene.scale = 1.0f;
		mScene.rotation = 0.0f;
		
		Go.to( mScene, mTime, new TweenConfig()
		      .setDelay(0.0f)
		      .floatProp( "alpha", 0.0f )
		      .floatProp( "scale", 10.0f )
		      .floatProp( "rotation", 360.0f )
		      .onComplete( HandleComplete ) );
	}
}

public class FTransitionFadeIn : FTransition
{
	public FTransitionFadeIn( FScene _scene, float _time )
	{
		mScene = _scene;
		mTime = _time;
	}

	public override void Start()
	{
		mScene.alpha = 0.0f;

		Go.to( mScene, mTime, new TweenConfig()
		      .setDelay( 0.0f )
		      .floatProp( "alpha", 1.0f )
		      .onComplete( HandleComplete ) );
	}
}

public class FTransitionFadeOut : FTransition
{
	public FTransitionFadeOut( FScene _scene, float _time )
	{
		mScene = _scene;
		mTime = _time;
	}
	
	public override void Start()
	{
		mScene.alpha = 1.0f;

		Go.to( mScene, mTime, new TweenConfig()
		      .setDelay( 0.0f )
		      .floatProp( "alpha", 0.0f )
		      .onComplete( HandleComplete ) );
	}
}

public class FTransitionSlideIn : FTransition
{
	protected FTransitionDirection mDirection = FTransitionDirection.Down;

	public FTransitionSlideIn( FScene _scene, float _time, FTransitionDirection _direction )
	{
		mScene = _scene;
		mTime = _time;
		mDirection = _direction;
	}

	public override void Start()
	{
		switch( mDirection )
		{
		case FTransitionDirection.Right:
			mScene.SetPosition( mScene.x - Futile.screen.width, 0 );
			break;
		case FTransitionDirection.Left:
			mScene.SetPosition( mScene.x + Futile.screen.width, 0 );
			break;
		case FTransitionDirection.Up:
			mScene.SetPosition( 0, mScene.y - Futile.screen.height );
			break;
		case FTransitionDirection.Down:
			mScene.SetPosition( 0, mScene.y + Futile.screen.height );
			break;
		default:
			break;
		}

		Go.to( mScene, mTime, new TweenConfig()
		      .setDelay( 0.0f )
		      .floatProp( "x", 0 )
		      .floatProp( "y", 0 )
		      .onComplete( HandleComplete ) );
	}
}

public class FTransitionSlideOut : FTransition
{
	protected FTransitionDirection mDirection = FTransitionDirection.Down;

	protected Vector2 mNewPosition = Vector2.zero;
	
	public FTransitionSlideOut( FScene _scene, float _time, FTransitionDirection _direction )
	{
		mScene = _scene;
		mTime = _time;
		mDirection = _direction;
	}
	
	public override void Start()
	{
		switch( mDirection )
		{
		case FTransitionDirection.Right:
			mNewPosition = new Vector2( mScene.x + Futile.screen.width, 0 );
			break;
		case FTransitionDirection.Left:
			mNewPosition = new Vector2( mScene.x - Futile.screen.width, 0 );
			break;
		case FTransitionDirection.Up:
			//mScene.SetPosition(0, mScene.y - Futile.screen.height);
			mNewPosition = new Vector2( 0, mScene.y + Futile.screen.height );
			break;
		case FTransitionDirection.Down:
			//mScene.SetPosition(0, mScene.y + Futile.screen.height);
			mNewPosition = new Vector2( 0, mScene.y - Futile.screen.height );
			break;
		default:
			break;
		}
		
		Go.to( mScene, mTime, new TweenConfig()
		      .setDelay( 0.0f )
		      .floatProp( "x", mNewPosition.x )
		      .floatProp( "y", mNewPosition.y )
		      .onComplete( HandleComplete ) );
	}
}