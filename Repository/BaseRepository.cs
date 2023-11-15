using Abp.Domain.Uow;
using Abp.Threading.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhoneBook_task.DTO;
using PhoneBook_task.Models;
using PhoneBook_task.Models.BaseModel;
using PhoneBook_task.Services;
using System;

namespace PhoneBook_task.Repository
{
    public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
    {
        private readonly ContactDbContext _context;
        private IMapper _mapper; 
        public BaseRepository(ContactDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> CreateContact(DtoContact contact)
        {
            try
            {
                var cont = _mapper.Map<TDbModel>(contact);
                await _context.AddAsync(cont);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;

            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex}");
            }
        }


        public async Task<bool> DeleteContact(int id)
        {
            try
            {
                var contact = await _context.Contacts.FindAsync(id);

                if (contact == null)
                {
                    throw new InvalidOperationException($"Record with id {id} not found.");
                }

                _context.Contacts.Remove(contact);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (InvalidOperationException ex)
            {
                throw ex ;
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<TDbModel>> getData()
        {
            try
            {
                var data = await _context.Set<TDbModel>().ToListAsync();
                return data;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex}");
            }
        }

        public async Task<TDbModel> GetDataById(int id)
        {
            try
            {

                var contact = await _context.Set<TDbModel>().FindAsync(id);

                if (contact == null)
                {
                    throw new ArgumentNullException($"No contact with id {id}");
                }

                return contact;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex}");
            }
        }

        public async Task<bool> UpdateContact(int id, DtoContact update)
        {
            try
            {
                var contact = await _context.Set<TDbModel>().FindAsync(id);

                if (contact == null)
                {
                    return false;
                }

                contact = _mapper.Map(update, contact);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex}");
            }
        }

    }
}
