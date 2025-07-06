
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{
    [ApiController]
    [Route("api/[Contollers]")]
    public class AppointmentsControllers : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentsControllers(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }



    }
}
