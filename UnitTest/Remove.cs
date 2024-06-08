using System.Runtime.ExceptionServices;
using TotCS;

namespace UnitTest
{
    public class Remove
    {
		readonly Tot tot = new();
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

		[Test]
		public async Task CheckRemovingFromRightPosition()
		{
			await tot.QPush(filename, "test1", "This is right name and right data1");
			await tot.QPush(filename, "test2", "This is right name and right data2");
			await tot.QPush(filename, "test3", "This is right name and right data3");
			await tot.QRemove(filename, "test2");
            string data1 = await tot.QGetDataByName(filename, "test1");
			string data2 = await tot.QGetDataByName(filename, "test3");
            bool result1 = false;
			bool result2 = false;
            if (data1.Equals("This is right name and right data1")) result1 = true;
			if (data2.Equals("This is right name and right data3")) result2 = true;

            Assert.That(result1 && result2, Is.True);
		}

	}
}
