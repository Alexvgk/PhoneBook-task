using PhoneBook_task.DTO;
using PhoneBook_task.Models.BaseModel;

namespace PhoneBook_task.Repository
{
    public interface IBaseRepository<Model> where Model : BaseModel
    {
        Task<IEnumerable<Model>> getData();
        Task<Model> GetDataById(int id);
        Task<bool> CreateContact(DtoContact contact);
        Task<bool> UpdateContact(int id, DtoContact updateContact);
        Task<bool> DeleteContact(int id);
    }
}
