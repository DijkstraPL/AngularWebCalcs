using Build_IT_Data.Calculators;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Build_IT_DataTests.UnitTests.Calculators
{
    [TestFixture]
    public class ResultTests
    {
        [Test]
        public void ConstructorTest_DescriptionsAreSet()
        {
            var properties = new Dictionary<string, string>
            {
                { "new1", "test1" },
                { "new2", "test2" },
            };
            var result = new Result(properties);

            Assert.AreSame(result.Descriptions, properties);
        }

        [Test]
        public void ConstructorTest_NullPropertiesInserted_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Result(null));
        }

        [Test]
        public void GetPropertiesTest_ReturnsTwoPairs()
        {
            var properties = new Dictionary<string, string>
            {
                { "new1", "test1" },
                { "new2", "test2" },
            };
            var result = new Result(properties);

            var attachedProperties = result.GetProperties();
            attachedProperties["new2"] = "test44";

            Assert.Multiple(() =>
            {
                Assert.That(result.GetProperties().Count, Is.EqualTo(2));
                Assert.That(result.GetProperties().ContainsKey("new1"));
                Assert.That(result.GetProperties()["new1"], Is.EqualTo(null));
                Assert.That(result.GetProperties().ContainsKey("new2"));
                Assert.That(result.GetProperties()["new2"], Is.EqualTo("test44"));
            });
        }
    }
}
