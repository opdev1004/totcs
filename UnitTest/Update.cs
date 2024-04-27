using TotCS;

namespace UnitTest
{
    public class Update
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
            bool result = await Tot.Update(filename, "test", "This is updated!");
            Assert.IsTrue(result);
        }
    }
}
