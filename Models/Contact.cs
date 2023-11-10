namespace PhoneBook_task.Models
{
    public class Contact : BaseModel.BaseModel
    {
        public string? Name { get; set;}
        public string? MobilePhone { get; set;}
        public string? JobTitle { get; set;}
        public string? Data { get; set;}
    }
}
