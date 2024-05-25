using NUnit.Framework;

namespace Build_IT_ScriptInterpreterTests.UnitTests.Scripts
{
    [TestFixture]
    public class ScriptBuilderTests
    {
        //[Test]
        //public void ConstructorTest_Success()
        //{
        //    var script = ScriptBuilder.Create("Test", "Desc", "Tag1", "Tag2").Build();

        //    Assert.That(script.Name, Is.EqualTo("Test"));
        //    Assert.That(script.Description, Is.EqualTo("Desc"));
        //    CollectionAssert.Contains(script.Tags, "Tag1");
        //    CollectionAssert.Contains(script.Tags, "Tag2");
        //}
        
        //[Test]
        //public void SetNameTest_Success()
        //{
        //    var scriptBuilder = ScriptBuilder.Create("Test", "Desc", "Tag1", "Tag2");
        //    var script = scriptBuilder.SetName("NewName").Build();

        //    Assert.That(script.Name, Is.EqualTo("NewName"));
        //}

        //[Test]
        //public void SetDescriptionTest_Success()
        //{
        //    var scriptBuilder = ScriptBuilder.Create("Test", "Desc", "Tag1", "Tag2");
        //    var script = scriptBuilder.SetDescription("NewDesc").Build();

        //    Assert.That(script.Description, Is.EqualTo("NewDesc"));
        //}

        //[Test]
        //public void SetGroupNameTest_Success()
        //{
        //    var scriptBuilder = ScriptBuilder.Create("Test", "Desc", "Tag1", "Tag2");
        //    var script = scriptBuilder.SetGroupName("GroupName").Build();

        //    Assert.That(script.GroupName, Is.EqualTo("GroupName"));
        //}

        //[Test]
        //public void SetAuthorTest_Success()
        //{
        //    var scriptBuilder = ScriptBuilder.Create("Test", "Desc", "Tag1", "Tag2");
        //    var script = scriptBuilder.SetAuthor("Author").Build();

        //    Assert.That(script.Author, Is.EqualTo("Author"));
        //}

        //[Test]
        //public void SetDocumentTest_Success()
        //{
        //    var scriptBuilder = ScriptBuilder.Create("Test", "Desc", "Tag1", "Tag2");
        //    var script = scriptBuilder.SetDocument("Document").Build();

        //    Assert.That(script.AccordingTo, Is.EqualTo("Document"));
        //}

        //[Test]
        //public void SetNotesTest_Success()
        //{
        //    var scriptBuilder = ScriptBuilder.Create("Test", "Desc", "Tag1", "Tag2");
        //    var script = scriptBuilder.SetNotes("Note").Build();

        //    Assert.That(script.Notes, Is.EqualTo("Note"));
        //}

        //[Test]
        //public void AppendTagsTest_Success()
        //{
        //    var scriptBuilder = ScriptBuilder.Create("Test", "Desc", "Tag1", "Tag2");
        //    var script = scriptBuilder.AppendTags("Tag3", "Tag4").Build();

        //    CollectionAssert.Contains(script.Tags, "Tag1");
        //    CollectionAssert.Contains(script.Tags, "Tag2");
        //    CollectionAssert.Contains(script.Tags, "Tag3");
        //    CollectionAssert.Contains(script.Tags, "Tag4");
        //}

        //[Test]
        //public void AppendParameterTest_Success()
        //{
        //    var scriptBuilder = ScriptBuilder.Create("Test", "Desc", "Tag1", "Tag2");
        //    var parameter = Mock.Of<IParameter>();

        //    var script = scriptBuilder.AppendParameter(parameter).Build();

        //    Assert.That(script.Parameters.Count, Is.EqualTo(1));
        //}

        //[Test]
        //public void AppendParametersTest_Success()
        //{
        //    var scriptBuilder = ScriptBuilder.Create("Test", "Desc", "Tag1", "Tag2");
        //    var parameter1 = Mock.Of<IParameter>();
        //    var parameter2 = Mock.Of<IParameter>();

        //    var script = scriptBuilder.AppendParameters(parameter1, parameter2).Build();

        //    Assert.That(script.Parameters.Count, Is.EqualTo(2));
        //}
    }
}
