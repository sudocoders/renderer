using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using ObjLoader.Loader.Data.VertexData;

namespace Renderer
{
  public abstract class IRenderable
  {

    protected List<Vertex> Vertices;
    protected List<Normal> Normals;
    protected List<TextureCoordinate> TextureCoordinates;
    public string Name;
    public void AddVertices(List<Vertex> vertices)
    {
        this.Vertices = vertices;
    }
    public void AddNormals(List<Normal> normals)
    {
        this.Normals = normals;
    }
    public void AddTextureCoordinates(List<TextureCoordinate> texcoords)
    {
        this.TextureCoordinates = texcoords;
    }
    public List<Vertex> GetVertices()
    {
        return this.Vertices;
    }
    public List<Normal> GetNormals()
    {
        return this.Normals;
    }
    public List<TextureCoordinate> GetTextureCoordinates()
    {
        return this.TextureCoordinates;
    }
    public abstract void SetUpRenderable();
    public abstract void AddTexture(ITexture tex);
    public abstract void SetShader(IShader shade);
    public abstract void SetTranslation(Vector3 trans);
    public abstract void SetRotation(Quaternion rot);
    public abstract void SetScale(Vector3 scale);
    public abstract void SetProjectionMatrix(Matrix4 projectionmatrix);
    public abstract void SetModelViewMatrix(Matrix4 modelviewmatrix);
    public abstract void Render();
  }
}