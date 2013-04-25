using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Renderer.Exceptions;


namespace Renderer
{
    public class GL33Renderable : IRenderable
    {

        private IShader Shader;
        private List<ITexture> Textures;
        private Dictionary<int, ITexture> Uniform_To_Texture;
        private int[] VertexBufferObject;
        private int VertexArrayObject;

        private Matrix4 ModelViewMatrix;
        private Matrix4 ProjectionMatrix;
        private Matrix4 TransformationMatrix;
        /// <summary>
        /// This can only be created after an opengl context has been created.
        /// </summary>
        public GL33Renderable()
        {
            GL.GenVertexArrays(1, out VertexArrayObject);
            GL.BindVertexArray(VertexArrayObject);
            if(!GL.IsVertexArray(VertexArrayObject))
                throw new OpenGLException("Vertex array object could not be created!");
            VertexBufferObject = new int[3];
            GL.GenBuffers(3, VertexBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject[0]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject[1]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject[2]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            if (!GL.IsBuffer(VertexBufferObject[0]) || !GL.IsBuffer(VertexBufferObject[1]) || !GL.IsBuffer(VertexBufferObject[2]))
                throw new OpenGLException("Vertex buffer object was not created!");

            Textures = new List<ITexture>();
            Uniform_To_Texture = new Dictionary<int, ITexture>();
            Position = new Vector3(0.0f, 0.0f, 0.0f);
            Rotation = new Quaternion();
            Scale = new Vector3(1.0f, 1.0f, 1.0f);
            TransformationMatrix = Matrix4.Identity;
            ModelViewMatrix = Matrix4.Identity;
            ProjectionMatrix = Matrix4.Identity;
            Shader = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mat"></param>
        public override void SetModelViewMatrix(Matrix4 mat)
        {
            ModelViewMatrix = mat;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mat"></param>
        public override void SetProjectionMatrix(Matrix4 mat)
        {
            ProjectionMatrix = mat;
        }
        /// <summary>
        /// ***This requires shaders to have uniforms called viewmatrix, projmatrix, and transformmatrix.***
        /// </summary>
        public override void Render()
        {
            if (Shader == null)
                throw new NullReferenceException("Cannot render without attached shader.");
            var viewmat = Shader.GetUniformLocation("viewmatrix");
            var projmat = Shader.GetUniformLocation("projmatrix");
            var transmat = Shader.GetUniformLocation("transformmatrix");
            var scalematrix = new Matrix4(Scale.X, 0.0f, 0.0f, 0.0f, 0.0f, Scale.Y, 0.0f, 0.0f, 0.0f, 0.0f, Scale.Z, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);
            Vector3 axis;
            Matrix4 rotmatrix;
            if (Rotation.Length > 0.0f)
            {
                float angle;
                Rotation.ToAxisAngle(out axis, out angle);
                rotmatrix = Matrix4.CreateFromAxisAngle(axis, angle);
            }
            else
                rotmatrix = Matrix4.Identity;
            var posmatrix = OpenTK.Matrix4.CreateTranslation(Position);
            TransformationMatrix = posmatrix * rotmatrix * scalematrix;
                                   
            Shader.Activate();
            GL.UniformMatrix4(viewmat, false, ref ModelViewMatrix);
            GL.UniformMatrix4(projmat, false, ref ProjectionMatrix);
            GL.UniformMatrix4(transmat, false, ref TransformationMatrix);
            int texture_unit = 0;
            foreach (KeyValuePair<int, ITexture> kvp in Uniform_To_Texture)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + texture_unit);
                GL.Uniform1(kvp.Key, texture_unit);
                GL.BindTexture(TextureTarget.Texture2D, kvp.Value.GetTextureHandle());
                texture_unit++;
            }
            if (!GL.IsVertexArray(VertexArrayObject))
                throw new OpenGLException("Vertex Array Object not set up correctly, cannot render!");
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(BeginMode.Triangles, 0, Vertices.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tex"></param>
        public override void AddTexture(ITexture tex)
        {
            Textures.Add(tex);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shade"></param>
        public override void SetShader(IShader shade)
        {
            Shader = shade;
        }
        /// <summary>
        ///  Requires shader to have attribs called position, normal, and texcoord
        /// </summary>
        public override void SetUpRenderable()
        {
            GL.BindVertexArray(VertexArrayObject);
            if(Vertices.Count == 0)
            {
                throw new MalformedVertexDataException("There are no vertices!");
            }
            if(Vertices.Count > 0)
            {
                List<float> vertex_information = new List<float>();
                var vertexLoc = Shader.GetAttribLocation("position");

                for(int i = 0; i < Vertices.Count; i++)
                {
                    vertex_information.Add(Vertices[i].X);
                    vertex_information.Add(Vertices[i].Y);
                    vertex_information.Add(Vertices[i].Z);
                }
                if (!GL.IsBuffer(VertexBufferObject[0]))
                    throw new OpenGLException("Vertex Buffer Object is not valid! Cannot set up renderable!");
                GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject[0]);
                GL.BufferData<float>(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * vertex_information.Count), vertex_information.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(vertexLoc);
                GL.VertexAttribPointer(vertexLoc, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
            if(Normals.Count > 0)
            {
                List<float> normal_information = new List<float>();
                var normalLoc = Shader.GetAttribLocation("normal");
                for(int i = 0; i < Normals.Count; i++)
                {
                    normal_information.Add(Normals[i].X);
                    normal_information.Add(Normals[i].Y);
                    normal_information.Add(Normals[i].Z);
                }
                if(!!GL.IsBuffer(VertexBufferObject[1]))
                    throw new OpenGLException("Vertex Buffer Object is not valid! Cannot set up renderable!");


                GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject[1]);
                GL.BufferData<float>(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * normal_information.Count), normal_information.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(normalLoc);
                GL.VertexAttribPointer(normalLoc, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
            else
            {
                VertexBufferObject[1] = -1;
            }
            if(TextureCoordinates.Count > 0)
            {
                List<float> texcoord_information = new List<float>();
                var texLoc = Shader.GetAttribLocation("texcoord");
                for(int i = 0; i < TextureCoordinates.Count; i++)
                {
                    texcoord_information.Add(TextureCoordinates[i].X);
                    texcoord_information.Add(TextureCoordinates[i].Y);
                }
                if(!GL.IsBuffer(VertexBufferObject[2]))
                    throw new OpenGLException("Vertex Buffer Object is not valid! Cannot set up renderable!");


                GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject[2]);
                GL.BufferData<float>(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * texcoord_information.Count), texcoord_information.ToArray(), BufferUsageHint.StaticDraw);
                GL.EnableVertexAttribArray(texLoc);
                GL.VertexAttribPointer(texLoc, 2, VertexAttribPointerType.Float, false, 0, 0);
 
            }
            else
            {
                VertexBufferObject[2] = -1;
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            for(int i = 0; i < Textures.Count; i++)
            {
                Console.WriteLine("Preparing texture.");
                Uniform_To_Texture[GL.GetUniformLocation(Shader.GetProgram(), "diffuse")] = Textures[i];
            }
            GL.BindVertexArray(0);
        }
    }
}
