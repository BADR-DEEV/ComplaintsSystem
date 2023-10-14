using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using AutoMapper;
using complainSystem.models;
using complainSystem.models.ComplainDto;
using complainSystem.models.Complains;
using complainSystem.models.Users;
using complainSystem.Validations;
using ComplainSystem.Data;
using ComplainSystem.models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace complainSystem.Services.ComplainService
{
    public class ComplainService : IComplainService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserManager<User> _userManager;

        public ComplainService(DataContext context, IMapper mappingDtos, IHttpContextAccessor httpContextAccessor, UserManager<User> usermanager)
        {
            _mapper = mappingDtos;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = usermanager;

        }
        private string GetUserId()
        {
            return _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email) ?? "mew";
        }

        public async Task<ServiceResponse<Complain>> AddComplaint(AddComplainDto PostedComplaint)
        {
            ServiceResponse<Complain> serviceResponse = new ServiceResponse<Complain>();
            Complain complaint = _mapper.Map<Complain>(PostedComplaint);


            //Validating the request before adding the complaint
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            var validationResult = new AddComplainDtoValidation().Validate(complaint);
            ReqguestValidationGeneric<Complain> req = new(validationResult.IsValid, complaint, validationResult.Errors);
            if (!validationResult.IsValid)
            {
                req.serviceResponse.StatusCode = 400;
                req.serviceResponse.Success = false;
                return req.serviceResponse;
            }
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            try
            {


                var userId = GetUserId();






                complaint.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == PostedComplaint.CategoryId);


                if (complaint.Category == null || complaint.CategoryId == 0)
                {
                    serviceResponse.Message = "Please Select a Category";
                    serviceResponse.Success = false;
                    serviceResponse.StatusCode = 400;
                    return serviceResponse;
                }
                var user = await _userManager.FindByEmailAsync(userId) ?? throw new Exception("User not found");
                var foundUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);

                complaint.PersonUser = foundUser;
                _context.Users.FirstOrDefault(u => u.Id == user.Id).Complaints.Add(complaint);
                await _context.Complains.AddAsync(complaint);
                await _context.SaveChangesAsync();
                serviceResponse.Data = complaint;
                serviceResponse.Message = "Complain added successfully ";
                serviceResponse.Success = true;
                serviceResponse.StatusCode = 200;



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
                var x = await _userManager.FindByEmailAsync(GetUserId()) ?? throw new Exception("User not found");
                serviceResponse.Data = await _context.Complains.Include(c => c.Category).ToListAsync();

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

            //Validating the request before updating the complaint
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            var complain = _mapper.Map<Complain>(updatedComplaint);
            var validationResult = new UpdateComplainDtoValidation().Validate(updatedComplaint);
            var IsValid = validationResult.IsValid;
            var Errors = validationResult.Errors;

            ReqguestValidationGeneric<Complain> req = new(IsValid, complain, Errors);
            if (!IsValid)
            {
                req.serviceResponse.StatusCode = 400;
                req.serviceResponse.Success = false;
                return req.serviceResponse;
            }
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------





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
                    if (complainToUpdate.Category == null || complainToUpdate.CategoryId == 0)
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