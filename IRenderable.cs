using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Future_Animal_Wars
{
    namespace Renderer
    {
        public interface IRenderable
        {
            void AddVertices(List<Vector3> vertices);
            void AddNormals(List<Vector3> normals);
            void AddTextureCoordinates(List<Vector2> texcoords);
            void SetUpRenderable();
            void AddTexture(ITexture tex);
            void AddShader(IShader shade);
            void SetTranslation(Vector3 trans);
            void SetRotation(Quaternion rot);
            void SetScale(Vector3 scale);
            void SetProjectionMatrix(Matrix4 projectionmatrix);
            void SetModelViewMatrix(Matrix4 modelviewmatrix);
            //Vector3 GetTranslation();
            //Vector3 GetScale();
            //Quaternion GetRotation();
            void Render();
        }
    }
}