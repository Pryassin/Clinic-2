public interface IBaseRepositoryService<T>
{
    T GetByName(string Name);
    T GetById(int ID);
    int Add();
    bool Update();
    bool Delete();
}

