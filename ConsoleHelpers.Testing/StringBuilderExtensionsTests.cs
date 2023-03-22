using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Extensions;
using System.Text;
using System.Reflection;

namespace ConsoleHelpers.Testing
{
    [TestClass]
    public class StringBuilderExtensionsTests
    {
        [TestMethod]
        public void AppendLongStringTest()
        {
            var longString = "Typing a really really long string so that we can test " +
                "appending it and make sure that it gets formatted correctly. This " +
                "should probably be long enough for testing, let's see how it goes.";
            var width = 10;
            var builder = new StringBuilder();
            var extensions = typeof(ConsoleApp).Assembly.GetType("ConsoleHelpers.Helpers.StringBuilderExtensions");
            var methodInfo = extensions?.GetMethod("AppendLongString", BindingFlags.Static | BindingFlags.NonPublic);
            var retObj = methodInfo?.Invoke(null, new object[] { builder, longString, width });
            Assert.IsNotNull(retObj);
            Assert.AreEqual(retObj.GetType(), typeof(StringBuilder));

            var output = builder.ToString();
            Assert.IsTrue(output.Contains("\r\n"));

            var numLines = longString.Length / width;
            var numLinesActual = 0;
            int index = 0;
            int indexOfNewLine = 0;
            while (true)
            {
                indexOfNewLine = output.IndexOf("\r\n", index);
                if (indexOfNewLine == -1) { break; }
                index = indexOfNewLine + 1;
                numLinesActual++;
            }
            Assert.AreEqual(numLines, numLinesActual);
            Assert.AreEqual(longString, output.Replace("\r\n", ""));
        }
    }
}