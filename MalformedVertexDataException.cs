using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
