using TodoAppNew.Models.Enums;

namespace TodoAppNew.Models.VMs
{
    public class ToDoListVM
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public string Description { get; set; }
        public DateTime FinishedDate { get; set; }
        public PriorityLevel Priority { get; set; }
        public bool Status { get; set; } = false;

    }
}
