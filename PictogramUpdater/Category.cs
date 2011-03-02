using System;
using System.Collections.Generic;
using System.Text;

namespace PictogramUpdater {
    public class CategoryRepository {
        private readonly List<Category> _categories;
        private readonly Dictionary<string, Category> _categoriesByCode;
        private readonly Dictionary<int, Category> _categoriesByIndex;

        public CategoryRepository() {
            _categories = new List<Category>();

            //Create all categories, with i, w removed.
            for (var index = 1; index <= 27; index++) {
                var code = "";
                if (index <= 8) {
                    code = ((char)('a' + index - 1)).ToString();
                } else if (index > 21 && index < 25) {
                    code = ((char)('a' + index + 1)).ToString();
                } else if (index >= 25) {
                    code = "a" + ((char)('a' + index - 25));
                } else {
                    code = ((char)('a' + index)).ToString();
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

    public class CategoryTranslationService {
        private readonly Dictionary<string, string[]> _translations;

        public CategoryTranslationService() {
            _translations = new Dictionary<string, string[]> {
																     {
																	     "SV", new[] {
																					     "Människor", "Kroppsdelar",
																					     "Kläder och personliga tillhörigheter"
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
																					     "TV-program", "Frågeord",
                                                                                         "Återvinning"
																				     }
																	     },


																	     { "UK", new[] {
																		        "People",
                                                                                "Parts of the body",
                                                                                "Clothes Belongings",
                                                                                "Effects",
                                                                                "Kitchen",
                                                                                "Bathroom",
                                                                                "Food",
                                                                                "Fruit Vegetables",
                                                                                "Animals",
                                                                                "Toys",
                                                                                "Garden",
                                                                                "Weather",
                                                                                "Music",
                                                                                "Leisure time",
                                                                                "Feelings",
                                                                                "Professions",
                                                                                "Means of transportation",
                                                                                "Premises",
                                                                                "Activities",
                                                                                "Festivities",
                                                                                "Qualities",
                                                                                "Position Direction",
                                                                                "Numbers",
                                                                                "Various",
                                                                                "TV-programs",
                                                                                "Questions",
                                                                                "Recycling"
																	     }
																	     },

																	     

																	     { "DE", new[] {
																		     "Menschen",
																		    "Körperteile",
																		    "Kleidung und persönliches Eigentum",
																		    "Einrichtung",
																		    "Küche",
																		    "Badezimmer",
																		    "Essen",
																		    "Obst und Gemüse",
																		    "Tiere",
																		    "Spielzeug",
																		    "Garten",
																		    "Wetter",
																		    "Musik",
																		    "Freizeit",
																		    "Gefühle",
																		    "Berufe",
																		    "Transportmittel",
																		    "Örtlichkeiten",
																		    "Aktivitäten",
																		    "Feste",
																		    "Eigenschaften",
																		    "Richtung",
																		    "Anzahl",
																		    "Verschiedenes",
																		    "Fernsehprogramm",
																		    "Fragewörter",
                                                                            "Recycling"
																	     }
																	     },

																	     { "PL", new[] {
																		     "Postacie",
																		    "Części ciała",
																		    "Odzież Rzeczy osobiste",
																		    "Przedmioty z otoczenia",
																		    "Kuchnia",
																		    "Łazienka",
																		    "Żywność",
																		    "Owoce Warzywa",
																		    "Zwierzęta",
																		    "Zabawki",
																		    "Ogród",
																		    "Pogoda",
																		    "Muzyka",
																		    "Sport Czas relaksu",
																		    "Uczucia",
																		    "Zawody",
																		    "Środki transportu",
																		    "Miejsca",
																		    "Czynności",
																		    "Uroczystości Święta",
																		    "Właściwości Cechy",
																		    "Pozycje Kierunki",
																		    "Liczby",
																		    "Różne",
																		    "Programy TV",
																		    "Pytania",
                                                                            "Recycling"
																	     }
																	     },

																	     { "DK", new[] {
																		     "Mennesker",
																		    "Kropsdele",
																		    "Tøj og personligt tilbehør",
																		    "Inventar",
																		    "Køkken",
																		    "Badeværelse",
																		    "Mad",
																		    "Frugt og grønsager",
																		    "Dyr",
																		    "Legetøj",
																		    "Have",
																		    "Vejret",
																		    "Musik",
																		    "Fritid",
																		    "Følelser",
																		    "Erhverv",
																		    "Transportmidler",
																		    "Lokaliteter",
																		    "Aktiviteter",
																		    "Højtider",
																		    "Egenskaber",
																		    "Vej retning",
																		    "Antal",
																		    "Diverse",
																		    "TV-program",
																		    "Spørgeord",
                                                                            "Recycling"
																	     }},

																	     {"ES", new [] {
																	        "Personas",
																	        "Partes del cuerpo",
																	        "Ropas y pertenencias",
																	        "Enseres",
																	        "Cocina",
																	        "Baño",
																	        "Alimentos",
																	        "Frutas y hortalizas",
																	        "Animales",
																	        "Juguetes",
																	        "Jardín",
																	        "Tiempo",
																	        "Música",
																	        "Ocio",
																	        "Sentimientos",
																	        "Profesiones",
																	        "Medios de transporte",
																	        "Lugares",
																	        "Actividades",
																	        "Fiestas",
																	        "Características",
																	        "Posición dirección",
																	        "Cantidad",
																	        "Varios",
																	        "Programas televisivos",
																	        "Interrogativas",
                                                                            "Recycling"
    																	 
																	     }},


																	     {"FI", new[] {
																		    "Ihmiset",
																		    "Ruumiinosat",
																		    "Henkkohttavarat",
																		    "Esineet",
																		    "Keittiö",
																		    "Kylpyhuone",
																		    "Ruoka",
																		    "Kasvikset",
																		    "Eläimet",
																		    "Leikkikalut",
																		    "Puutarha",
																		    "Sää",
																		    "Musiikki",
																		    "Vapaaaika",
																		    "Tunteet",
																		    "Ammatit",
																		    "Kulkuvälineet",
																		    "Paikat",
																		    "Toiminnot",
																		    "Juhlat",
																		    "Omihaisuudet",
																		    "Sijainti suunta",
																		    "Lukumäärä",
																		    "Sekalaista",
																		    "TV-ohjelmat",
																		    "Kysymyssanat",
                                                                            "Recycling"
    																	 
																	     }},


																	     {"FR", new[] {
      																        "Personnes",
                                                                            "Parties du corps",
                                                                            "Vêtements et effets personnels",
                                                                            "Objets usuels",
                                                                            "Cuisine",
                                                                            "Salle de bains",
                                                                            "Alimentation",
                                                                            "Fruits et légumes",
                                                                            "Animaux",
                                                                            "Jouets",
                                                                            "Jardinage",
                                                                            "Temps",
                                                                            "Musique",
                                                                            "Loisirs",
                                                                            "Sentiments",
                                                                            "Métiers",
                                                                            "Transports",
                                                                            "Locaux",
                                                                            "Activités",
                                                                            "Fêtes",
                                                                            "Qualités",
                                                                            "Position direction",
                                                                            "Nombres",
                                                                            "Divers",
                                                                            "Programme de télé",
                                                                            "Questions",
                                                                            "Recycling"
    																	 
																	     }},

																	     {"IS", new[] {
																	        "Fólk",
                                                                            "Líkamshlutar",
                                                                            "Föt persónulegir munir",
                                                                            "Innanstokksmunir",
                                                                            "Eldhúsáhöld",
                                                                            "Baðherbergi",
                                                                            "Matur",
                                                                            "Ávextir grænmeti",
                                                                            "Dýr",
                                                                            "Leikföng",
                                                                            "Garður",
                                                                            "Veður",
                                                                            "Tónlist",
                                                                            "Tómstundir",
                                                                            "Tilfinningar",
                                                                            "Starfsheiti",
                                                                            "Samgöngutæki",
                                                                            "Næsta umhverfi",
                                                                            "Athafnir",
                                                                            "Hátíðir",
                                                                            "Eiginleikar",
                                                                            "Staðsetning",
                                                                            "Fjöldi",
                                                                            "Ýmislegt",
                                                                            "Sjónvarpsefni",
                                                                            "Spurnarorð",
                                                                            "Recycling"
    																	 
																	     }},



																	     {"IT", new[] {
                                                                            "Persone",
                                                                            "Parti del corpo",
                                                                            "Vestiti ed effetti personali",
                                                                            "Mobili ed accessori",
                                                                            "Cucina",
                                                                            "Bagno",
                                                                            "Alimenti",
                                                                            "Frutta e verdura",
                                                                            "Animali",
                                                                            "Giocattoli",
                                                                            "Giardino",
                                                                            "Tempo",
                                                                            "Musica",
                                                                            "Tempo libero",
                                                                            "Sensazioni",
                                                                            "Professioni",
                                                                            "Mezzi di trasporto",
                                                                            "Ambienti",
                                                                            "Attività",
                                                                            "Feste",
                                                                            "Proprietà",
                                                                            "Posizione direzione",
                                                                            "Quantità",
                                                                            "Varie",
                                                                            "Programmi TV",
                                                                            "Pronomi ed avverbi interrogativi",
                                                                            "Recycling"
																	     }},

																	     {"LE", new[] {
																	        "Cilvēki",
                                                                            "Ķermeņa daļas",
                                                                            "Apģērbs",
                                                                            "Priekšmeti",
                                                                            "Virtuve",
                                                                            "Vannas istaba",
                                                                            "Ēdiens",
                                                                            "Augļi Dārzeņi",
                                                                            "Dzīvnieki",
                                                                            "Rotaļlietas",
                                                                            "Dārzs",
                                                                            "Laika apstākļi",
                                                                            "Mūzika",
                                                                            "Brīvais laiks",
                                                                            "Jūtas",
                                                                            "Arodi",
                                                                            "Transports",
                                                                            "Telpas",
                                                                            "Darbības",
                                                                            "Svētki",
                                                                            "Īpašības",
                                                                            "Stāvoklis Virziens",
                                                                            "Skaitļi",
                                                                            "Dažādi",
                                                                            "TV-programmas",
                                                                            "Jautājumi",
                                                                            "Recycling"
																	     }},

																	     {"LT", new[] {
																	        "Žmonės",
                                                                            "Kūno dalys",
                                                                            "Asmeniniai daiktai",
                                                                            "Daiktai",
                                                                            "Virtuvė",
                                                                            "Vonios reikmenys",
                                                                            "Maistas",
                                                                            "Vaisiai Daržovės",
                                                                            "Gyvūnai",
                                                                            "Žaislai",
                                                                            "Sodas",
                                                                            "Gamtos reiškiniai",
                                                                            "Muzikos instrumentai",
                                                                            "Laisvalaikis",
                                                                            "Jausmai",
                                                                            "Profesijos",
                                                                            "Transporto priemonės",
                                                                            "Patalpos",
                                                                            "Veikla",
                                                                            "Šventės",
                                                                            "Ypatybės",
                                                                            "Kryptys",
                                                                            "Skaičiai",
                                                                            "Įvairūs",
                                                                            "Televizijos programos",
                                                                            "Klausimai",
                                                                            "Recycling"
																	     }},

																	     {"NO", new[] {
																	        "Mennesker",
                                                                            "Kroppsdeler",
                                                                            "Klær/pers.eiendeler",
                                                                            "Inventar",
                                                                            "Kjøkken",
                                                                            "Bad",
                                                                            "Mat",
                                                                            "Frukt/grønnsaker",
                                                                            "Dyr",
                                                                            "Leker",
                                                                            "Hage",
                                                                            "Vær",
                                                                            "Musikk",
                                                                            "Fritid",
                                                                            "Følelser",
                                                                            "Yrker",
                                                                            "Transportmidler",
                                                                            "Lokaliteter/sted",
                                                                            "Aktiviteter",
                                                                            "Høytider",
                                                                            "Egenskaper",
                                                                            "Retning/plassering",
                                                                            "Antall",
                                                                            "Diverse",
                                                                            "TV-program",
                                                                            "Spørreord",
                                                                            "Recycling"
    																	 
																	     }},


																	     {"PT", new[] {

																		    "Pessoas",
                                                                            "Partes do Corpo",
                                                                            "Roupas e objectos pessoais",
                                                                            "Objectos",
                                                                            "Cozinha",
                                                                            "Casa de Banho",
                                                                            "Comida",
                                                                            "Frutas e Legumes",
                                                                            "Animais",
                                                                            "Brinquedos",
                                                                            "Jardim",
                                                                            "O Tempo",
                                                                            "Música",
                                                                            "Tempos Livres",
                                                                            "Emoções",
                                                                            "Profissão",
                                                                            "Meios de Transporte",
                                                                            "Locais",
                                                                            "Actividades",
                                                                            "Festividades",
                                                                            "Características",
                                                                            "Localização direcção",
                                                                            "Quantidade",
                                                                            "Diversos",
                                                                            "Programas de Televisão",
                                                                            "Palavras que definem uma pergunta",
                                                                            "Recycling"
																	     }},

																	     {"RU", new[] {
    																	 
																	        "Люди",
                                                                            "Части тела",
                                                                            "Личные принадлежности",
                                                                            "Инвентарь",
                                                                            "Кухня",
                                                                            "Ванная",
                                                                            "Еда",
                                                                            "Фрукты и овощи",
                                                                            "Животные",
                                                                            "Игрушки",
                                                                            "Сад",
                                                                            "Явления природы",
                                                                            "Музыкальные инструменты",
                                                                            "Досуг",
                                                                            "Чувства",
                                                                            "Профессии",
                                                                            "Транспортные средства",
                                                                            "Помещения",
                                                                            "Занятия",
                                                                            "Праздники",
                                                                            "Свойства",
                                                                            "Положение направление",
                                                                            "Число",
                                                                            "Разное",
                                                                            "программа ТВ",
                                                                            "Вопросительные слова",
                                                                            "Recycling"
																	     }}

															     };
        }

        public string Translate(Category category, Language language) {
            return _translations[language.Code][category.Index - 1];
        }
    }

    public class Category {
        public Category(int index, string code) {
            Index = index;
            Code = code;
        }

        public int Index { get; set; }
        public string Code { get; set; }
    }
}