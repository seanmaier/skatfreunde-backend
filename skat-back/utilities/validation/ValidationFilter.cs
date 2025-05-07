using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace skat_back.utilities.validation;

public class ValidationFilter(IServiceProvider serviceProvider) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        
        foreach (var argument in context.ActionArguments)
        {
            var argumentType = argument.Value?.GetType();
            if (argumentType == null) continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);

            if (serviceProvider.GetService(validatorType) is not IValidator validator) continue;

            if (argument.Value == null) continue;
            var validationContext = new ValidationContext<object>(argument.Value);
            var validationResult = await validator.ValidateAsync(validationContext);

            if (validationResult.IsValid) continue;
            context.Result = new BadRequestObjectResult(validationResult.Errors);
            return;
        }

        await next();
    }
}