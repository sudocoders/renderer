using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Future_Animal_Wars.Renderer
{
    namespace Renderer
    {
        namespace Exceptions
        {

            public class MalformedVertexDataException : Exception
            {
                public MalformedVertexDataException(string info)
                    : base(info)
                {
                }
            }
        }
    }
}
