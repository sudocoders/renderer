using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Future_Animal_Wars.Renderer.Exceptions
{
    class OpenGLException : Exception
    {
        public OpenGLException(string info)
            : base(info)
        {
        }
    }
}
