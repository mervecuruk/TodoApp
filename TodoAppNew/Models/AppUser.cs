using Microsoft.AspNetCore.Identity;

namespace TodoAppNew.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<ToDoItem> ToDoItems { get; set; }
        public AppUser()
        {
            ToDoItems = new HashSet<ToDoItem>();
        }

    }
}
