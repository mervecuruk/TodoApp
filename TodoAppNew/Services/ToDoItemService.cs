using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoAppNew.Contexts;
using TodoAppNew.Models;
using TodoAppNew.Models.VMs;

namespace TodoAppNew.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly AppDbContext _Context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ToDoItemService(AppDbContext Context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _Context = Context;
            _userManager = userManager;
            _mapper = mapper;
        }



        /// <summary>
        /// ekleme
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        public void AddToDoItem(ToDoListVM model, string userId)
        {
            //var item = new ToDoItem()
            //{
            //    AppUserId = userId,
            //    Task = vm.Task,
            //    Description = vm.Description,
            //    FinishedDate = vm.FinishedDate,
            //    Status = vm.Status,
            //    Priority = vm.Priority
            //};
            var todoitem = _mapper.Map<ToDoItem>(model);
            todoitem.AppUserId = userId;
            _Context.ToDoItems.Add(todoitem);
            _Context.SaveChanges();
        }



        /// <summary>
        /// silme
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTodoItem(int id)
        {
            var item = _Context.ToDoItems.Find(id);
            if (item != null)
            {
                _Context.ToDoItems.Remove(item);
                _Context.SaveChanges();
            }
            Console.WriteLine("Task Silinemedi");
        }



       /// <summary>
       /// güncelleme
       /// </summary>
       /// <param name="model"></param>
        public void UpdateTodoItem(ToDoListVM model)
        {
            var item = _Context.ToDoItems.Find(model.Id);
            if (item != null)
            {
                var result = _mapper.Map<ToDoItem>(model);
                _Context.ToDoItems.Update(result);
                _Context.SaveChanges();
            }
            Console.WriteLine("Task Güncellenemedi");

        }



        /// <summary>
        /// id ye göre item bulma
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ToDoListVM> GetToDoItemById(int id)
        {
            return await _Context.ToDoItems.Where(x => x.Id == id).Select(x => new ToDoListVM { Id = x.Id, Description = x.Description, FinishedDate = x.FinishedDate, Priority = x.Priority, Status = x.Status, Task = x.Task }).FirstOrDefaultAsync();
        }



        /// <summary>
        /// listeleme:Tümünü listeler
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ToDoListVM>> GetUserToDoList(string userId)
        {
            return await _Context.ToDoItems.Where(x => x.AppUserId == userId)
                .Select(x => new ToDoListVM
                {
                    Id = x.Id,
                    Description = x.Description,
                    Status = x.Status,
                    FinishedDate = x.FinishedDate,
                    Priority = x.Priority,
                    Task = x.Task
                }).ToListAsync();
        }



        /// <summary>
        ///Statusu true yapıp ana listeden silip tamamlananlar listesine ekleyen metot
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> CompletedTodoItemAsync(int id)
        {
            var todoItem = await _Context.ToDoItems.FindAsync(id);

            // Tamamlandı mı alanını güncelle
            todoItem.Status = true;

            // Değişiklikleri kaydet
            // var result = await _Context.SaveChangesAsync();



            //CompletedToDoItem nesnesi oluştur
            var completedItem = new CompletedToDoItem
            {
                Task = todoItem.Task,
                Description = todoItem.Description,
                CompletedDate = DateTime.Now, // Tamamlanma tarihini kaydet
                Status = true, // Tamamlandı olarak işaretliyoruz
                AppUserId = todoItem.AppUserId
            };
            //var result = _mapper.Map<CompletedToDoItem>(todoItem);

            // Ana listeden sil ve tamamlananlar listesine ekle
            _Context.ToDoItems.Remove(todoItem);
            _Context.CompletedToDoItems.Add(completedItem);

            return await _Context.SaveChangesAsync() > 0;


        }




        /// <summary>
        /// tamamlananlar için metot
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<List<CompletedToDoItem>> GetCompletedTodoItemsAsync()
        {

            return await _Context.CompletedToDoItems.Select(x => new CompletedToDoItem { Id = x.Id, Description = x.Description, Status = x.Status, Task = x.Task }).ToListAsync();
        }


        /// <summary>
        /// Arama Metodu
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<ToDoListVM>> Search(string name)
        {
            return await _Context.ToDoItems.Where(x => x.Task.Contains(name)).Select(x => new ToDoListVM { Id = x.Id, Description = x.Description, FinishedDate = x.FinishedDate, Priority = x.Priority, Status = x.Status, Task = x.Task }).ToListAsync();
        }

        /*
         
        ///Şuan  kullanılmayan diğer metotlar

        /// <summary>
        /// tamamlanmayanları listeler
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
      
        //public async Task<List<ToDoListVM>> GetIncompleteTodoItemsAsync(ClaimsPrincipal user)
        //{
        //    var userId = _userManager.GetUserId(user);

        //    return await _Context.ToDoItems
        //        .Where(t => t.AppUserId == userId && !t.Status) // Tamamlanmamış görevler
        //        .Select(t => new ToDoListVM
        //        {
        //            Id = t.Id,
        //            Description = t.Description,
        //            FinishedDate = t.FinishedDate,
        //            Priority = t.Priority,
        //            Status = t.Status,
        //            Task = t.Task
        //        }).ToListAsync();
        //}


        ///birlikte yaptığımız
        //public async Task<List<ToDoListVM>> GetAllTaskByStatus(bool status ,int id)
        //{
        //    return await _Context.ToDoItems.Where(x => x.Status == status).Select(x => new ToDoListVM { Id = x.Id, Description = x.Description, FinishedDate = x.FinishedDate, Priority = x.Priority, Status = x.Status, Task = x.Task }).ToListAsync();
        //   
        //}


        ///Benim Yaptığım
        //public async Task GetAllTaskByStatusAsync(bool completed, int id)
        //{
        //    var item = await _Context.ToDoItems.FindAsync(id);
        //    if (item != null)
        //    {
        //        if (item.Status != completed)
        //        {
        //            item.Status = completed;
        //            await _Context.SaveChangesAsync();
        //        }
        //    }
        //}
         
         */





    }
}
