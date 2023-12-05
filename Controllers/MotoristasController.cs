using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class MotoristasController : Controller
{
    private IMotoristasData MotoristasData;
    private ILocaisData LocaisData;

    public MotoristasController (IMotoristasData MotoristasData, ILocaisData LocaisData)
    {
        this.MotoristasData = MotoristasData;
        this.LocaisData = LocaisData;
    }

    /*
    ----- INDEX -----
    */
    public ActionResult Index()
    {
        List<Motoristas> lista = MotoristasData.Read();

        return View(lista);
    }
    //SEARCH
    public ActionResult Search(IFormCollection FormMotoristas)
    {
        string search = FormMotoristas["search"];

        List<Motoristas> motoristas = MotoristasData.Read(search);

        return View("Index", motoristas);
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
    public ActionResult Create(Motoristas motorista)
    {
        MotoristasData.Create(motorista);

        return RedirectToAction("Index");
    }



    /*
    ----- UPDATE -----
    */
    [HttpGet]
    public ActionResult Update(int IdMotorista)
    {
        ViewBag.Transportadoras = LocaisData.Read("", (int) TipoLocal.Transportadora).Select(c => new SelectListItem()
            { 
                Text= c.IdLocal + " - " + c.LocalRazaoSocial, 
                Value = c.IdLocal.ToString()
            }).ToList();

        Motoristas motorista = MotoristasData.Read(IdMotorista);

        return View(motorista);
    }
    [HttpPost]
    public ActionResult Update(Motoristas motorista)
    {
        MotoristasData.Update(motorista);

       return RedirectToAction("Index");
    }



    /*
    ----- LOCAIS_MOTORISTAS -----
    */
    //READ MOTORISTAS LOCAL
    public JsonResult ReadMotoristasLocal(int IdLocal)
    {
        List<Motoristas> motoristas = MotoristasData.ReadMotoristasLocal(IdLocal);

        return Json(motoristas);
    }

    [HttpGet]
    //READ MOTORISTAS
    public JsonResult ReadMotoristas()
    {   
        List<Motoristas> motoristas = MotoristasData.ReadAtivo();

        return Json(motoristas);
    }
}