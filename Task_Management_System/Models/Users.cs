using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Management_System.Models;

[Table("Users")]
public class Users
{
    public Users()
    {
        User_Name = $"user_ +{this.User_Id}";
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public  int User_Id { get; set; }

    public string User_Name { get; set; }
    public string User_Email { get; set; }
    public string User_Password { get; set; }
    public string User_Date_Created { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
    public string User_Activation_Link { get; set; }
    public List<Tasks> Tasks { get; set; } = [];
}