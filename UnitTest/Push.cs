using TotCS;

namespace UnitTest
{
    public class Push
    {
        readonly string filename = "./test.tot";

        [SetUp]
        public void Setup()
        {
            if (!Tot.IsFileExists(filename))
            {
                Tot.CreateFile(filename);
            }
            else
            {
                File.Delete(filename);
                Tot.CreateFile(filename);
            }
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            File.Delete(filename);
        }

        [Test]
        public async Task RightNameAndData()
        {
            (bool isExists, _) = await Tot.IsOpenTagExists(filename, "test");
            if (isExists)
            {
                await Tot.Remove(filename, "test");
            }
            bool result = await Tot.Push(filename, "test", "This is right name and right data");
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task WrongName()
        {
            bool result = await Tot.Push(filename, "<d:test>", "This is wrong name");
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task NullName()
        {
            #pragma warning disable CS8625
            bool result = await Tot.Push(filename, null, "This is wrong name");
            #pragma warning restore CS8625
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task NullData()
        {
            #pragma warning disable CS8625
            bool result = await Tot.Push(filename, "test", null);
            #pragma warning restore CS8625
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EmptyName()
        {
            bool result = await Tot.Push(filename, "", "This is wrong name");
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task WrongData()
        {
            bool result = await Tot.Push(filename, "test", "<d:test>This is wrong name");
            Assert.That(result, Is.False);
        }
    }
}