using System;
using ObjLoader.Loader.Loaders;
using System.Collections.Generic;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;

namespace Renderer
{
  public class GLRenderableFactory
  {
    static private GLRenderableFactory instance;
    private GLRenderableFactory ()
    {
    }

    static public GLRenderableFactory GetInstance()
    {
      if(instance == null)
      {
        instance = new GLRenderableFactory();
      }
      return instance;
    }
    /// <summary>
    /// Creates a list of renderables from an obj load result.
    /// </summary>
    /// <returns>A list of renderables with 1:1 correspondance to obj groups.</returns>
    /// <param name="result">The load result from objLoader.</param>
    public List<T> CreateRenderableFromObj<T>(LoadResult result) where T: IRenderable, new()
    {
      List<T> listofrenderables = new List<T> ();
      foreach(Group g in result.Groups)
      {
        T newrenderable = new T();
        newrenderable.Name = g.Name;
        List<Vertex> vertices = new List<Vertex>();
        List<Normal> normals = new List<Normal>();
        List<TextureCoordinate> texcoords = new List<TextureCoordinate>();
        //Looks at each face in the group
        foreach(Face f in g.Faces)
        {
          //Grabs vertices, normals, and texcoords based on the indices given from the face
          if(result.Vertices.Count > 0)
          {
            for(int i = 0; i < 3; i++)
              vertices.Add (result.Vertices[f[i].VertexIndex]);
          }
          if(result.Normals.Count > 0)
          {
            for(int i = 0; i < 3; i++)
              normals.Add (result.Normals[f[i].NormalIndex]);
          }
          if(result.Textures.Count > 0)
          {
            for(int i = 0; i < 3; i++)
              texcoords.Add (result.Textures[f[i].TextureIndex]);
          }
        }
        newrenderable.AddVertices (vertices);
        newrenderable.AddNormals (normals);
        newrenderable.AddTextureCoordinates(texcoords);
        listofrenderables.Add(newrenderable);
      }
      return listofrenderables;
    }

    public T CreateRenderableFromVertexData<T>(List<Vertex> vertices, List<Normal> normals, List<TextureCoordinate> texcoords) where T: IRenderable, new()
    {
      T renderable = new T ();
      renderable.AddVertices (vertices);
      renderable.AddNormals (normals);
      renderable.AddTextureCoordinates (texcoords);
      return renderable;
    }
  }
}

