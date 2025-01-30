using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class VisitController : Controller
{

    private readonly QueueProgram.Visit.IVisitService visitService;
    private readonly QueueProgram.Admin.IAutorisationService autorisationService;
    private readonly ILogger<VisitController> _logger;

    public VisitController(QueueProgram.Visit.IVisitService visitService, QueueProgram.Admin.IAutorisationService autorisationService, ILogger<VisitController> logger)
    {
        this.visitService = visitService;
        this.autorisationService = autorisationService;
        _logger = logger;
    }
    
    //HomeSubmit/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteVisit(Guid id)
    {
        try
        {
            visitService.DeleteVisitById(id);
            return Ok(new { Message = "Visit successfully deleted" });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { Error = "Visit not found" });
        }
    }

    //getVisitInfo/{id}
    [HttpGet("getVisitInfo/{id}")]
    public IActionResult GetVisitInformation(Guid id)
    {
        var visitInformation = visitService.GetVisitInformation(id);
        if (visitInformation != null)
        {

            return Ok(visitInformation);
        }
        return NotFound(new { Message = "Visit not found" });

     }

    //frontVisitInfo/{id}
    [HttpGet("frontVisitInfo/{id}")]
    public IActionResult FrontVisitInfo(Guid id)
    {
        var visitInformation = visitService.GetVisitInformation(id);
        if (visitInformation == null)
        {
            return NotFound(new { Message = "Visit not found" });
        }

        var response = new VisitResponseModel
        {
            PlaceInQueue = visitInformation.PlaceInQueue,
            SelectedOption = visitInformation.SelectedOption
        };
        return Ok(response);
    }
    
    [HttpGet("quantity/{option}")]
    public IActionResult GetQueueQuantityByOption(string option)
    {
        var quantity = visitService.GetQueueQuantityByOption(option);

        if (quantity == null)
        {
            return NotFound(new { message = "Option queue not found or empty" });
        }
        
        return Ok(new { quantity });
    }

    //deleteFirst/{option}
    [HttpDelete("deleteFirst/{option}")]
    public IActionResult DeleteFirstVisitInOption([FromBody] QueueProgram.Models.Admin.LoginModel login, string option)
    {
        if (!autorisationService.AutorisationValidation(login.Username, login.Password, option))
        {
            return Unauthorized("Invalid credentials for this option");
        }

        bool removed = visitService.DeleteFirstVisitInOption(option);
        if (removed)
        {
            return Ok("First person from queue sucessfuly removed");
        }

        return NotFound("Visit not found or queue is empty");
    }
}
