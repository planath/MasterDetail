using MasterDetail.Core.Helper;
using Moq;
using NUnit.Framework;

namespace MasterDetail.Tests.Repo
{
    [TestFixture]
    public class LocalPersistanceHelperTests
    {
        private ILocalPersistanceHelper _fakeLocalPersistanceHelper;
        private string _jsonString;

        public LocalPersistanceHelperTests()
        {
            _fakeLocalPersistanceHelper = Mock.Of<ILocalPersistanceHelper>();
            _jsonString = "[{\"Id\":1,\"Name\":\"Andreas Plüss\",\"FirstName\":\"André\",\"LastName\":\"Plüss\",\"Email\":\"andi@qlu.ch\",\"Birthday\":\"2006-10-14T00:00:00\",\"Favourite\":false},{\"Id\":2,\"Name\":\"Rudolf Rentiel\",\"FirstName\":\"Rudolf\",\"LastName\":\"Rentiel\",\"Email\":\"rudddi@qlu.ch\",\"Birthday\":\"2002-12-04T00:00:00\",\"Favourite\":false},{\"Id\":3,\"Name\":\"Michi Albrecht\",\"FirstName\":\"Michi\",\"LastName\":\"Albrecht\",\"Email\":\"albani@qlu.ch\",\"Birthday\":\"2002-12-04T00:00:00\",\"Favourite\":false},{\"Id\":4,\"Name\":\"Gerome Acker\",\"FirstName\":\"Gerome\",\"LastName\":\"Acker\",\"Email\":\"cheri@qlu.ch\",\"Birthday\":\"2002-12-04T00:00:00\",\"Favourite\":false}]";
            
        }

        [Test]
        public void GetData_NoSetup_ReturnsDefaultData()
        {
            //arrange
            Mock.Get(_fakeLocalPersistanceHelper).Setup(o => o.GetData())
                .Returns(_jsonString);

            //act
            var data = _fakeLocalPersistanceHelper.GetData();

            //assert
            Assert.IsTrue(data.Equals(_jsonString));
        }

        //[Test]
        //public void SaveData_DefaultData_ReturnsNewlySavedData()
        //{
        //    //arrange
        //    Mock.Get(_fakeLocalPersistanceHelper).Setup(o => o.GetData())
        //        .Returns(_jsonString);
        //    Mock.Get(_fakeLocalPersistanceHelper)
        //        .Setup(o => o.SaveData(It.IsAny<string>()))
        //        .Callback((string s) => _jsonString = s);

        //    var oldString = _jsonString;
        //    var newString =
        //        "[{\"Id\":1,\"Name\":\"Andreas Plüss\",\"FirstName\":\"André\",\"LastName\":\"Plüss\",\"Email\":\"andi@qlu.ch\",\"Birthday\":\"2006-10-14T00:00:00\",\"Favourite\":false}]";

        //    //act
        //    _fakeLocalPersistanceHelper.SaveData(newString);
        //    var data = _fakeLocalPersistanceHelper.GetData();

        //    //assert
        //    Assert.IsTrue(data.Equals(newString));
        //}
    }
}