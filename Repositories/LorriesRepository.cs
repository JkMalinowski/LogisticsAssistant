using LogisticsAssistant.Data;
using LogisticsAssistant.Models;

namespace LogisticsAssistant.Repositories
{
    public class LorriesRepository : ILorriesRepository
    {
        private readonly LogisticsAssistantContext _context;

        public LorriesRepository(LogisticsAssistantContext context)
        {
            _context = context;
        }

        public void Add(Lorries lorry)
        {
            _context.Lorries.Add(lorry);
            _context.SaveChanges();
        }

        public void Delete(int lorryId)
        {
            var lorry = _context.Lorries.FirstOrDefault(l => l.Id == lorryId);
            if(lorry != null)
            {
                _context.Remove(lorry);
                _context.SaveChanges();
            }
        }

        public Lorries Get(int lorryId)
        {
            var lorry = _context.Lorries.SingleOrDefault(x => x.Id == lorryId);
            return lorry;
        }

        public IQueryable<Lorries> GetAll()
        {
            return _context.Lorries;
        }

        public void Update(int lorryId, Lorries lorry)
        {
            var result = _context.Lorries.SingleOrDefault(x => x.Id == lorryId);
            if(result != null)
            {
                result.LorryBrand = lorry.LorryBrand;
                result.MaxSpeed = lorry.MaxSpeed;
                result.BreakInMinutes = lorry.BreakInMinutes;
                result.BreakAfterRideInHours = lorry.BreakAfterRideInHours;
                _context.SaveChanges();
            }
        }
    }
}
