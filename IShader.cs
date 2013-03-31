using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Renderer
{
    public interface IShader
    {
        bool LoadVertexShader(string file);
        bool LoadFragmentShader(string file);
        bool LoadGeometryShader(string file);
        bool LinkProgram();

        void Activate();

        int GetAttribLocation(string name);
        int GetUniformLocation(string name);
        int GetProgram();
    }
}
