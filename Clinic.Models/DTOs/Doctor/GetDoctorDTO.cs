using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Models.DTOs.Person;

namespace ModelsLayer.DTOs.Doctor
{
    public class GetDoctorDTO
    {
        public int PersonID { get;set;}

        public int DoctorID { get; set; }
 
        public string Specialization { get; set; }

    }
}
