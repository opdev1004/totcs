using TotCS;

namespace UnitTest
{
    public class GetAll
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
        public async Task RightNameAndData()
        {
            await Tot.Push(filename, "test1", "This is right name and right data 1");
            await Tot.Push(filename, "test2", "This is right name and right data 2");
            await Tot.Push(filename, "test3", "This is right name and right data 3");
            await Tot.Push(filename, "test4", "This is right name and right data 4");
            await Tot.Push(filename, "test5", "This is right name and right data 5");
            await Tot.Push(filename, "test6", "This is right name and right data 6");
            await Tot.Push(filename, "test7", "This is right name and right data 7");
            await Tot.Push(filename, "test8", "This is right name and right data 8");
            TotItemList totItemList = await tot.QGetAll(filename);


			if (totItemList.ItemList.Count != 8)
			{
				Assert.Fail("List must contains 8 items when 8 items are pushed.");
			}

			Assert.That(totItemList.ItemList[6].Data, Is.EqualTo("This is right name and right data 7"));
		}
    }
}