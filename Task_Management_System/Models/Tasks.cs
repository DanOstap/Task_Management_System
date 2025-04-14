using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Management_System.Models;

[Table("Tasks")]
public class Tasks
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Task_Id { get; set; }
    public string Task_Name { get; set; }
    public string Task_Description { get; set; }
    public int Task_Days_Deadline { get; set; } = 3;
    public DateTime Task_Work_Time { get; set; } = DateTime.MinValue; // 0:00:00
    public List<Users> Users { get; set; } = [];

}