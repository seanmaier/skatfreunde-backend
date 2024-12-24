using skat_back.data;

namespace skat_back.services.UserService;

public class UserService: IUserService
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository)
    {
        _repository = repository;
    }

    public IEnumerable<User> GetAll()
    {
        return _repository.GetAll();
    }

    public User? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public void Add(User user)
    {
        _repository.Add(user);
    }

    public void Update(int id, User updatedUser)
    {
        _repository.Update(id, updatedUser, (existing, updated) =>
        {
            existing.Email = updated.Email;
            existing.PasswordHash = updated.PasswordHash;
            existing.FirstName = updated.FirstName;
            existing.LastName = updated.LastName;
            existing.UpdatedAt = DateTime.UtcNow;
        });
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}