using Application.Models.Responses;
using Application.Models.Responses.Base;
using Microsoft.AspNetCore.Mvc;

namespace Unicam.Paradigmi._118301.Restaurant.Web.Results
{
    public class BadRequestResultFactory : BadRequestObjectResult
    {
        public BadRequestResultFactory(ActionContext context) : base(new BadResponse())
        {
            var listOfErrors = new List<string>();
            foreach (var key in context.ModelState)
            {
                var errors = key.Value.Errors;
                for (var i = 0; i < errors.Count(); i++)
                {
                    listOfErrors.Add(errors[0].ErrorMessage);
                }
            }

            var response = (BadResponse)Value;
            response.Errors = listOfErrors;
        }
    }
}
