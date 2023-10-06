using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using complainSystem.models;
using complainSystem.models.ComplainDto;
using complainSystem.models.Complains;
using ComplainSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace complainSystem.Services.ComplainService
{
    public class ComplainService : IComplainService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ComplainService(DataContext context, IMapper mappingDtos)
        {
            _mapper = mappingDtos;
            _context = context;
        }

        public async Task<ServiceResponse<Complain>> AddComplaint(AddComplainDto PostedComplaint)
        {
            ServiceResponse<Complain> serviceResponse = new ServiceResponse<Complain>();
            Complain complaint = _mapper.Map<Complain>(PostedComplaint);

            try
            {

                if (PostedComplaint.CategoryId == null || PostedComplaint.CategoryId == 0)
                {
                    serviceResponse.Message = "Please Select a Category";
                    serviceResponse.Success = false;
                    serviceResponse.StatusCode = 400;


                }
                else
                {
                    await _context.Complains.AddAsync(complaint);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = complaint;
                    complaint.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == complaint.CategoryId);
                    serviceResponse.Message = "Complain added successfully";
                    serviceResponse.Success = true;
                    serviceResponse.StatusCode = 200;

                }

            }
            catch (Exception e)
            {
                serviceResponse.Message = "Server Error : " + e.Message;
                serviceResponse.Success = false;
                serviceResponse.StatusCode = 500;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Complain>> DeleteComplaint(int id)
        {
            ServiceResponse<Complain> serviceResponse = new ServiceResponse<Complain>();

            try
            {
                Complain? complaint = await _context.Complains.FirstOrDefaultAsync(c => c.Id == id);
                if (complaint != null)
                {
                    _context.Complains.Remove(complaint);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = complaint;
                    serviceResponse.Message = "Complaint deleted successfully";
                    serviceResponse.Success = true;
                    serviceResponse.StatusCode = 200;
                }
                else
                {
                    serviceResponse.Data = complaint;
                    serviceResponse.Message = "Complaint not found";
                    serviceResponse.Success = false;
                    serviceResponse.StatusCode = 404;
                }
            }
            catch (Exception e)
            {
                serviceResponse.Message = "Server Error : " + e.Message;
                serviceResponse.Success = false;
                serviceResponse.StatusCode = 500;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Complain>> GetComplaint(int id)
        {
            ServiceResponse<Complain> serviceResponse = new ServiceResponse<Complain>();

            try
            {
                serviceResponse.Data = await _context.Complains.Where(c => c.Id == id)
                              .Include(p => p.Category)
                              .FirstOrDefaultAsync();

                if (serviceResponse.Data == null)
                {
                    serviceResponse.Message = "No data found";
                    serviceResponse.Success = false;
                    serviceResponse.StatusCode = 404;
                }

                else
                {
                    serviceResponse.Message = "Data found";
                    serviceResponse.Success = true;
                    serviceResponse.StatusCode = 200;
                }

            }
            catch (Exception e)
            {
                serviceResponse.Message = "Server Error : " + e.Message;
                serviceResponse.Success = false;
                serviceResponse.StatusCode = 500;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Complain>>> GetComplaints()
        {

            ServiceResponse<List<Complain>> serviceResponse = new ServiceResponse<List<Complain>>();

            try
            {
                serviceResponse.Data = await _context.Complains.ToListAsync();

                if (serviceResponse.Data == null)
                {
                    serviceResponse.Message = "No data found";
                    serviceResponse.Success = false;
                    serviceResponse.StatusCode = 404;
                }

                else
                {
                    serviceResponse.Message = "Data found";
                    serviceResponse.Success = true;
                    serviceResponse.StatusCode = 200;
                }

            }
            catch (Exception e)
            {
                serviceResponse.Message = "Server Error : " + e.Message;
                serviceResponse.Success = false;
                serviceResponse.StatusCode = 500;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Complain>> UpdateComplaint(UpdateComplainDto updatedComplaint)
        {
            ServiceResponse<Complain> serviceResponse = new ServiceResponse<Complain>();

            try
            {
                Complain? complainToUpdate = await _context.Complains.FirstOrDefaultAsync(c => c.Id == updatedComplaint.Id);
                if (complainToUpdate != null)
                {
                    complainToUpdate.ComplainTitle = updatedComplaint.ComplainTitle;
                    complainToUpdate.ComplainDescription = updatedComplaint.ComplainDescription;
                    complainToUpdate.CategoryId = updatedComplaint.CategoryId;
                    complainToUpdate.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == updatedComplaint.CategoryId);
                    if ( complainToUpdate.Category == null || complainToUpdate.CategoryId == 0 )
                    {
                        serviceResponse.Message = "Please Select a Category";
                        serviceResponse.Success = false;
                        serviceResponse.StatusCode = 400;
                    }
                    else
                    {
                        await _context.SaveChangesAsync();
                        serviceResponse.Data = complainToUpdate;
                        serviceResponse.Message = "Complaint updated successfully";
                        serviceResponse.Success = true;
                        serviceResponse.StatusCode = 200;

                    }

                }
                else
                {
                    serviceResponse.Data = complainToUpdate;
                    serviceResponse.Message = "Complaint not found";
                    serviceResponse.Success = false;
                    serviceResponse.StatusCode = 404;
                }
            }
            catch (Exception e)
            {
                serviceResponse.Message = "Server Error : " + e.Message;
                serviceResponse.Success = false;
                serviceResponse.StatusCode = 500;

            }
            return serviceResponse;

        }
    }
}