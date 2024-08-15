namespace BackendApp.Controllers.ViewModels
{
    public class VisitViewModel
    {
        public Guid UserId { get; set; }
        public int BusinessId { get; set; }
        public DateTime VisitTime { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
