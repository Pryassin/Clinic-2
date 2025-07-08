using AutoMapper;
using Clinic.Models.DTOs.Person;

namespace APILayer.Mappings
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<Person, CreatePersonDto>();
            CreateMap<CreatePersonDto, Person>();

            CreateMap<GetPersonDto, Person>();
            CreateMap<Person,GetPersonDto>();
        }
    }
}
