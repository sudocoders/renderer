using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Renderer.Exceptions
{
    class OpenGLException : Exception
    {
        public OpenGLException(string info)
            : base(info)
        {
        }
    }
}
