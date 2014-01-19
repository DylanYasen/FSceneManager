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

public class FLayer : FContainer
{
	protected FScene mScene;
	public FScene Scene
	{
		get { return mScene; }
		set { mScene = value; }
	}

	public FLayer ( FScene _scene )
	{
		mScene = _scene;

		ListenForUpdate( HandleUpdate );
		ListenForResize( HandleResize );
	}

	virtual public void HandleUpdate ()
	{
		
	}
	
	virtual public void HandleResize( bool _change )
	{
		
	}
}