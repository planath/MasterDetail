namespace MasterDetail.Core.Helper
{
    public interface ILocalPersistanceHelper
    {
        void SaveData(string dataJson);
        string GetData();
    }
}
