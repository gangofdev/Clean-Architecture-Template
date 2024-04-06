namespace CleanArc.WebFramework.WebExtensions
{
    public static class ActionResultExtensions
    {
        public static IActionResult ToActionResult<TModel>(this OperationResult<TModel> result)
        {
            ArgumentNullException.ThrowIfNull(result, nameof(OperationResult<TModel>));

            if (result.IsSuccess) return result.Result is bool ? new OkResult() : new OkObjectResult(result.Result);

            if (result.IsNotFound) return string.IsNullOrEmpty(result.ErrorMessage) ? new NotFoundResult() : new NotFoundObjectResult(result.ErrorMessage);

            return string.IsNullOrEmpty(result.ErrorMessage) ? new BadRequestObjectResult("Invalid Parameters. Please try again") : new BadRequestObjectResult(result.ErrorMessage);
        }
    }
}
