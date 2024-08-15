using Microsoft.AspNetCore.Mvc;
using ProcessadorJSON.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessadorJSON.Controllers
{
    public class ProcessamentoController : Controller
    {
        private readonly ProcessamentoService _processamentoService;

        public ProcessamentoController(ProcessamentoService processamentoService)
        {
            _processamentoService = processamentoService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessarArquivos(string pastaCaminho)
        {
            var resultado = await _processamentoService.ProcessarArquivos(pastaCaminho);
            return Json(resultado);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
