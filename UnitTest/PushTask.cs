using TotCS;

namespace UnitTest
{
    public class PushTask
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
            await Tot.Push(filename, "test1", "This is right name and right data 1");
            await Tot.Push(filename, "test2", "This is right name and right data 2");
            await Tot.Push(filename, "test3", "This is right name and right data 3");
            await Tot.Push(filename, "test4", "This is right name and right data 4");
            await Tot.Push(filename, "test5", "This is right name and right data 5");
            await Tot.Push(filename, "test6", "This is right name and right data 6");
            await Tot.Push(filename, "test7", "This is right name and right data 7");
            await Tot.Push(filename, "test8", "This is right name and right data 8");
            TotItemListWithLastPosition totItemList = await Tot.GetMultipleData(filename, 4);

            if (totItemList.ItemList.Count != 4)
            {
                Assert.Fail("List must contains 4 items when 4 items are wanted.");
            }

            totItemList = await Tot.GetMultipleData(filename, 2, totItemList.LastPosition);

            if (totItemList.ItemList.Count != 2)
            {
                Assert.Fail("List must contains 2 items when 2 items are wanted.");
            }

            bool result = false;
            TotItem totItem1 = totItemList.ItemList[0];
            TotItem totItem2 = totItemList.ItemList[1];

            if (totItem1.Name.Equals("test5") && totItem2.Name.Equals("test6"))
            {
                result = true;
            }

            Assert.That(result, Is.True);
        }
    }
}