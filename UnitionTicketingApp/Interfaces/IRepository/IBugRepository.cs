using UnitionTicketingApp.Entities;

namespace UnitionTicketingApp.Interfaces.IRepository
{
    public interface IBugRepository
    {
        Bug AddBug(Bug bug);
        Bug UpdateBug(Bug bug);
        bool DeleteBug(Bug bug);
        Bug GetBug(Guid id);
        IList<Bug> GetAllBugs();
    }
}
