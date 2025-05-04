using skat_back.models;
using skat_back.repositories;

namespace skat_back.services.UserService;

public class UserService(IRepository<User> repository) : IUserService
{
    public IEnumerable<User> GetAll()
    {
        return repository.GetAll();
    }

    public User? GetById(int id)
    {
        return repository.GetById(id);
    }

    public void Add(User user)
    {
        repository.Add(user);
    }

    public void Update(int id, User updatedUser)
    {
        repository.Update(id, updatedUser, (existing, updated) =>
        {
            existing.Email = updated.Email;
            existing.Password = updated.Password;
            existing.FirstName = updated.FirstName;
            existing.LastName = updated.LastName;
            existing.UpdatedAt = DateTime.UtcNow;
        });
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }
}