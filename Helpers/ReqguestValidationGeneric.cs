using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using complainSystem.models;
using FluentValidation;
using FluentValidation.Results;

namespace complainSystem.Validations
{
    public class ReqguestValidationGeneric<T>
    {
        public List<string> ValidationMessages { get; set; } = new List<string>();
        public ServiceResponse<T>? serviceResponse = new ServiceResponse<T>();
        public bool IsValid;
        public List<ValidationFailure> Errors;
        public T? Data;


        public ReqguestValidationGeneric(bool isValid, T? data, List<ValidationFailure> validationErrors)
        {
            IsValid = isValid;
            serviceResponse.Data = data;
            Errors = validationErrors;
            data = Data;



            serviceResponse.Message = "Bad Request";
            foreach (ValidationFailure failure in Errors)
            {
                ValidationMessages.Add(failure.ErrorMessage);
            }
            serviceResponse.ValidationMessages = ValidationMessages;
            serviceResponse.Data = data;
        }
    }

}
