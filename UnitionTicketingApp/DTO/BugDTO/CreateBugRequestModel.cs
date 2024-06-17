using System.ComponentModel.DataAnnotations;
using UnitionTicketingApp.Entities.Enums;

namespace UnitionTicketingApp.DTO.BugDTO
{
    public class CreateBugRequestModel
    {
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string BugTitle { get; set; }
    }

    public class BugViewModel
    { 
        public Guid BugId { get; set; }
        public string BugTitle { get; set; }
        public string BugSummary { get; set; }
        public string BugDescription { get; set; }
        public string BugStatus { get; set; }
        public string  BugCreatedOn { get; set; }
    }

}
