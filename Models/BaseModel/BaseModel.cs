using System.ComponentModel.DataAnnotations;

namespace PhoneBook_task.Models.BaseModel
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }   
    }
}
