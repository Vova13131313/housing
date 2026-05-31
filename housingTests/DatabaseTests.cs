using housing;

namespace housingTests
{
    [TestClass]
    public sealed class DatabaseTests
    {
        [TestMethod]
        public void DataAccess_ShoudLoadDataFromMySQL()
        {
            DataAccess dbAccess = new DataAccess();
            Assert.IsNotNull(dbAccess.fList);
            Assert.IsTrue(dbAccess.fList.Count > 0);
        }
    }
}
