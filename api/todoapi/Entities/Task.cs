using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Task
{
    [Key]
    public int Id { get; set; }

    public string? Description { get; set; }


    [DefaultValue("Active")]
    public string? Completed { get; set; }


}
