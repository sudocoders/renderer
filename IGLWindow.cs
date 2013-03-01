using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Future_Animal_Wars
{
    namespace Renderer
    {
        public delegate void LoadFunction(ref List<IRenderable> renderables);
        public delegate void UpdateFunction(ref List<IRenderable> renderables);
        public interface IGLWindow
        {
        }
    }
}
