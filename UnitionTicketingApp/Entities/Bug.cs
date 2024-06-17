using UnitionTicketingApp.Entities.Enums;

namespace UnitionTicketingApp.Entities
{
    public class Bug:BaseEntity
    {
        public string BugTitle {  get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public BugStatus BugStatus { get; set; }= BugStatus.Open;
        public DateTime CreatedOn { get; set; } 
        public Guid CreatedBy { get; set; }
    }
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}
