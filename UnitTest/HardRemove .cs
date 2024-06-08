using TotCS;

namespace UnitTest
{
    public class HardRemove
    {
        readonly string filename = "./test.tot";

        [SetUp]
        public void Setup()
        {
            if (!Tot.IsFileExistsSync(filename))
            {
                Tot.CreateFileSync(filename);
            }
            else
            {
                File.Delete(filename);
                Tot.CreateFileSync(filename);
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
            await Tot.Push(filename, "test2", "2");
            bool result = await Tot.HardRemove(filename, "test");
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task CheckDataRemoved()
        {
            await Tot.Push(filename, "test", "This is right name and right data");
            await Tot.Push(filename, "test2", "2");
            await Tot.HardRemove(filename, "test");
            (bool result, _) = await Tot.IsOpenTagExists(filename, "test");
            Assert.That(result, Is.False);
        }

    }
}
