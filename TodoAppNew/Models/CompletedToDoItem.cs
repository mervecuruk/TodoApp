namespace TodoAppNew.Models
{
    public class CompletedToDoItem
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public string Description { get; set; }
        public DateTime CompletedDate { get; set; }=DateTime.Now;
        public bool Status { get; set; }
        public string AppUserId { get; set; }
    }
}
