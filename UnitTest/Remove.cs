using TotCS;

namespace UnitTest
{
    public class Remove
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
        public async Task RightRemove()
        {
            await Tot.Push(filename, "test", "This is right name and right data");
            bool result = await Tot.Remove(filename, "test");
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task CheckDataRemoved()
        {
            await Tot.Push(filename, "test", "This is right name and right data");
            await Tot.Remove(filename, "test");
            (bool result, _) = await Tot.IsOpenTagExists(filename, "test");
            Assert.That(result, Is.False);
        }

    }
}
