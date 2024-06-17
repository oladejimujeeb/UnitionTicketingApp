using UnitionTicketingApp.DTO;
using UnitionTicketingApp.DTO.BugDTO;

namespace UnitionTicketingApp.Interfaces.IServices
{
    public interface IBugService
    {
        BaseResponse CreateBug(CreateBugRequestModel model);
        BaseResponse DeleteBug(Guid bugId);
        BaseResponse<BugViewModel> GetBug(Guid bugId);
        BaseResponse<IEnumerable<BugViewModel>> AllBugs();
        BaseResponse UpdateBugStatus(Guid bugId, string status);
    }
}
