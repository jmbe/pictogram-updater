using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace PictogramUpdater {
    [TestFixture]
    public class PictogramEntryTests {

        [Test]
        public void ShouldReturnCode() {
            var entry = new PictogramEntry("a3", "flicka");
            Assert.That(entry.CategoryCode, Is.EqualTo("a"));
        }
    }
}
