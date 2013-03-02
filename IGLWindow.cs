using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Input;
namespace Future_Animal_Wars
{
    namespace Renderer
    {
		public struct MouseInformation
		{
			public MouseButton Button;
			public int X;
			public int Y;
			public int XThreshold;
			public int YThreshold;
		}
        public delegate void LoadFunction(ref List<IRenderable> renderables);
        public delegate void UpdateFunction(ref List<IRenderable> renderables);
		public delegate void KeyEvent(ref IRenderable renderable);
        public interface IGLWindow
        {
		   void GetMousePosition(out int x, out int y);
		   void AddKeyAction(Key key, Action action);
		   void AddMouseAction(MouseInformation info, Action action);
		}
    }
}
