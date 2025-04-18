using Task_Management_System.Models;

namespace Task_Management_System.Service;

public interface ITaskService
{
    public Task<Tasks> CreateTask(Tasks task);
    public Task<Tasks> UpdateTask(Tasks task);
    public Task<Tasks> DeleteTask(Tasks task);
    public Task<Tasks> GetTaskById(int id);
    public Task<List<Tasks>> GetAllTasks();
    public Task<List<Tasks>> GetTasksByUser(int userId);
    public Task<List<Tasks>> GetAllTasksByUser(int userId);
    public Task<Tasks> AddUserToTask(int userId, Tasks task);
    public Task<Tasks> RemoveUserFromTask(int userId, Tasks task);
    
}

public class TaskService
{
    private readonly DataBaseService context;

    public TaskService(DataBaseService context)
    {
        this.context = context;
    }

    async public Task<Tasks> CreateTask(Tasks task, int userId)
    {
        Users user = context.Users.Find(userId);
        if(user == null) return null;
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
}