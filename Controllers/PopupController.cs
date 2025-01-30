using Microsoft.AspNetCore.Mvc;
using QueueProgram.Popup;

[Route("popup")]
public class PopupController : Controller
{
    private readonly QueueProgram.Popup.IPopupService service;

    public PopupController(IPopupService service)
    {
        this.service = service;
    }

    [HttpGet]
    public QueueProgram.Popup.PopupOptions Options()
    {
        return service.GetOptions();
    }
}


