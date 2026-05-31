using housing;

namespace housingTests
{
    [TestClass]
    public sealed class FilterTests
    {
        private List<Housing> GetTestList()
        {
            return new List<Housing>
            {
                new Housing(1, "Шевченко", "вул. Франка, 1", 45),
                new Housing(2, "Коваленко", "вул. Лесі Українки, 5", 60),
                new Housing(3, "Шепель", "вул. Миру, 10", 30)
            };
        }

        [TestMethod]
        public void FilterBySurname_ShouldRenurnCorrectItems()
        {
            var list = GetTestList();
            string searchQuery = "ше";

            var result = HousingLogic.FilerBySurname(list, searchQuery);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Шевченко", result[0].surname);
            Assert.AreEqual("Шепель", result[1].surname);
        }

        [TestMethod]
        public void FilterByArea_ShouldReturnItemsLagerThanSpecified()
        {
            var list = GetTestList();
            int minArea = 40;

            var result = HousingLogic.FilterByArea(list, minArea);

            Assert.AreEqual(2, result.Count);
            Assert.IsFalse(result.Exists(h => h.surname == "Шепель"));
        }
    }
}
