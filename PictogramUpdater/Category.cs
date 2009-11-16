using System;
using System.Collections.Generic;
using System.Text;

namespace PictogramUpdater {
    internal class CategoryRepository {
        private readonly List<Category> _categories;
        private readonly Dictionary<string, Category> _categoriesByCode;
        private readonly Dictionary<int, Category> _categoriesByIndex;

        public CategoryRepository() {
            _categories = new List<Category>();
            
            //Create all categories, with i, w removed.
            for (var index = 1; index <= 26; index++) {
                var code = "";
                if(index <= 8 ) {
                    code = ((char) ('a' + index -1)).ToString(); 
                } else if(index > 21 && index < 25) {
                    code = ((char)('a' + index + 1)).ToString(); 
                }
                else if (index >= 25) {
                    code = "a" + ((char) ('a' + index - 25));
                } else {
                    code = ((char) ('a' + index)).ToString();
                }

                _categories.Add(new Category(index, code));
            }

            _categoriesByCode = new Dictionary<string, Category>();
            _categoriesByIndex = new Dictionary<int, Category>();

            foreach (var category in _categories) {
                _categoriesByIndex[category.Index] = category;
                _categoriesByCode[category.Code] = category;
            }
        }

        public List<Category> FindAll() {
            return _categories;
        }

        public Category FindByCode(string code) {
            return _categoriesByCode[code];
        }

        public Category FindByIndex(int index) {
            return _categoriesByIndex[index];
        }
    }

    internal class CategoryTranslationService {
        private readonly Dictionary<string, string[]> _translations;

        public CategoryTranslationService() {
            _translations = new Dictionary<string, string[]> {
                                                                 {
                                                                     "SV", new[] {
                                                                                     "Människor", "Kroppsdelar",
                                                                                     "Kläder och personliga tillhörighete"
                                                                                     ,
                                                                                     "Inventarier", "Kök",
                                                                                     "Badrum", "Mat",
                                                                                     "Frukt och grönsaker",
                                                                                     "Djur", "Leksaker",
                                                                                     "Trädgård",
                                                                                     "Väder", "Musik", "Fritid",
                                                                                     "Känslor", "Yrken",
                                                                                     "Transportmedel",
                                                                                     "Lokaliteter", "Aktiviteter"
                                                                                     ,
                                                                                     "Högtider", "Egenskaper",
                                                                                     "Läge riktning", "Antal",
                                                                                     "Diverse",
                                                                                     "TV-program", "Frågeord"
                                                                                 }
                                                                     }
                                                             };
        }

        public string Translate(Category category, string locale) {
            return _translations[locale][category.Index - 1];
        }
    }

    internal class Category {
        public Category(int index, string code) {
            Index = index;
            Code = code;
        }

        public int Index { get; set; }
        public string Code { get; set; }
    }
}