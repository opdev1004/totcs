using TotCS;

namespace UnitTest
{
    public class Get
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
            await Tot.Push(filename, "test", "This is right name and right data");
            string result = await Tot.GetDataByName(filename, "test");
            Assert.That(result, Is.EqualTo("This is right name and right data"));
        }
    }
}