using Cj

namespace Sudocoders
{
  namespace Renderer
  {
    
    public interface IGLRenderableFactory
    {
      IGLRenderer CreateRenderable();
    }
  }
}