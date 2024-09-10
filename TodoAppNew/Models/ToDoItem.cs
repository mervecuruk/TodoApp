using TodoAppNew.Models.Enums;

namespace TodoAppNew.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public string Description { get; set; }
        public DateTime FinishedDate { get; set; }
        public PriorityLevel Priority { get; set; }
        public bool Status { get; set; } = false;

        //navigation property
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
