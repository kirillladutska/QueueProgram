// namespace QueueProgram.Visit
// {
//     public interface IVisitService
//     { VisitResponseModel AddVisit(VisitRequestModel model);
//         void DeleteVisitById(Guid id);
//         VisitResponseModel GetVisitInformation(Guid id);
//         List<VisitResponseModel> GetAllVisits();
//         int? GetQueueQuantityByOption(string option);
//         bool DeleteFirstVisitInOption(string option);
//     }
//     
//     public class VisitService: IVisitService
//     {
//         private readonly Dictionary<string, List<VisitList>> _visits = new Dictionary<string, List<VisitList>>();
//         public VisitResponseModel AddVisit(VisitRequestModel model)
//         {
//             var visit = new VisitList(model.SelectedOption, model.Email, model.Phone,
//                 GetNextPlaceInQueue(model.SelectedOption));
//             if (!_visits.ContainsKey(model.SelectedOption))
//             {
//                 _visits[model.SelectedOption] = new List<VisitList>();
//             }
//
//             _visits[model.SelectedOption].Add(visit);
//             var response = new VisitResponseModel(Message: "Visit successfully added",
//                 VisitId: visit.GetVisitId(),
//                 PlaceInQueue: visit.PlaceInQueue,
//                 SelectedOption: visit.GetSelectedOption(),
//                 Email: visit.GetEmail(),
//                 Phone: visit.GetPhone());
//             return response;
//         }
//
//         public void DeleteVisitById(Guid id)
//         {
//             foreach (var option in _visits.Keys.ToList())
//             {
//                 var visitToRemove = _visits[option].FirstOrDefault(v => v.GetVisitId() == id);
//                 if (visitToRemove != null)
//                 {
//                     _visits[option].Remove(visitToRemove);
//                     UpdateQueueByOption(option);
//                     break;
//                 }
//             }
//         }
//
//         public VisitResponseModel GetVisitInformation(Guid id)
//         {
//             foreach (var option in _visits.Values)
//             {
//                 var visit = option.FirstOrDefault(v => v.GetVisitId() == id);
//                 if (visit != null)
//                 {
//                     var response =  new VisitResponseModel(Message: "Visit found",
//                         VisitId: visit.GetVisitId(),
//                         PlaceInQueue: visit.PlaceInQueue,
//                         SelectedOption: visit.GetSelectedOption(),
//                         Email: visit.GetEmail(),
//                         Phone: visit.GetPhone());
//                     return response;
//                 }
//             }
//             return null;
//         }
//
//         private void UpdateQueueByOption(string option)
//         {
//             if (_visits.ContainsKey(option))
//             {
//                 var queue = _visits[option];
//                 for (int i = 0; i < queue.Count; i++)
//                 {
//                     queue[i].PlaceInQueue = i + 1;
//                 }
//             }
//         }
//
//         private int GetNextPlaceInQueue(string option)
//         {
//             int place;
//             if (!_visits.ContainsKey(option))
//             {
//                 place = 1;
//                 return place;
//             }
//             
//             place = _visits[option].Count + 1;
//             return place;
//         }
//
//         public List<VisitResponseModel> GetAllVisits()
//         {
//             var allVisits = new List<VisitResponseModel>();
//             foreach (var visitList in _visits.Values)
//             {
//                 foreach (var visit in visitList)
//                 {
//                     var response = new VisitResponseModel
//                     {
//                         PlaceInQueue = visit.PlaceInQueue,
//                         VisitId = visit.GetVisitId(),
//                         SelectedOption = visit.GetSelectedOption(),
//                         Email = visit.GetEmail(),
//                         Phone = visit.GetPhone()
//                     };
//                     allVisits.Add(response);
//                 }
//             }
//
//             var result = allVisits;
//             return result;
//         }
//
//         public int? GetQueueQuantityByOption(string option)
//         {
//             if (!_visits.ContainsKey(option))
//             {
//                 var result = (int?)null;
//                 return result;
//             }
//
//             var quantity = _visits[option].Count;
//             return quantity;
//         }
//
//         public bool DeleteFirstVisitInOption(string option)
//         {
//             bool result;
//             
//             if (!_visits.ContainsKey(option) || !_visits[option].Any())
//             {
//                 result = false;
//                 return result;
//             }
//
//             _visits[option].RemoveAt(0);
//             UpdateQueueByOption(option);
//             
//             result = true;
//             return result;
//         }
//     }
// }


using QueueProgram.Data;
using Microsoft.EntityFrameworkCore;

namespace QueueProgram.Visit
{
    public interface IVisitService
    { VisitResponseModel AddVisit(VisitRequestModel model);
        void DeleteVisitById(Guid id);
        VisitResponseModel GetVisitInformation(Guid id);
        List<VisitResponseModel> GetAllVisits();
        int? GetQueueQuantityByOption(string option);
        bool DeleteFirstVisitInOption(string option);
    }
    
