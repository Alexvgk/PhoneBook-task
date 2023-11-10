using AutoMapper;
using PhoneBook_task.DTO;
using PhoneBook_task.Models;
using System;

namespace PhoneBook_task.Services
{
    public class ContactsMappingProfile : Profile
    {
        public ContactsMappingProfile()
        {
            CreateMap<DtoContact, Contact>();
        }
    }
}
