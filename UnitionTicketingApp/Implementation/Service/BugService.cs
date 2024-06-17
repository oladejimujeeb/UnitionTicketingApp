using UnitionTicketingApp.DTO;
using UnitionTicketingApp.DTO.BugDTO;
using UnitionTicketingApp.Entities;
using UnitionTicketingApp.Interfaces.IRepository;
using UnitionTicketingApp.Interfaces.IServices;
using UnitionTicketingApp.Entities.Enums;

namespace UnitionTicketingApp.Implementation.Service
{
    public class BugService : IBugService
    {
        private readonly IBugRepository _bugRepository;
        public BugService(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        public BaseResponse<IEnumerable<BugViewModel>> AllBugs()
        {
            var response = new BaseResponse<IEnumerable<BugViewModel>>();
            var bugs = _bugRepository.GetAllBugs().Select(b => new BugViewModel
            {
                BugId = b.Id,
                BugTitle = b.BugTitle,
                BugCreatedOn = b.CreatedOn.ToString("F"),
                BugDescription = b.Description,
                BugStatus = b.BugStatus.ToString(),
                BugSummary = b.Summary,
            }).ToList();
            response.Data = bugs;
            response.Status = true;
            return response;
        }

        public BaseResponse CreateBug(CreateBugRequestModel model)
        {
            var response = new BaseResponse();
            if (model == null)
            {
                response.Message = "Failed to create bug, Invalid model";
                return response;
            }
            var bug = new Bug
            {
                BugStatus = BugStatus.Open,
                BugTitle = model.BugTitle,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Summary = model.Summary,

            };
            var saveBug = _bugRepository.AddBug(bug);
            if (saveBug != null)
            {
                response.Message = $"Bug created Successfully with Id{saveBug.Id}";
                response.Status = true;
                return response;
            }
            response.Message = "Failed to save bug";
            return response;
        }

        public BaseResponse DeleteBug(Guid bugId)
        {
            var response = new BaseResponse();
            if (Guid.Empty == bugId)
            {
                response.Message = "BugId cannot be null";
                return response;
            }
            var bug = _bugRepository.GetBug(bugId);
            if (bug is null)
            {
                response.Message = "Bug not found";
                return response;
            }
            var deleteBug = _bugRepository.DeleteBug(bug);
            if (!deleteBug)
            {
                response.Message = "Something went wrong, Cannot delete bug";
                return response;
            }

            response.Status = true;
            return response;

        }

        public BaseResponse<BugViewModel> GetBug(Guid bugId)
        {
            var response = new BaseResponse<BugViewModel>();
            if (Guid.Empty == bugId)
            {
                response.Message = "BugId cannot be null";
                return response;
            }
            var bug = _bugRepository.GetBug(bugId);
            if (bug is null)
            {
                response.Message = "Bug not found";
                return response;
            }
            var data = new BugViewModel
            {
                BugTitle = bug.BugTitle,
                BugCreatedOn = bug.CreatedOn.ToString("F"),
                BugDescription = bug.Description,
                BugStatus = bug.BugStatus.ToString(),
                BugSummary = bug.Summary,
            };
            response.Data = data;
            response.Status = true;
            return response;
        }
        public BaseResponse UpdateBugStatus(Guid bugId, string status)
        {
            var response = new BaseResponse();

            if (Guid.Empty == bugId)
            {
                response.Message = "BugId cannot be null";
                return response;
            }
            if (string.IsNullOrEmpty(status))
            {
                response.Message = "BugId cannot be null";
                return response;
            }
            var bug = _bugRepository.GetBug(bugId);
            if (bug is null)
            {
                response.Message = "Bug not found";
                return response;
            }
            switch (status) 
            {
                case "Open":
                    bug.BugStatus = BugStatus.Open;
                    break;
                case "Active":
                    bug.BugStatus = BugStatus.Active;
                    break;
                case "Close":
                    bug.BugStatus = BugStatus.Close;
                    break;
                case "Resolve":
                    bug.BugStatus = BugStatus.Resolve;
                    break;

            }
            var update = _bugRepository.UpdateBug(bug);
            if (update is null)
            {
                response.Message = "Bug status failed to upadate";
            }
            response.Status = true;
            return response;
        }
    }
}
