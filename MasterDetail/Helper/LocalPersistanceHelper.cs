using Android.App;
using Android.Content;
using MasterDetail.Core.Helper;

namespace MasterDetail.Helpers
{
    public class LocalPersistanceHelper: ILocalPersistanceHelper
    {
        public void SaveData(string dataJson)
        {
            var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutString("People", dataJson);
            prefEditor.Commit();
        }
        public string GetData()
        {
            var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
            var dataJson = prefs.GetString("People", null);

            if (dataJson == null || dataJson.Length <= 5)
            {
                return
                    "[{\"Id\":1,\"Name\":\"Andreas Plüss\",\"FirstName\":\"André\",\"LastName\":\"Plüss\",\"Email\":\"andi@qlu.ch\",\"Birthday\":\"2006-10-14T00:00:00\",\"Favourite\":false},{\"Id\":2,\"Name\":\"Rudolf Rentiel\",\"FirstName\":\"Rudolf\",\"LastName\":\"Rentiel\",\"Email\":\"rudddi@qlu.ch\",\"Birthday\":\"2002-12-04T00:00:00\",\"Favourite\":false},{\"Id\":3,\"Name\":\"Michi Albrecht\",\"FirstName\":\"Michi\",\"LastName\":\"Albrecht\",\"Email\":\"albani@qlu.ch\",\"Birthday\":\"2002-12-04T00:00:00\",\"Favourite\":false},{\"Id\":4,\"Name\":\"Gerome Acker\",\"FirstName\":\"Gerome\",\"LastName\":\"Acker\",\"Email\":\"cheri@qlu.ch\",\"Birthday\":\"2002-12-04T00:00:00\",\"Favourite\":false}]";
            }
            
            return dataJson;
        }
    }
}
