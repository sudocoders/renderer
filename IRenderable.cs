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
    protected Vector3 Position;
    protected Quaternion Rotation;
    protected Vector3 Scale;
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
    public void SetTranslation(Vector3 trans)
    {
        this.Position = trans;
    }
    public void SetRotation(Quaternion rot)
    {
        this.Rotation = rot;
    }
    public void SetScale(Vector3 scale)
    {
        this.Scale = scale;
    }
    public Vector3 GetTranslation()
    {
        return this.Position;
    }
    public Quaternion GetRotation()
    {
        return this.Rotation;
    }
    public Vector3 GetScale()
    {
        return this.Scale;
    }


    public abstract void SetUpRenderable();
    public abstract void AddTexture(ITexture tex);
    public abstract void SetShader(IShader shade);
    public abstract void SetProjectionMatrix(Matrix4 projectionmatrix);
    public abstract void SetModelViewMatrix(Matrix4 modelviewmatrix);
    public abstract void Render();
  }
}