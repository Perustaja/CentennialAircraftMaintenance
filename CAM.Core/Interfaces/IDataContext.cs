namespace CAM.Core.Interfaces
{
    public interface IDataContext
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}