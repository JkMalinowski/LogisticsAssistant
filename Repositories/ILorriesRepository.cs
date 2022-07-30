using LogisticsAssistant.Models;

namespace LogisticsAssistant.Repositories
{
    public interface ILorriesRepository
    {
        Lorries Get(int lorryId);
        IQueryable<Lorries> GetAll();
        void Add(Lorries lorry);
        void Update(int lorryId, Lorries lorry);
        void Delete(int lorryId);
    }
}