    public class VisitService: IVisitService
    {
        private readonly ApplicationDbContext _context;
        public VisitService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        //private readonly Dictionary<string, List<VisitList>> _visits = new Dictionary<string, List<VisitList>>();
    //     public VisitResponseModel AddVisit(VisitRequestModel model)
    //     {
    //         var visit = new VisitList(model.SelectedOption, model.Email, model.Phone,
    //             GetNextPlaceInQueue(model.SelectedOption));
    //         
    //         
    //         // if (!_context.ContainsKey(model.SelectedOption))
    //         // {
    //         //     _context[model.SelectedOption] = new List<VisitList>();
    //         // }
    //         
    //         
    //         //_context[model.SelectedOption].Add(visit);
    //         var response = new VisitResponseModel(Message: "Visit successfully added",
    //             VisitId: visit.VisitId(),
    //             PlaceInQueue: visit.PlaceInQueue,
    //             SelectedOption: visit.SelectedOption(),
    //             Email: visit.Email(),
    //             Phone: visit.Phone());
    //         _context.Visits.Add(visit);
    //         _context.SaveChanges();
    //         return response;
    //         
    //     }
    //
    //     public void DeleteVisitById(Guid id)
    //     {
    //         var visit = _context.Visits
    //             .FirstOrDefault(v => v.VisitId() == id);
    //
    //         if (visit != null)
    //         {
    //             _context.Visits.Remove(visit);
    //             _context.SaveChanges();
    //             UpdateQueueForOption(visit.SelectedOption());
    //         }
    //     }
    //
    //     public VisitResponseModel GetVisitInformation(Guid id)
    //     {
    //         var visit = _context.Visits
    //             .FirstOrDefault(v => v.VisitId() == id);
    //
    //         if (visit == null) return null;
    //
    //         return new VisitResponseModel(
    //             Message: "Visit found",
    //             VisitId: visit.VisitId(),
    //             PlaceInQueue: visit.PlaceInQueue,
    //             SelectedOption: visit.SelectedOption(),
    //             Email: visit.Email(),
    //             Phone: visit.Phone()
    //         );
    //     }
    //
    //     public List<VisitResponseModel> GetAllVisits()
    //     {
    //         return _context.Visits
    //             .Select(visit => new VisitResponseModel
    //             {
    //                 PlaceInQueue = visit.PlaceInQueue,
    //                 VisitId = visit.VisitId(),
    //                 SelectedOption = visit.SelectedOption(),
    //                 Email = visit.Email(),
    //                 Phone = visit.Phone()
    //             })
    //             .ToList();
    //     }
    //
    //     public int? GetQueueQuantityByOption(string option)
    //     {
    //         return _context.Visits
    //             .Count(v => v.SelectedOption() == option);
    //     }
    //
    //     public bool DeleteFirstVisitInOption(string option)
    //     {
    //         var firstVisit = _context.Visits
    //             .Where(v => v.SelectedOption() == option)
    //             .OrderBy(v => v.PlaceInQueue)
    //             .FirstOrDefault();
    //
    //         if (firstVisit == null) return false;
    //
    //         _context.Visits.Remove(firstVisit);
    //         _context.SaveChanges();
    //
    //         UpdateQueueForOption(option);
    //         return true;
    //     }
    //
    //     private void UpdateQueueForOption(string option)
    //     {
    //         var visits = _context.Visits
    //             .Where(v => v.SelectedOption() == option)
    //             .OrderBy(v => v.PlaceInQueue)
    //             .ToList();
    //
    //         for (int i = 0; i < visits.Count; i++)
    //         {
    //             visits[i].PlaceInQueue = i + 1;
    //         }
    //
    //         _context.SaveChanges();
    //     }
    //
    //     private int GetNextPlaceInQueue(string option)
    //     {
    //         return _context.Visits
    //             .Count(v => v.SelectedOption() == option) + 1;
    //     }
    public VisitResponseModel AddVisit(VisitRequestModel model)
{
    var visit = new VisitList(model.SelectedOption, model.Email, model.Phone,
        GetNextPlaceInQueue(model.SelectedOption));
    
    var response = new VisitResponseModel(
        Message: "Visit successfully added",
        VisitId: visit.VisitId,
        PlaceInQueue: visit.PlaceInQueue,
        SelectedOption: visit.SelectedOption,
        Email: visit.Email,
        Phone: visit.Phone);
    
    _context.Visits.Add(visit);
    _context.SaveChanges();
    return response;
}

public void DeleteVisitById(Guid id)
{
    var visit = _context.Visits
        .FirstOrDefault(v => v.VisitId == id);

    if (visit != null)
    {
        _context.Visits.Remove(visit);
        _context.SaveChanges();
        UpdateQueueForOption(visit.SelectedOption);
    }
}

public VisitResponseModel GetVisitInformation(Guid id)
{
    var visit = _context.Visits
        .FirstOrDefault(v => v.VisitId == id);

    if (visit == null) return null;

    return new VisitResponseModel(
        Message: "Visit found",
        VisitId: visit.VisitId,
        PlaceInQueue: visit.PlaceInQueue,
        SelectedOption: visit.SelectedOption,
        Email: visit.Email,
        Phone: visit.Phone
    );
}

public List<VisitResponseModel> GetAllVisits()
{
    return _context.Visits
        .Select(visit => new VisitResponseModel
        {
            PlaceInQueue = visit.PlaceInQueue,
            VisitId = visit.VisitId,
            SelectedOption = visit.SelectedOption,
            Email = visit.Email,
            Phone = visit.Phone
        })
        .ToList();
}

public int? GetQueueQuantityByOption(string option)
{
    return _context.Visits
        .Count(v => v.SelectedOption == option);
}

public bool DeleteFirstVisitInOption(string option)
{
    var firstVisit = _context.Visits
        .Where(v => v.SelectedOption == option)
        .OrderBy(v => v.PlaceInQueue)
        .FirstOrDefault();

    if (firstVisit == null) return false;

    _context.Visits.Remove(firstVisit);
    _context.SaveChanges();

    UpdateQueueForOption(option);
    return true;
}

private void UpdateQueueForOption(string option)
{
    var visits = _context.Visits
        .Where(v => v.SelectedOption == option)
        .OrderBy(v => v.PlaceInQueue)
        .ToList();

    for (int i = 0; i < visits.Count; i++)
    {
        visits[i].PlaceInQueue = i + 1;
    }

    _context.SaveChanges();
}

private int GetNextPlaceInQueue(string option)
{
    return _context.Visits
        .Count(v => v.SelectedOption == option) + 1;
}
    }
}
