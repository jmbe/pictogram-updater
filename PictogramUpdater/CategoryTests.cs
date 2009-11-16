﻿using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace PictogramUpdater {
    
    [TestFixture]
    public class CategoryTranslationServiceTests {
        
        [Test]
        public void ShouldReturnTranslation() {
            const string locale = "SV";
            var service = new CategoryTranslationService();
            var repository = new CategoryRepository();
            var category = repository.FindByCode("a");
            
            Assert.That(service.Translate(category, locale), Is.EqualTo("Människor"));
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
