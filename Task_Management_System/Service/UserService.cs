using Microsoft.EntityFrameworkCore;
using Task_Management_System.Models;
using static Task_Management_System.Tools.Tools;

namespace Task_Management_System.Service;
public interface IUsersService
{
    public Task<Users> CreateUsers(Users mpdel); 
    public Task<Users?> FindUsersOneByEmail(string email); 
    public Task<Users?> FindOneUserByActivationLink(string activationLink);
    public Task<List<Users>?> FindAllUsers(); 
    public Task<Users?> FindOneUserById(int id);
    public Task<Users?> UpdateUser(int id, Users model);
    public Task<Users?> RemoveUser(int id);
}
public class UserService : IUsersService
{
    private readonly DataBaseService context;

    public UserService(DataBaseService context)
    {
        this.context = context;
    }

    async public Task<Users> CreateUsers(Users dto)
    {
        Users user = new Users();
        user.User_Email = dto.User_Email;
        user.User_Password = dto.User_Password;
        user.User_Activation_Link = GenerateRandomString(10);
        return dto;
    }

    async public Task<Users?> FindUsersOneByEmail(string email)
    {
        var user = context.Users.FirstOrDefault(x => x.User_Email == email);
        if(user == null) return null;
        return user;
    }

    async public Task<List<Users>?> FindAllUsers()
    {
        var users = context.Users.ToList();
        if(users == null) return null;
        return users;
    }

    async public Task<Users?> FindOneUserById(int id)
    {
        var user = await context.Users.FindAsync(id);
        if(user == null) return null;
        return user;
    }

    async public Task<Users?> FindOneUserByActivationLink(string activationLink)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.User_Activation_Link == activationLink);
        if(user == null) return null;
        return user;
    }


    async public Task<Users?> UpdateUser(int id, Users model)
    {
        var user = await context.Users.FindAsync(id);
        if(user == null) return null;
        context.Entry(model).State = EntityState.Modified;
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if ((context.Users?.Any( e => e.User_Id == id)).GetValueOrDefault()) return null;
            throw;
            
        }

        return user;
    }

    public Task<Users?> RemoveUser(int id)
    {
        var user = context.Users.Find(id);
        if(user == null) return null;
        context.Users.Remove(user);
        context.SaveChanges();
        return Task.FromResult(user);
    }

    private bool UserExists(int id)
    {
        return (context.Users?.Any(e => e.User_Id == id)).GetValueOrDefault();
    }
}
