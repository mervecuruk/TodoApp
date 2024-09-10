using System.Security.Claims;
using TodoAppNew.Models;
using TodoAppNew.Models.VMs;

namespace TodoAppNew.Services
{
    public interface IToDoItemService
    {
        //ÇALIŞAN METOTLAR
        void AddToDoItem(ToDoListVM model, string userId);
        Task<bool> CompletedTodoItemAsync(int id);
        void DeleteTodoItem(int id);
        Task<List<CompletedToDoItem>> GetCompletedTodoItemsAsync();

        Task<ToDoListVM> GetToDoItemById(int id);
        Task<List<ToDoListVM>> GetUserToDoList(string userId);
        Task<List<ToDoListVM>> Search(string name);
        void UpdateTodoItem(ToDoListVM model);


        /* //DİĞER GEREK OLMAYAN METOTLAR

        // Task AddToCompletedListAsync(ToDoItem todoItem);
        
        //Task<List<ToDoListVM>> GetAllTaskByStatus(bool status = true);
        //Task GetAllTaskByStatusAsync(bool completed, int id);
        //tamamlananlar için metot
        //Task<List<ToDoListVM>> GetCompletedTodoItemsAsync(ClaimsPrincipal user, ToDoItem todoItem);

        //tamamlanmayanlar için metot
        // Task<List<ToDoListVM>> GetIncompleteTodoItemsAsync(ClaimsPrincipal user);*/


    }
}