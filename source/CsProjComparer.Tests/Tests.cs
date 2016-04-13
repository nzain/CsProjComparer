using CsProjComparer.Tests.Resources;
using NUnit.Framework;

namespace CsProjComparer.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void WrongNumberOfArguments()
        {
            string[] args = new string[0];
            int rv = Program.Main(args);
            Assert.That(rv, Is.Not.EqualTo(0));

            args = new[] {"not two"};
            rv = Program.Main(args);
            Assert.That(rv, Is.Not.EqualTo(0));
        }

        [Test]
        public void BadArguments()
        {
            string[] args = {"one", "two, but not exists"};
            int rv = Program.Main(args);
            Assert.That(rv, Is.Not.EqualTo(0));
        }

        [Test]
        public void ValidDifferentFiles()
        {
            string[] args = {ResourceFiles.Project1, ResourceFiles.Project2};
            int rv = Program.Main(args);
            Assert.That(rv, Is.Not.EqualTo(0));
        }

        [Test]
        public void ValidEqualFiles()
        {
            string[] args = {ResourceFiles.Project1, ResourceFiles.Project1};
            int rv = Program.Main(args);
            Assert.That(rv, Is.EqualTo(0));
        }
    }
}