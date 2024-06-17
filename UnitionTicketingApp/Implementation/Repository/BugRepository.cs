using UnitionTicketingApp.Context;
using UnitionTicketingApp.Entities;
using UnitionTicketingApp.Interfaces.IRepository;

namespace UnitionTicketingApp.Implementation.Repository
{
    public class BugRepository : IBugRepository
    {
        private readonly ApplicationContext _context;
        public BugRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Bug AddBug(Bug bug)
        {
            _context.Add(bug);
            _context.SaveChanges();
            return bug;
        }

        public bool DeleteBug(Bug bug)
        {
            _context.Remove(bug);
            _context.SaveChanges();
            return true;
        }

        public IList<Bug>GetAllBugs()
        {
            var bugs = _context.Bugs.ToList();
            return bugs;
        }

        public Bug GetBug(Guid id)
        {
            var bug = _context.Bugs.SingleOrDefault(b=>b.Id==id) ;
            return bug;
        }

        public Bug UpdateBug(Bug bug)
        {
            _context.Update(bug);
            _context.SaveChanges();
            return bug;
        }
    }
}
