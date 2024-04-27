using System.Text;
using TotCS;

namespace UnitTest
{
    public class Clean
    {
        Tot tot = new Tot();
        string filename = "./test.tot";

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

            if (Tot.IsFileExists(filename + ".tmp"))
            {
                File.Delete(filename + ".tmp");
            }
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            if (Tot.IsFileExists(filename))
            {
                File.Delete(filename);
            }
            if (Tot.IsFileExists(filename + ".tmp"))
            {
                File.Delete(filename + ".tmp");
            }
        }
        [Test]
        public async Task CleanUpdate()
        {
            await Tot.Push(filename, "test", "This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.This is right name and right data.", Encoding.UTF8, 128);
            await Tot.Update(filename, "test", "This is updated!");
            bool result = await Tot.Clean(filename);
            Assert.IsTrue(result);
        }
    }
}
