public interface IAppointmentService
{
   Appointments GetById(int ID);
    int Add(Appointments entity);
    bool Update(Appointments entity);
    bool Delete(Appointments entity);

}
