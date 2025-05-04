namespace skat_back;

public interface IUnitOfWork: IDisposable
{
    Task<bool> CommitAsync();
    void Rollback();
}