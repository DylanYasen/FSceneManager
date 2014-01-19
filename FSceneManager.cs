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
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum FSceneState
{
	None,
	TransitionOn,
	TransitionOff,
	Active,
	Paused
}

public sealed class FSceneManager : FContainer
{
	private static readonly FSceneManager mInstance = new FSceneManager();
	public static FSceneManager Instance
	{
		get
		{
			return mInstance;
		}
	}
	
	private List<FScene> mScenes;
	private List<FScene> mRemoveScenes;

	public static FStage mStage;

	private FSceneManager() : base()
	{
		mScenes = new List<FScene>();
		mRemoveScenes = new List<FScene>();

		mStage = Futile.stage;

		mStage.AddChild( this );

		ListenForUpdate( HandleUpdate );
	}

	public void SetScene( FScene _scene )
	{
		while( mScenes.Count > 0 )
			PopScene();
		
		PushScene( _scene );
	}
	
	public void PushScene( FScene _scene, bool _pause = true )
	{
		if( _pause )
		foreach( FScene scene in mScenes )
			scene.State = FSceneState.Paused;

		// Add to Scene list
		mScenes.Add( _scene );
		// Add to display
		AddChild( _scene );

		// Scene is starting
		_scene.HandleEnter();

		// If Scene has a transition, run it
		if( _scene.TransitionOn != null )
		{
			_scene.State = FSceneState.TransitionOn;
			_scene.TransitionOn.Start();
			_scene.TransitionOn.NewState = FSceneState.Active;
		}
	}

	public void PopScene()
	{
		if( mScenes.Count > 0 )
		{
			FScene scene = mScenes[ mScenes.Count - 1 ];

			if( scene.TransitionOff != null )
			{
				scene.TransitionOff.Start();
				scene.State = FSceneState.TransitionOff;

				mScenes.Remove( scene );
				mRemoveScenes.Add( scene );
			}
			else
				RemoveScene( scene );
				
		}
	}

	private void RemoveScene( FScene _scene )
	{
		if( mScenes.Contains( _scene ) )
		{
			_scene.RemoveFromContainer();
			_scene.HandleExit();

			mScenes.Remove( _scene );
		}

		// Unpause scene
		if( mScenes.Count > 0 )
		{
			FScene scene = mScenes[ mScenes.Count - 1 ];
			scene.State = FSceneState.Active;
		}
	}

	private void HandleUpdate()
	{
		for( int i = mRemoveScenes.Count - 1; i >= 0; i-- )
		{
			if( mRemoveScenes[ i ].TransitionOff.IsComplete == true )
			{
				FScene scene = mRemoveScenes[ i ];

				scene.RemoveFromContainer();
				scene.HandleExit();

				mRemoveScenes.RemoveAt( i );

				// Unpause scene
				if( mScenes.Count > 0 )
				{
					FScene bottomscene = mScenes[ mScenes.Count - 1 ];
					bottomscene.State = FSceneState.Active;
				}
			}
		}
	}
	
	// Added after looking at Iron Pencil's implementation. Thanks!
	private string GetSceneList()
    {
        string sceneList = "";
		
		int i = 1;
		
		foreach( FScene scene in mScenes )
		{
			sceneList += "[" + i + "] - " + scene.ToString() + "\r\n";
			i++;
		}
		
        return sceneList;
    }
}