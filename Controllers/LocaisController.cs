using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public class LocaisController : Controller
{
    private ILocaisData LocaisData;

    public LocaisController (ILocaisData LocaisData)
    {
        this.LocaisData = LocaisData;
    }



    /*
    ----- INDEX -----
    */
    public ActionResult Index ()
    {
        List<Locais> lista = LocaisData.Read();

        return View(lista);
    }
    //SEARCH
    public ActionResult Search (IFormCollection FormLocais)
    {
        string search = FormLocais["search"];
        int local = int.Parse(FormLocais["locais"]);

        List<Locais> lista = LocaisData.Read(search, local);
        
        ViewBag.Search = search;
        ViewData["search"] = search;

        return View("Index", lista);
    }



    /*
    ----- CREATE -----
    */
    [HttpGet]
    public ActionResult Create() 
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Locais locais)
    {
        LocaisData.Create(locais);
        return RedirectToAction("Index");
    }



    /*
    ----- UPDATE -----
    */
    [HttpGet]
    public ActionResult Update(int IdLocal)
    {
        Locais local = LocaisData.Read(IdLocal);

        if(local == null)
            return RedirectToAction("Index");

        return View(local);
    }

    [HttpPost]
    public ActionResult Update(Locais local)
    {
        LocaisData.Update(local);
        return RedirectToAction("Index");
    }



    /*
    ----- DELETE -----
    */
    public ActionResult Delete(int IdLocal) 
    {
        LocaisData.Delete(IdLocal);
        return RedirectToAction("Index");
    }



    /*
    ----- RESPONSAVEIS -----
    */
    //READ
    public JsonResult Responsaveis(int CodLocal)
    {
        List<Responsaveis> responsaveis = LocaisData.ReadResponsaveis(CodLocal);

        return Json(responsaveis);
    }

    //CREATE
    public JsonResult CreateResponsavel(Responsaveis responsaveis)
    {
        Responsaveis newResponsavel = LocaisData.CreateResponsaveis(responsaveis);

        return Json(newResponsavel);
    }

    //DELETE
    public JsonResult DeleteResponsavel(int IdResponsavel)
    {
        int OldIdResponsavel = LocaisData.DeleteResponsaveis(IdResponsavel);

        return Json(OldIdResponsavel);
    }



    /*
    ----- EMAILS -----
    */
    //READ
    public JsonResult Emails(int CodLocal)
    {
        List<Emails> emails = LocaisData.ReadEmails(CodLocal);

        return Json(emails);
    }

    //CREATE
    public JsonResult CreateEmail(Emails emails)
    {
        Emails newEmail = LocaisData.CreateEmails(emails);

        return Json(newEmail);
    }

    //DELETE
    public JsonResult DeleteEmail(int IdEmail)
    {   
        int OldEmail = LocaisData.DeleteEmails(IdEmail);

        return Json(OldEmail);
    }



    /*
    ----- TELEFONES -----
    */
    //READ
    public JsonResult Telefones(int CodLocal)
    {
        List<Telefones> telefones = LocaisData.ReadTelefones(CodLocal);

        return Json(telefones);
    }

    //CREATE
    public JsonResult CreateTelefone(Telefones telefones)
    {
        Telefones NewTelefone = LocaisData.CreateTelefones(telefones);

        return Json(NewTelefone);
    }

    //DELETE
    public JsonResult DeleteTelefone(int IdTelefone)
    {
        int OldTelefone = LocaisData.DeleteTelefones(IdTelefone);

        return Json(OldTelefone);
    }



    /*
    ----- MOTORISTA TRANSPORTADORAS -----
    */
    //READ
    public JsonResult ReadLocaisMotorista (int IdMotorista)
    {
        List<Locais> locais = LocaisData.ReadLocaisMotorista(IdMotorista);
    
        return Json(locais);
    }

    //CREATE
    public JsonResult CreateLocaisMotorista (int IdMotorista, int IdLocal)
    {
        LocaisData.CreateLocaisMotorista(IdMotorista, IdLocal);

        return Json("Success");
    }

    //DELETE
    public JsonResult DeleteLocaisMotorista (int IdMotorista, int IdLocal)
    {
        LocaisData.DeleteLocaisMotorista(IdMotorista, IdLocal);

        return Json("Success");
    }

}