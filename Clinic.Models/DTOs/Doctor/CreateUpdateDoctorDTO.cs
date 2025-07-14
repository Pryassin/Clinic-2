using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Models.DTOs.Person;

namespace ModelsLayer.DTOs.Doctor
{
    public class CreateUpdateDoctorDTO:CreatePersonDto
    {
        public string Specialization { get; set; }
    }
}
