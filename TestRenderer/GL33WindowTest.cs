using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using Xunit;
using Renderer;

namespace Renderer
{
    namespace TestRenderer
    {
        public class GL33WindowTest
        {
            [Fact]
            public void TestValidConstructor()
            {
                GL33Window window = new GL33Window(800, 600, "blah", OpenTK.GameWindowFlags.Default);
                Assert.NotNull(window);
				window.Dispose();
            }
            [Fact]
            public void TestInvalidWidthConstructor()
            {
                Assert.Throws<ArgumentOutOfRangeException>(delegate { new GL33Window(-800, 600, "blah", OpenTK.GameWindowFlags.Default); });
            }
            [Fact]
            public void TestInvalidHeightConstructor()
            {
                Assert.Throws<ArgumentOutOfRangeException>(delegate { new GL33Window(800, -600, "blah", OpenTK.GameWindowFlags.Default); });
            }

            [Fact]
            public void TestBlankNameConstructor()
            {
                GL33Window window = new GL33Window(800, 600, "", OpenTK.GameWindowFlags.Default);
                Assert.NotNull(window);
				window.Dispose();
            }
        }
    }
}
