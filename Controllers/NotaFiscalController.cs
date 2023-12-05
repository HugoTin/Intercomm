using Microsoft.AspNetCore.Mvc;

public class NotaFiscalController : Controller
{
    private INotaFiscalData NotaFiscalData;

    public NotaFiscalController (INotaFiscalData NotaFiscalData)
    {
        this.NotaFiscalData = NotaFiscalData;
    }
}