using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace DownloadManager {
    
    [TestFixture]
    public class CategoryTranslationServiceTests {
        
        [Test]
        public void ShouldReturnTranslation() {
            var language = new Language("SV", "Svenska");
            var service = new CategoryTranslationService();
            var repository = new CategoryRepository();
            var category = repository.FindByCode("a");
            
            Assert.That(service.Translate(category, language), Is.EqualTo("Människor"));
        }
    }

    [TestFixture]
    public class CategoryRepositoryTests {
        
        [Test]
        public void ShouldReturnAllCategories() {
            var repository = new CategoryRepository();

            Assert.That(repository.FindAll().Count, Is.EqualTo(26));
        }

    }

}
