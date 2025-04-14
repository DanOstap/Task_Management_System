using Task_Management_System.Models;

namespace Task_Management_System.Service;
public interface IUsersService
{
    public Task<Users> CreateUsers(Users dto);
    public Task<Users?> FindUsersOneByEmail(string email);
    public Task<Users?> FindOneByActivationLink(string activationLink);
    public Task<List<Users>?> FindAll();
    public Task<Users?> FindOneById(int id);
    public Task<Users?> Update(int id, Users model);
    public Task<Users?> Remove(int id);
}
public class UserService : IUsersService
{
    private readonly DataBaseService contex;

    public UserService(DataBaseService contex)
    {
        this.contex = contex;
    }

    async public Task<Users> CreateUsers(Users dto)
    {
        Users user = new Users();
        user.User_Email = dto.User_Email;
        user.User_Password = dto.User_Password;
        user.User_Activation_Link = this.GenerateRandomString(10);

        return dto;
    }
    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Range(0, length)
            .Select(_ => chars[random.Next(chars.Length)])
            .ToArray());
    }
}
