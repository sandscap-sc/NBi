﻿#region Using directives
using System.IO;
using System.Reflection;
using NBi.Xml;
using NBi.Xml.Constraints;
using NUnit.Framework;
#endregion

namespace NBi.Testing.Xml.Unit.Constraints
{
    [TestFixture]
    public class SuccessfulXmlTest : BaseXmlTest
    { 
        [Test]
        public void Deserialize_SampleFile_ReadCorrectlySuccessful()
        {
            int testNr = 0;

            // Create an instance of the XmlSerializer specifying type and namespace.
            TestSuiteXml ts = DeserializeSample();

            Assert.That(ts.Tests[testNr].Constraints[0], Is.TypeOf<SuccessfulXml>());
        }
    }
}
