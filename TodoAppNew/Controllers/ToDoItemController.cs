using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoAppNew.Models;
using TodoAppNew.Models.VMs;
using TodoAppNew.Services;

namespace TodoAppNew.Controllers
{
    [Authorize]
    public class ToDoItemController : Controller
    {
        private readonly IToDoItemService _service;
        private readonly UserManager<AppUser> _userManager;
       
        public ToDoItemController(IToDoItemService service, UserManager<AppUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        /// <summary>
        /// CLAIMSPRINCIPAL nedir?
        /// </summary>
        /// <param name="service"></param>
        /// <param name="userManager"></param>
      
        //ClaimsPrincipal: kimlik doğrulama (authentication) ve yetkilendirme (authorization) süreçlerinde kullanılan temel bir sınıftır. Bir kullanıcının kimliği ve sahip olduğu iddiaları (claims) hakkında bilgi tutar.


        /// <summary>
        /// İlk eklenen tüm görevlerin listesi
        /// Statusu true olmayanların
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var list = await _service.GetUserToDoList(userId);
            return View(list);
        }

        /// <summary>
        /// Ekleme
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ToDoListVM model)
        {
            var userId = _userManager.GetUserId(User);
            _service.AddToDoItem(model, userId);
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Güncelleme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Update(int id)
        {
            var item = await _service.GetToDoItemById(id);
            return View(item);
        }


        [HttpPost]
        public IActionResult Update(ToDoListVM model)
        {
            _service.UpdateTodoItem(model);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Silme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            _service.DeleteTodoItem(id);
            return RedirectToAction("Index");
        }



        /// <summary>
        /// tamamlandı işaretlenenler için bir acton metot
        /// deneme   ve çalıştııı!!!!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Completed(int id)
        {
            var result = await _service.CompletedTodoItemAsync(id);

            if (result)
            {
                // Güncelleme başarılı
                return RedirectToAction("Index"); // Başarıyla güncellenmiş sayfayı göster
            }
            else
            {
                // Güncelleme başarısız
                return StatusCode(500); // İç sunucu hatası
            }
        }


        /// <summary>
        /// Tamamlanan görevleri listeleyen metot
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CompletedTasks()
        {
            var completedItems = await _service.GetCompletedTodoItemsAsync();
            return View(completedItems); // Tamamlanan görevler için bir View döndürür
        }

        
        //Gerekli olmayan metotlar
        /*
         
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Completed(bool completed,int id)
        //{
        //    try
        //    {
        //        await _service.GetAllTaskByStatusAsync(completed,id);
        //        // Form gönderildiğinde sayfayı yenileyin veya uygun bir yönlendirme yapın
        //        return RedirectToAction("Index"); // Veya başka bir uygun view
        //    }
        //    catch (Exception ex)
        //    {
        //        // Hata yönetimi
        //        Console.WriteLine("Hata: " + ex.Message);
        //        return StatusCode(500); // İç sunucu hatası
        //    }
        //}




        ////tamamlanmayanların listesi->çalışıyor sadece şuan gerekli değil
        //[HttpGet]
        //public async Task<IActionResult> GetInCompletedTasks()
        //{
        //    var incompleteTasks = await _service.GetIncompleteTodoItemsAsync(User);
        //    return View(incompleteTasks); // Ana görev listesi
        //}
         
         */






    }
}
