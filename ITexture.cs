using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Renderer
{
    public interface ITexture
    {
        bool Load(string filename);
        int GetTextureHandle();
    }
}
