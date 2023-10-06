using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using complainSystem.models;
using complainSystem.models.ComplainDto;
using complainSystem.models.Complains;

namespace complainSystem.Services.ComplainService
{
    public interface IComplainService
    {
        Task<ServiceResponse<List<Complain>>> GetComplaints();
        Task<ServiceResponse<Complain>> GetComplaint(int id);
        Task<ServiceResponse<Complain>> AddComplaint(AddComplainDto complain);
        Task<ServiceResponse<Complain>> UpdateComplaint(UpdateComplainDto complain);
        Task<ServiceResponse<Complain>> DeleteComplaint(int id);  
    }
}