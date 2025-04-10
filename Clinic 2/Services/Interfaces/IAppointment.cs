public interface IAppointment
{
   Appointments GetById(int ID);
    int Add(Appointments entity);
    bool Update(Appointments entity);
    bool Delete(Appointments entity);

}
