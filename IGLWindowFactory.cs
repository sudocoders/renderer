using System;
using OpenTK;


namespace Renderer
{
    public interface IGLWindowFactory
    {
        IGLWindow CreateWindow(int width, int height, string name, GameWindowFlags fullscreen);
    }
}