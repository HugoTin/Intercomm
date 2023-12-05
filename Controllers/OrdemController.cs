using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class OrdemController : Controller
{
    private IOrdemData OrdemData;
    private IContratosData ContratosData;
    private ILocaisData LocaisData;
    private IMotoristasData MotoristasData;

    public OrdemController (IOrdemData OrdemData, IContratosData ContratosData, ILocaisData LocaisData, IMotoristasData MotoristasData)
    {
        this.OrdemData = OrdemData;
        this.ContratosData = ContratosData;
        this.LocaisData = LocaisData;
        this.MotoristasData = MotoristasData;
    }

    public ActionResult index()
    {
        List<Ordem> Ordens = OrdemData.Read();

        return View(Ordens);
    }



    /*
    ----- CREATE -----
    */
    [HttpGet]
    public ActionResult create()
    {
        ViewBag.Locais = LocaisData.ReadLocaisContrato().Select(c => new SelectListItem
            {
                Text = c.IdLocal + " - " + c.LocalRazaoSocial,
                Value = c.IdLocal.ToString()
            }).AsList();

        ViewBag.Destinos = LocaisData.ReadDestino().Select(c => new SelectListItem()
            {
                Text = c.IdLocal + " - " + c.LocalRazaoSocial,
                Value = c.IdLocal.ToString()
            }).ToList();

        ViewBag.Transportadoras = LocaisData.Read("", (int) TipoLocal.Transportadora).Select(c => new SelectListItem()
            { 
                Text= c.IdLocal + " - " + c.LocalRazaoSocial, 
                Value = c.IdLocal.ToString()
            }).ToList();


        return View();
    }

    [HttpPost]
    public ActionResult create(Ordem ordem)
    {
        OrdemData.Create(ordem);

        return RedirectToAction("Index");
    }



    /*
    ----- UPDATE -----
    */
    [HttpGet]
    public ActionResult update(int IdOrdem)
    {

        Ordem ordem = OrdemData.Read(IdOrdem);

        ViewBag.Locais = LocaisData.ReadLocaisContrato().Select(c => new SelectListItem
            {
                Text = c.IdLocal + " - " + c.LocalRazaoSocial,
                Value = c.IdLocal.ToString()
            }).AsList();

        ViewBag.Motoristas = MotoristasData.ReadAtivo().Select(c => new SelectListItem
            {
                Text = c.IdMotorista + " - " + c.NomeMotorista,
                Value = c.IdMotorista.ToString()
            }).AsList();

        ViewBag.Destinos = LocaisData.ReadDestino().Select(c => new SelectListItem()
            {
                Text = c.IdLocal + " - " + c.LocalRazaoSocial,
                Value = c.IdLocal.ToString()
            }).ToList();

        ViewBag.Transportadoras = LocaisData.Read("", (int) TipoLocal.Transportadora).Select(c => new SelectListItem()
            { 
                Text= c.IdLocal + " - " + c.LocalRazaoSocial, 
                Value = c.IdLocal.ToString()
            }).ToList();
        
        ViewBag.Contratos = ContratosData.ReadContratos(ordem.Contrato.Locais.IdLocal, 1).Select(c => new SelectListItem()
            { 
                Text= c.IdContrato + " - " + c.Locais.LocalRazaoSocial + " - Volume: " + c.Volume + " m²", 
                Value = c.IdContrato.ToString()
            }).ToList();
 

        return View(ordem);
    }
    [HttpPost]
    public ActionResult update(Ordem ordem)
    {
        OrdemData.Update(ordem);

        return RedirectToAction("index");
    }
}