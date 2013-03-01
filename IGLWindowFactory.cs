using System;
using OpenTK;

namespace Future_Animal_Wars
{
    namespace Renderer
    {
        public interface IGLWindowFactory
        {
            IGLWindow CreateWindow(int width, int height, string name, GameWindowFlags fullscreen);
        }
    }
}