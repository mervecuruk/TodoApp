using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoAppNew.Models;
using TodoAppNew.Models.VMs;

namespace TodoAppNew.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if(ModelState.IsValid)
            {
                //var user = _mapper.Map<AppUser>(model);
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result=await _userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    //eğer kayıt başarılıysa signmanager sınıfından signin metodunu kullanarak kullanıcı girişini direkt yapmasını sağladık.IsPersistent: kullanıcı girişinin kalıcı olmasını istemediğimiz için false yaptık çünkü başka bir kullanıcı da girebilir
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Index","Home");
                }
                foreach (var item in result.Errors)
                {
                    //eğer bir model doğrulama hatası varsa bu hatlar motelstate içerisinde tutuluyor.Ve hata oluşturuğunda kullanıcıya gösterilir
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(model);       
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent:model.RememberMe,lockoutOnFailure:false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifresi hatalıdır.");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
