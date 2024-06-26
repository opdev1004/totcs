﻿using TotCS;

namespace UnitTest
{
    public class Exists
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
        public async Task ExistsRight()
        {
            await Tot.Push(filename, "test", "This is right name and right data");
            (bool result, _) = await Tot.IsOpenTagExists(filename, "test");
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ExistsNone()
        {
            (bool result, _) = await Tot.IsOpenTagExists(filename, "test");
            Assert.That(result, Is.False);
        }


    }
}
