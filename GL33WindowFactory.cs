using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Renderer
{
    public class GL33WindowFactory : IGLWindowFactory
    {
        static private GL33WindowFactory instance;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public GL33WindowFactory GetInstance()
        {
            if (instance == null)
                instance = new GL33WindowFactory();
            return instance;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="name"></param>
        /// <param name="fullscreen"></param>
        /// <returns></returns>
        public IGLWindow CreateWindow(int width, int height, string name, GameWindowFlags fullscreen)
        {
            return new GL33Window(width, height, name, fullscreen);
        }
    }
}