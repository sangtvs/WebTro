using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using testt.Models;

namespace testt.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Login(){
        return View();
    }
    public IActionResult IndexAdmin()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(User model)
    {
        Database database = new Database();
        User users = database.GetUsers(model);
        if (users != null)
        {
            // Đăng nhập thành công
            // Kiểm tra vai trò của người dùng và chuyển hướng đến trang tương ứng
            TempData["UserName"] = model.UserName;
            TempData.Keep("UserName");
            if (users.Role == "admin")
            {
                TempData["Role"] = "admin";
                TempData["Name"] = users.Name.ToString();
                TempData["Email"] = users.Email.ToString();
                TempData["Phone"] = users.Phone.ToString();
                TempData["Address"] = users.Address.ToString();
                TempData["Status"] = users.Status.ToString();
                TempData.Keep("Role");
                return RedirectToAction("IndexAdmin", "Home"); // Chuyển hướng đến trang admin
            }
            else
            {
                TempData["Role"] = "user";
                TempData["Name"] =users.Name.ToString();
                TempData["Email"] = users.Email.ToString();
                TempData["Phone"] = users.Phone.ToString();
                TempData["Address"] = users.Address.ToString();
                TempData["Status"] = users.Status.ToString();
                TempData.Keep("Role");
             
                return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ
            }

        }
        else
        {
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            // Đăng nhập không thành công, hiển thị lại trang đăng nhập với thông báo lỗi
            return View();
        }
    }
    [HttpPost]
    public IActionResult dangky(User model)
    {
        Database database=new Database();
        User existingUser = database.CheckUsers(model);
        if (model.UserName == null || model.Name == null || model.PassWord == null || model.Email == null || model.Phone == null)
        {
            return View();
        }
        else
        if (existingUser != null)
        {
            ModelState.AddModelError("", "Người dùng đã tồn tại.");
            return View();
        }
        else
        
        {
            database.AddUser(model);
            // Đăng ký thành công, chuyển hướng đến trang đăng nhập
            return RedirectToAction("Login");
        }
    }
    [HttpPost]
    public IActionResult thongtin(User model){
        Database database=new Database();
        User existingUser = database.CheckUsers(model);
        if (existingUser != null)
        {
            ModelState.AddModelError("", "Người dùng đã tồn tại.");
            return View();
        }
        else
        {
            database.Themkh(model);
            return View();
        }
    }
    [HttpGet]
    public IActionResult suathongtin(int id)
    {
        Database database=new Database();
        User user=database.GetUsers(id);
        return View(user);
    }
    public ActionResult DeleteUser(int id)
    {
        Database database=new Database();
        database.DeleteUser(id);
        return RedirectToAction("thongtin");
    }
    [HttpPost]
    public IActionResult suathongtin(User model) { 
        Database database=new Database();
        database.saveUser(model);
        database.addlich(model.Phone, model.NgayThue, model.NgayTra);
        return RedirectToAction("thongtin","Home");
    }
    [HttpPost]
    public IActionResult ThemPhong(Tro model)
    {
        Database database=new Database();
        using (var memoryStream = new MemoryStream())
        {
            model.AnhPhong.CopyTo(memoryStream);
            model.anh = memoryStream.ToArray();
        }
        database.ThemPhong(model);
        return RedirectToAction("IndexAdmin");
    }
    public ActionResult GetImage(int id)
    {
        Database db=new Database();
        byte[] imageData = db.GetAnh(id);

        // Trả về hình ảnh dưới dạng FileResult
        return File(imageData, "image/jpeg");
    }
    public IActionResult Dangky()
    {
        return View();
    }
    public IActionResult Chitiet(int id) {
        Database database=new Database();
        Tro model=database.DetailPhong(id);
        return View(model) ;
    }
    public IActionResult DeletePhong(int id)
    {
        Database database=new Database();
        database.DeletePhong(id);
        return RedirectToAction("IndexAdmin");
    }
    public IActionResult Thongtin(){
        return View();
    }
    public IActionResult Suathongtin()
    {
        return View();
    }
    public IActionResult Logout()
    {
        TempData.Remove("UserName");
        TempData.Remove("Role");
        return RedirectToAction("Index", "Home");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
