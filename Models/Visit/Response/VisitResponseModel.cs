public class VisitResponseModel
{

    public string Message { get; set; }
    public Guid VisitId { get; set; }
    public int PlaceInQueue { get; set; }
    public string SelectedOption { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public VisitResponseModel() { }

    public VisitResponseModel(int PlaceInQueue, string SelectedOption)
    {
        this.PlaceInQueue = PlaceInQueue;
        this.SelectedOption = SelectedOption;
    }
    public VisitResponseModel(Guid VisitId, int PlaceInQueue, string SelectedOption, string Email, string Phone)
    {
        this.VisitId = VisitId;
        this.PlaceInQueue = PlaceInQueue;
        this.SelectedOption = SelectedOption;
        this.Email = Email;
        this.Phone = Phone;
    }
    public VisitResponseModel(string Message, int PlaceInQueue, string SelectedOption, string Email, string Phone)
    {
        this.Message = Message;
        this.PlaceInQueue = PlaceInQueue;
        this.SelectedOption = SelectedOption;
        this.Email = Email;
        this.Phone = Phone;
    }
    public VisitResponseModel(string Message, Guid VisitId, int PlaceInQueue, string SelectedOption, string Email, string Phone)
    {
        this.Message = Message;
        this.VisitId = VisitId;
        this.PlaceInQueue = PlaceInQueue;
        this.SelectedOption = SelectedOption;
        this.Email = Email;
        this.Phone = Phone;
    }
}
