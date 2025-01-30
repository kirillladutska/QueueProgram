using System.ComponentModel.DataAnnotations;

public class VisitList
{
    [Key]
    public Guid VisitId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string SelectedOption { get; set; }
    public int PlaceInQueue { get; set; }

    public VisitList() 
    {
        VisitId = Guid.NewGuid();
    }

    public VisitList(string selectedOption, string email, string phone, int placeInQueue)
    {
        VisitId = Guid.NewGuid();
        SelectedOption = selectedOption;
        Email = email;
        Phone = phone;
        PlaceInQueue = placeInQueue;
    }
}