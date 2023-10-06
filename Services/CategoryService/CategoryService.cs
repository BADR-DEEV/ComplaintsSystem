

using System.Net;
using complainSystem.models;
using ComplainSystem.Data;
using ComplainSystem.models;
using Microsoft.EntityFrameworkCore;

namespace ComplainSystem.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        public CategoryService(DataContext context)
        {
            _context = context;
        }


        public async Task<ServiceResponse<List<Category>>> GetAllCategories()
        {

            ServiceResponse<List<Category>> serviceResponse = new ServiceResponse<List<Category>>();

            try
            {
                serviceResponse.Data = await _context.Categories.ToListAsync();

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
    }
}
