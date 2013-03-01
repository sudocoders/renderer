using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Future_Animal_Wars
{
    namespace Renderer
    {
        public interface ITexture
        {
            bool Load(string filename);
            int GetTextureHandle();
        }
    }
}

