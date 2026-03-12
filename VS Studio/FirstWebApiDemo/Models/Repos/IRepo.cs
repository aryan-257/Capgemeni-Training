namespace FirstWebApiDemo.Models.Repos
{
    public interface IRepo<T>
    {
        T Get(int id);
        ICollection<T> GetAll();
        bool Add(T item);
        bool Update(int id,T item);
        bool Delete(int id);

    }
}
