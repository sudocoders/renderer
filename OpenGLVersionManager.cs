using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace Renderer
{
    public class OpenGLVersionManager
		{
		    static private OpenGLVersionManager instance;
				static public OpenGLVersionManager GetInstance()
				{
				    if(instance == null)
						{
						    instance = new OpenGLVersionManager();
					  }
						return instance;
				}
				
				public int GetVersion()
				{
				    string version = GL.GetString(StringName.Version);
						int major = int.Parse(version[0].ToString());
						int minor = int.Parse(version[2].ToString());
				    return major * 10 + minor;
				}
		}
}