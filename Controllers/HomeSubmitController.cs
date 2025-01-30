using Microsoft.AspNetCore.Mvc;

[Route("homesubmit")]
public class HomeSubmitController : Controller
{
    private readonly QueueProgram.Visit.IVisitService visitService;
    private readonly QueueProgram.Visit.IValidationService validationService;

    public HomeSubmitController(QueueProgram.Visit.IVisitService visitService, QueueProgram.Visit.IValidationService validationService)
    {
        this.visitService = visitService;
        this.validationService = validationService;
    }
    
    [HttpPost]
    public IActionResult SubmitForm([FromBody] VisitRequestModel request)
    {
        var validationResult = validationService.Validate(request.SelectedOption, request.Email, request.Phone);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponseModel
            {
                ErrorMessage = validationResult.ErrorMessage,

            });
        }

        var visit = new VisitRequestModel
        {
            SelectedOption = request.SelectedOption,
            Email = request.Email,
            Phone = request.Phone
        };

        var responseModel = visitService.AddVisit(request);

        return Ok(responseModel);
    }
}
