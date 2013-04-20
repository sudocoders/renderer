﻿using System;
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
        /// Gets the singleton instance of GL33WindowFactory
        /// </summary>
        /// <returns>GL33WindowFactory Instance</returns>
        static public GL33WindowFactory GetInstance()
        {
            if (instance == null)
                instance = new GL33WindowFactory();
            return instance;
        }
        /// <summary>
        /// Creates a window of specified width, heigh, name, and whether or not it is fullscreen
        /// </summary>
        /// <param name="width">The width of the screen</param>
        /// <param name="height">The height of the screen</param>
        /// <param name="name">The name of the screen</param>
        /// <param name="fullscreen">Whether or not the screen is full screen.</param>
        /// <returns>A new GL33Window</returns>
        public IGLWindow CreateWindow(int width, int height, string name, GameWindowFlags fullscreen)
        {
            return new GL33Window(width, height, name, fullscreen);
        }
    }
}