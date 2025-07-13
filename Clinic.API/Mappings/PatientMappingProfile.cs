using AutoMapper;
using ModelsLayer.DTOs;
using ModelsLayer.DTOs.Patient;

namespace APILayer.Mappings
{
    public class PatientMappingProfile : Profile
    {
        public PatientMappingProfile ()
        {
            CreateMap<Patient, CreateUpdatePatientDTO>();
            CreateMap<CreateUpdatePatientDTO, Patient>();

        }
        
    }
}
