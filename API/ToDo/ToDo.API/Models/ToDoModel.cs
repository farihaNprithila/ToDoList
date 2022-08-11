using System.ComponentModel.DataAnnotations;

namespace ToDo.API.Models
{
    public class ToDoModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TaskName { get; set; }

        [Required]
        public string Priority { get; set; }
    }
}
