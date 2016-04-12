using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace CsProjComparer.Tests.Resources
{
    public static class ResourceFiles
    {
        public static readonly string Project1 = ToAbsolute(@"Resources\WPFCore.csproj");
        public static readonly string Project2 = ToAbsolute(@"Resources\SharpGeometry.csproj");

        private static string ToAbsolute(string relative)
        {
            var ctx = TestContext.CurrentContext;
            var testDir = ctx.TestDirectory;
            string absolute = Path.Combine(testDir, relative);
            if (!File.Exists(absolute))
            {
                throw new FileNotFoundException(absolute);
            }
            return absolute;
        }
    }
}