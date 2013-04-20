using System;
using ObjLoader.Loader.Loaders;
using System.Collections.Generic;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;

namespace Renderer
{
  public class GL33RenderableFactory
  {
    static private GL33RenderableFactory instance;
    private GL33RenderableFactory ()
    {
    }

    static public GL33RenderableFactory GetInstance()
    {
      if(instance == null)
      {
        instance = new GL33RenderableFactory();
      }
      return instance;
    }
    /// <summary>
    /// Creates a list of renderables from an obj load result.
    /// </summary>
    /// <returns>A list of renderables with 1:1 correspondance to obj groups.</returns>
    /// <param name="result">The load result from objLoader.</param>
    public List<GL33Renderable> CreateRenderableFromObj(LoadResult result)
    {
      List<GL33Renderable> listofrenderables = new List<GL33Renderable> ();
      foreach(Group g in result.Groups)
      {
        GL33Renderable newrenderable = new GL33Renderable();
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

    public GL33Renderable CreateRenderableFromVertexData(List<Vertex> vertices, List<Normal> normals, List<TextureCoordinate> texcoords)
    {
      GL33Renderable renderable = new GL33Renderable ();
      renderable.AddVertices (vertices);
      renderable.AddNormals (normals);
      renderable.AddTextureCoordinates (texcoords);
      return renderable;
    }
  }
}

