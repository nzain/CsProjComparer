using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CsProjComparer
{
    public class Program
    {
        private const string Usage = @"Usage: CsProjComparer file1.csproj file2.csproj";
        private static readonly string[] RelevantElements = { "None", "Compile", "Content", "EmbeddedResource" };

        public static FileInfo File1 { get; set; }
        public static FileInfo File2 { get; set; }
        public static XDocument Project1 { get; set; }
        public static XDocument Project2 { get; set; }

        public static int Main(string[] args)
        {
            // 1. check the arguments
            if (args == null || args.Length != 2)
            {
                Console.WriteLine(Usage);
                return -1;
            }
            File1 = new FileInfo(args[0]);
            if (!File1.Exists)
            {
                Console.WriteLine($"Project1 does not exist ({File1.FullName}).");
                return -2;
            }
            File2 = new FileInfo(args[1]);
            if (!File2.Exists)
            {
                Console.WriteLine($"Project2 does not exist ({File2.FullName}).");
                return -2;
            }

            // 2. now parse the xml, may well throw on invalid args.
            Project1 = XDocument.Load(File1.FullName);
            Project2 = XDocument.Load(File2.FullName);

            // 3. compare content, display all differences before return
            bool equal = true;
            foreach (string elementName in RelevantElements)
            {
                equal &= ElementsAreEqual(elementName);
            }
            return equal ? 0 : -3;
        }

        private static bool ElementsAreEqual(string elementName)
        {
            // gather all elements of given elementName (e.g. "Compile") for both csproj files
            HashSet<string> compiles1 = new HashSet<string>(GetFilenames(Project1, elementName));
            HashSet<string> compiles2 = new HashSet<string>(GetFilenames(Project2, elementName));
            return CompareAndDisplayDiff(compiles1, compiles2, elementName);
        }

        private static IEnumerable<string> GetFilenames(XDocument doc, string elementName)
        {
            // XML namespaces are a pain. Avoid them for better compatibility / simplicity?
            foreach (XElement element in doc.Descendants().Where(w => w.Name.LocalName == elementName))
            {
                var include = element.Attributes().Single(s => s.Name.LocalName == "Include");
                yield return include.Value;
            }
        }

        private static bool CompareAndDisplayDiff(HashSet<string> compiles1, HashSet<string> compiles2,
            string elementName)
        {
            string[] missingFrom1 = compiles2.Except(compiles1).ToArray();
            string[] missingFrom2 = compiles1.Except(compiles2).ToArray();
            // human readable output, if any
            DisplayDiff(File1.Name, elementName, missingFrom1);
            DisplayDiff(File2.Name, elementName, missingFrom2);
            return missingFrom1.Length == 0 && missingFrom2.Length == 0;
        }

        private static void DisplayDiff(string fileName, string elementName, string[] missingElements)
        {
            foreach (string missingFile in missingElements)
            {
                Console.WriteLine("{0}: missing <{1} Include=\"{2}\" />", fileName, elementName, missingFile);
            }
        }
    }
}