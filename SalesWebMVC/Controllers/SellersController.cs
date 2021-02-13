using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using SalesWebMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        // Dependência para o SellerService
        private readonly SellerService _sellerService;

        // Dependência para o DepartementServer
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();

            return View(list);
        }

        public IActionResult Create()
        {
            // Carregando todos os departamentos
            var departments = _departmentService.FindAll();

            // Instanciando objeto do ViewModel
            var viewModel = new SellerFormViewModel
            {
                Departments = departments
            };

            return View(viewModel);
        }

        // Recebendo um objeto vendedor, que veio na requisição
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);

            // Redirecionando ação para a tela Index
            return RedirectToAction(nameof(Index));
        }

        // Id opcional
        public IActionResult Delete(int? id)
        {
            // Se nulo, requisição foi de forma indevida
            if(id == null)
            {
                return NotFound();
            }

            // Pegando o objeto
            var obj = _sellerService.FindById(id.Value);

            // Se não existir
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        // Id opcional
        public IActionResult Details(int? id)
        {

            // Se nulo, requisição foi de forma indevida
            if (id == null)
            {
                return NotFound();
            }

            // Pegando o objeto
            var obj = _sellerService.FindById(id.Value);

            // Se não existir
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
    }
}
