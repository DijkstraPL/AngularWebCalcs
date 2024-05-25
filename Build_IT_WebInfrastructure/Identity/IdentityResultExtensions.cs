using Build_IT_WebApplication.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Build_IT_WebInfrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}