using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Models.DTOs.Person;

namespace ModelsLayer.DTOs.Patient
{
    public class CreateUpdatePatientDTO : CreatePersonDto
    {
        public string EmergencyContactName { get; set; }

        public string EmergencyContactPhone { get; set; }

    }
}
