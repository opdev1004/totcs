using TotCS;

namespace UnitTest
{
    public class GetAt
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
        public async Task RightNameDataAndWrongPosition()
        {
            (bool isExists, _) = await Tot.IsOpenTagExists(filename, "test");
            if (isExists)
            {
                await Tot.Remove(filename, "test");
            }
            await Tot.Push(filename, "test", "This is right name and right data");
            string result = await Tot.GetDataByNameAt(filename, "test", 5);
            Assert.That(result, Is.EqualTo(""));
        }
    }
}