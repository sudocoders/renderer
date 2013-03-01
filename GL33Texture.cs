using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using OpenTK.Graphics;
using System.Drawing;
using System.Drawing.Imaging;
using Future_Animal_Wars.Renderer.Exceptions;
using System.IO;

namespace Future_Animal_Wars
{
    namespace Renderer
    {
        public class GL33Texture : ITexture
        {
            private int TextureId;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="filename"></param>
            /// <returns></returns>
            public bool Load(string filename)
            {
                if (String.IsNullOrEmpty(filename))
                    throw new ArgumentException("Filename cannot be nil or empty!.", filename);
                Bitmap bmp;
                try
                {
                    bmp = new Bitmap(filename);
                }
                catch(FileNotFoundException)
                {
                    Console.WriteLine("Texture file not found, cannot create texture");
                    return false;
                }
                TextureId = GL.GenTexture();
                if (TextureId == 0)
                    throw new OpenGLException("Texture id could not be generated.");
                GL.BindTexture(TextureTarget.Texture2D, TextureId);

                BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, bmp_data.Scan0);

                bmp.UnlockBits(bmp_data);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                return true;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public int GetTextureHandle()
            {
                return TextureId;
            }
        }
    }
}
