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

public class FScene : FContainer, FMultiTouchableInterface
{
	protected FSceneManager mSceneManager = null;
	public FSceneManager SceneManager
	{
		get { return mSceneManager; }
		set { mSceneManager = value; }
	}

	protected string mName;
	public string Name
	{
		get { return mName; }
		set { mName = value; }
	}

	protected bool mIsExiting = false;
	public bool IsExiting
	{
		get { return mIsExiting; }
		set { mIsExiting = value; }
	}

	protected FSceneState mState = FSceneState.Active;
	public FSceneState State
	{
		get { return mState; }
		set { mState = value; }
	}

	protected FTransition mTransitionOn = null;
	public FTransition TransitionOn
	{
		get { return mTransitionOn; }
		set { mTransitionOn = value; }
	}

	protected FTransition mTransitionOff = null;
	public FTransition TransitionOff
	{
		get { return mTransitionOff; }
		set { mTransitionOff = value; }
	}

	public FScene( string _name = "Default" ) : base()
	{
		mName = _name;

		ListenForUpdate( HandleUpdate );

		ListenForResize( HandleResize );

		EnableMultiTouch();
	}
	
	virtual public void HandleUpdate ()
	{
		
	}

	virtual public void HandleEnter()
	{

	}

	virtual public void HandleExit()
	{

	}

	virtual public void HandleResize( bool _change )
	{
		
	}

	virtual public void HandleMultiTouch( FTouch[] touches )
	{

	}

	public override string ToString()
	{
		return "Name: " + mName + " - State: " + mState.ToString();
	}
}
