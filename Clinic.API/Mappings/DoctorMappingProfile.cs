using AutoMapper;
using ModelsLayer.DTOs.Doctor;

namespace APILayer.Mappings
{
    public class DoctorMappingProfile:Profile
    {
        public DoctorMappingProfile()
        {
            CreateMap<CreateUpdateDoctorDTO,Doctor>();
            CreateMap<Doctor, CreateUpdateDoctorDTO>();

            CreateMap<Doctor, GetDoctorDTO>();
            CreateMap<GetDoctorDTO, Doctor>();
        }
    }
}
