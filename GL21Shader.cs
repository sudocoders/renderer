using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.IO;
using Renderer.Exceptions;

namespace Renderer
{
    public class GL21Shader : IShader
    {
        private int ShaderProgram;
        private int VertexShader;
        private int FragmentShader;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool LoadVertexShader(string file)
        {
            String vshader;
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    vshader = sr.ReadToEnd();
                }
            }
            catch
            {
                throw new ArgumentException("Vertex file not found!");
            }
            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, vshader);
            GL.CompileShader(VertexShader);
            if (VertexShader == 0)
                throw new OpenGLException("Vertex shader did not compile. Results are: " + GL.GetShaderInfoLog(VertexShader));
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool LoadFragmentShader(string file)
        {
            String fshader;
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    fshader = sr.ReadToEnd();
                }
            }
            catch
            {
                throw new ArgumentException("Fragment shader file not found!");
            }
            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, fshader);
            GL.CompileShader(FragmentShader);
            if (FragmentShader == 0)
                throw new OpenGLException("Fragment shader did not compile. Results are: " + GL.GetShaderInfoLog(FragmentShader));

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool LinkProgram()
        {
            ShaderProgram = GL.CreateProgram();
            GL.AttachShader(ShaderProgram, VertexShader);
            GL.AttachShader(ShaderProgram, FragmentShader);
            GL.LinkProgram(ShaderProgram);
            if (!GL.IsProgram(ShaderProgram))
                throw new OpenGLException("When trying to create shader program there was a problem!");
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Activate()
        {
            if (!GL.IsProgram(ShaderProgram))
                throw new OpenGLException("Shader Program isn't valid!");
            GL.UseProgram(ShaderProgram);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetAttribLocation(string name)
        {
            if (!GL.IsProgram(ShaderProgram))
                throw new OpenGLException("Shader Program isn't valid!");
            return GL.GetAttribLocation(ShaderProgram, name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetUniformLocation(string name)
        {
            if (!GL.IsProgram(ShaderProgram))
                throw new OpenGLException("Shader Program isn't valid!");
            return GL.GetUniformLocation(ShaderProgram, name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetProgram()
        {
            return ShaderProgram;
        }
    }
}