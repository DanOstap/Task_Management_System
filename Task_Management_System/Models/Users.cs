using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Management_System.Models;

[Table("Users")]
public class Users
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public static int User_Id { get; set; }

    public string User_Name { get; set; } = $"user_{User_Id}";
    public string User_Email { get; set; }
    public string User_Password { get; set; }
    public string User_Date_Created { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
    public string User_Activation_Link { get; set; }
    public List<Tasks> Tasks { get; set; } = [];
}