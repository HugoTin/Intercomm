using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public class Home : Controller
{
    public ActionResult Index()
    {
        if(HttpContext.Session.GetString("user") ==  null){
            return RedirectToAction("Login", "user");
        }
        return View();
    }
}