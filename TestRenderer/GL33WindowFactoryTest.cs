using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using Xunit;
using Future_Animal_Wars.Renderer;
namespace Renderer
{
    namespace TestRenderer
    {
        /// <summary>
        /// Summary description for UnitTest1
        /// </summary>
        public class GL33WindowFactoryTest
        {
            [Fact]
            public void GetInstanceTest()
            {
                GL33WindowFactory window = GL33WindowFactory.GetInstance();

                Assert.NotNull(window);
            }
            [Fact]
            public void CreateValidWindowTest()
            {
                IGLWindow window = GL33WindowFactory.GetInstance().CreateWindow(100, 100, "Blah", OpenTK.GameWindowFlags.Default);
                Assert.NotNull(window);
				((GL33Window)window).Dispose();
            }
            [Fact]
            public void CreateInvalidWindowTest()
            {
                Assert.Throws<ArgumentOutOfRangeException>(delegate 
                                                           { 
                                                               GL33WindowFactory.GetInstance().CreateWindow(-100, -100, "Blah", OpenTK.GameWindowFlags.Default);
                                                           });
            }
            [Fact]
            public void CreateWindowWithNoNameTest()
            {
                IGLWindow window = GL33WindowFactory.GetInstance().CreateWindow(100, 100, "", OpenTK.GameWindowFlags.Default);
                Assert.NotNull(window);
				((GL33Window)window).Dispose();
            }
        }
    }
}