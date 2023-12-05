using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ContratosController : Controller
{
    private IContratosData ContratosData;
    private ILocaisData LocaisData;

    public ContratosController (IContratosData ContratosData, ILocaisData LocaisData)
    {
        this.ContratosData = ContratosData;
        this.LocaisData = LocaisData;
    }



    /*
    ----- INDEX -----
    */
    public ActionResult Index()
    {
        List<Contratos> lista = ContratosData.Read();
        return View(lista);
    }
    //SEARCH
    public ActionResult Search(IFormCollection FormContratos)
    {
        string search = FormContratos["search"];

        List<Contratos> lista = ContratosData.Read(search);

        ViewBag.Search = search;

        return View("Index", lista);
    }



    /*
    ----- CREATE -----
    */
    [HttpGet]
    public ActionResult Create() 
    {
        ViewBag.Locais = LocaisData.ReadLocaisContrato().Select(c => new SelectListItem()
            { 
                Text= c.LocalRazaoSocial, 
                Value = c.IdLocal.ToString()
            }).ToList();

        return View();
    }

    [HttpPost]
    public ActionResult Create(Contratos contratos)
    {
        ContratosData.Create(contratos);
        return RedirectToAction("Index");
    }



    /*
    ----- UPDATE -----
    */
    [HttpGet]
    public ActionResult Update(int IdContrato) 
    {      
        ViewBag.Locais = LocaisData.Read().Select(c => new SelectListItem()
            { 
                Text= c.LocalRazaoSocial, 
                Value = c.IdLocal.ToString()
            }).ToList();

        Contratos contrato = ContratosData.Read(IdContrato);

        if(contrato == null)
            return RedirectToAction("Index");

        return View(contrato);
    }

    [HttpPost]
    public ActionResult Update(Contratos contratos)
    {
        ContratosData.Update(contratos);
        return RedirectToAction("Index");
    }



    /*
    ----- DELETE -----
    */
    public ActionResult Delete(int IdContrato)
    {
        ContratosData.Delete(IdContrato);
        return View("index");
    }

    /*
    ----- ORDEM -----
    */
    public JsonResult LocaisContratos(int IdLocal)
    {
        List<Contratos> lista = ContratosData.ReadContratos(IdLocal);

        return Json(lista);
    }
}