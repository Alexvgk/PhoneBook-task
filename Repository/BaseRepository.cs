using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhoneBook_task.DTO;
using PhoneBook_task.Models;
using PhoneBook_task.Models.BaseModel;
using PhoneBook_task.Services;
using System;

namespace PhoneBook_task.Repository
{
    public class ContactRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
    {
        private ContactDbContext Context { get; set; }
        private IMapper Mapper { get; set; }
        public ContactRepository(ContactDbContext context, IMapper mapper)
        {
            this.Mapper = mapper;
            Context = context;
        }

        public async Task<bool> CreateContact(DtoContact contact)
        {
            try
            {
                var cont = Mapper.Map<TDbModel>(contact);
                //Context.Set<TDbModel>().Add(cont);
                //await Context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, логирование
                return false;
            }
        }

        public Task<bool> DeleteContact(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TDbModel> getData()
        {
            throw new NotImplementedException();
        }

        public Task<TDbModel> GetDataById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateContact(int id, DtoContact update)
        {
            throw new NotImplementedException();
        }

    }
}
