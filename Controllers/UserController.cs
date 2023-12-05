using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public class UserController : Controller
{
    private IUserData UserData;

    public UserController(IUserData UserData) 
    {
        this.UserData = UserData;
    }

    [HttpGet]
    public ActionResult Login()
    {
        if(HttpContext.Session.GetString("user") !=  null){
            string? session = HttpContext.Session.GetString("user");
            User? user = JsonSerializer.Deserialize<User>(session);
        }

        return View();
    }

    [HttpPost]
    public ActionResult Login(User? User)
    {
        User? user = UserData.Login(User);

        if(user == null)
        {
            ViewBag.Error = "Usuário/Senha inválidos";
            return View();
        }

        HttpContext.Session.SetString("user", JsonSerializer.Serialize(user));

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
