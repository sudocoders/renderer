using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Renderer
{
    public class GL33Window : GameWindow, IGLWindow
    {
        private List<IRenderable> Renderables;
        private Dictionary<Key, Action> KeyEvents;
		private Dictionary<MouseInformation, Action> MouseEvents;
        public Color4 ClearColor
        {
            get;
            set;
        }

        public LoadFunction OnLoadFunction;
        public UpdateFunction OnUpdateFunction;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="name"></param>
        /// <param name="fullscreen"></param>
        public GL33Window(int width, int height, string name, GameWindowFlags fullscreen)
            : base(width, height, GraphicsMode.Default, name, fullscreen, DisplayDevice.Default, 3, 3, GraphicsContextFlags.ForwardCompatible)
        {
            Renderables = new List<IRenderable>();
            ClearColor = new Color4();
            OnLoadFunction = null;
            OnUpdateFunction = null;
			KeyEvents = new Dictionary<Key, Action>();
			MouseEvents = new Dictionary<MouseInformation, Action>();
        }

        public void AddRenderable(IRenderable renderable, Matrix4 projectionmatrix, Matrix4 modelviewmatrix)
        {
            renderable.SetModelViewMatrix(modelviewmatrix);
            renderable.SetProjectionMatrix(projectionmatrix);
            Renderables.Add(renderable);
        }
		
		public void AddKeyAction(Key key, Action action)
		{
		    KeyEvents[key] = action;
		}
		
	    public void AddMouseAction(MouseInformation info, Action action)
		{
		    MouseEvents[info] = action;
		}
		
		public void GetMousePosition(out int x, out int y)
		{
		    x = Mouse.X;
			y = Mouse.Y;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnLoad(EventArgs eventargs)
        {
            if(OnLoadFunction != null)
                OnLoadFunction(ref Renderables);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnUnload(EventArgs eventargs)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="manual"></param>
        protected override void Dispose(bool manual)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameeventargs"></param>
        protected override void OnRenderFrame(FrameEventArgs frameeventargs)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(ClearColor);
            foreach(IRenderable renderable in Renderables)
            {
                renderable.Render();
            }
            SwapBuffers();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameeventargs"></param>
        protected override void OnUpdateFrame(FrameEventArgs frameeventargs)
        {
            if(OnUpdateFunction != null)
                OnUpdateFunction(ref Renderables);
			foreach(KeyValuePair<Key, Action> kvp in KeyEvents)
			{
			   if(Keyboard[kvp.Key])
			      kvp.Value();
			}
			foreach(KeyValuePair<MouseInformation, Action> kvp in MouseEvents)
			{
			    //No Position data needed to carry out action
			    if(kvp.Key.X == -1 || kvp.Key.Y == -1)
				   kvp.Value();
				//Make sure mouse is within the threshold (like a button)
				if(Mouse.X >= kvp.Key.X - kvp.Key.XThreshold &&
				   Mouse.X <= kvp.Key.X + kvp.Key.XThreshold &&
				   Mouse.Y >= kvp.Key.Y - kvp.Key.YThreshold &&
				   Mouse.Y <= kvp.Key.Y + kvp.Key.YThreshold)
				   kvp.Value();
			}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnWindowInfoChanged(EventArgs eventargs)
        {
        }
    }
}
