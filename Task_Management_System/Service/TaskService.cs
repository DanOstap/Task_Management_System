using Microsoft.EntityFrameworkCore;
using Task_Management_System.Models;
using static Task_Management_System.Tools.Tools;

namespace Task_Management_System.Service;

public interface ITaskService
{
    public Task<Tasks> CreateTask(Tasks model);
    public Task<Tasks> UpdateTask(int id,Tasks model);
    public Task<Tasks> DeleteTask(Tasks model);
    public Task<Tasks> GetTaskById(int id);
    public Task<List<Tasks>> GetAllTasks();
    public Task<List<Tasks>> GetTasksByUser(int userId);
    public Task<Tasks> AddUserToTask(int userId, int taskId);
    public Task<Tasks> RemoveUserFromTask(int userId, Tasks model);
    
}

public class TaskService
{
    private readonly DataBaseService context;

    public TaskService(DataBaseService context)
    {
        this.context = context;
    }

    async public Task<Tasks?> CreateTask(Tasks task, int userId)
    {
        Users user = context.Users.Find(userId);
        if (user == null) return null;
        return new Tasks
        {
            Task_Name = task.Task_Name,
            Task_Description = task.Task_Description,
            Task_Days_Deadline = task.Task_Days_Deadline,
            Task_Work_Time = task.Task_Work_Time,
            Users = new List<Users>() { user }
        };
/*
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        return task;
*/
    }

    async public Task<Tasks> UpdateTask(int id, Tasks model)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task == null) return null;
        context.Entry(model).State = EntityState.Modified;
        try
        {
        }
        catch (DbUpdateConcurrencyException)
        {
            if ((context.Tasks?.Any(t => t.Task_Id == id)).GetValueOrDefault()) return null;
            throw;
        }

        return task;
    }

    public Task<Tasks> DeleteTask(int id)
    {
        var task = context.Tasks.Find(id);
        if (task == null) return null;
        context.Tasks.Remove(task);
        context.SaveChanges();
        return Task.FromResult(task);
    }

    async public Task<Tasks> GetTaskById(int id)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task == null) return null;
        return task;
    }

    async public Task<List<Tasks>> GetAllTasks()
    {
        var tasks = await context.Tasks.ToListAsync();
        return await Task.FromResult(tasks);
    }

    async public Task<List<Tasks>> GetTasksByUser(int userId)
    {
        var tasks = await context.Tasks.Where(t => t.Users.Any(u => u.User_Id == userId)).ToListAsync();
        return tasks;
    }

    async public Task<Tasks> AddUserToTask(int userId, int taskId)
    {
        var user = await context.Users.FindAsync(userId);
        if (user == null) return null;
        var task = await context.Tasks.FindAsync(taskId);
        if (task == null) return null;
        task.Users.Add(user);
        return task;
    }
}