using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ConjuntosController : Controller
{
    private IConjuntosData ConjuntosData;
    private IMotoristasData MotoristasData;

    public ConjuntosController (IConjuntosData ConjuntosData, IMotoristasData MotoristasData)
    {
        this.ConjuntosData = ConjuntosData;
        this.MotoristasData = MotoristasData;
    }



    /*
    ----- INDEX -----
    */
    public ActionResult Index()
    {
        List<Conjuntos> conjuntos = ConjuntosData.Read();

        return View(conjuntos);
    }

    public ActionResult Search(IFormCollection FormConjuntos)
    {
        string search = FormConjuntos["search"];

        List<Conjuntos> conjuntos = ConjuntosData.Read(search);

        return View("index", conjuntos);
    }


    /*
    ----- CREATE -----
    */
    [HttpGet]
    public ActionResult Create()
    {
        ViewBag.Motoristas = MotoristasData.Read().Select(c => new SelectListItem()
            {
                Text = c.NomeMotorista,
                Value = c.IdMotorista.ToString()
            }).ToList();

        return View();
    }
    [HttpPost]
    public ActionResult Create(Conjuntos conjuntos)
    {
        ConjuntosData.Create(conjuntos);

        return RedirectToAction("Index");
    }



    /*
    ----- UPDATE -----
    */
    [HttpGet]
    public ActionResult Update(int IdConjunto)
    {
        ViewBag.Motoristas = MotoristasData.Read().Select(c => new SelectListItem()
            {
                Text = c.NomeMotorista,
                Value = c.IdMotorista.ToString()
            }).ToList();

        Conjuntos conjunto = ConjuntosData.Read(IdConjunto);

        return View(conjunto);
    }
    [HttpPost]
    public ActionResult Update(Conjuntos conjuntos)
    {
        ConjuntosData.Update(conjuntos);

        return RedirectToAction("index");
    }



    /*
    ----- CONJUNTOS MOTORISTA -----
    */
    //READ
    public JsonResult ReadConjuntosMotorista(int IdMotorista)
    {
        List<Conjuntos> conjuntos = ConjuntosData.ReadConjuntosMotorista(IdMotorista);

        return Json(conjuntos);
    }

    public JsonResult ReadConjunto(int IdConjunto)
    {
        Conjuntos conjunto = ConjuntosData.Read(IdConjunto);

        return Json(conjunto);
    }
}