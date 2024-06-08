using System.Text;
using TotCS;

namespace UnitTest
{
    public class Clean
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

            if (Tot.IsFileExistsSync(filename + ".tmp"))
            {
                File.Delete(filename + ".tmp");
            }
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            if (Tot.IsFileExistsSync(filename))
            {
                File.Delete(filename);
            }
            if (Tot.IsFileExistsSync(filename + ".tmp"))
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
			Assert.That(result, Is.True);
        }
    }
}
