using TotCS;

namespace UnitTest
{
    public class HardUpdate
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
        public async Task RightUpdate()
        {
            await Tot.Push(filename, "test", "This is right name and right data");
            bool result = await Tot.HardUpdate(filename, "test", "This is updated!");
			Assert.That(result, Is.True);
        }
    }
}
