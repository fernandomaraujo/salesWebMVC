using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using SalesWebMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;

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
                return RedirectToAction(nameof(Error), new
                {
                    message = "Id not provided"
                });
            }

            // Pegando o objeto
            var obj = _sellerService.FindById(id.Value);

            // Se não existir
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Id not found"
                });
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
                return RedirectToAction(nameof(Error), new
                {
                    message = "Id not provided"
                });
            }

            // Pegando o objeto
            var obj = _sellerService.FindById(id.Value);

            // Se não existir
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Id not found"
                });
            }

            return View(obj);
        }

        // Id opcional
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Id not provided"
                });
            }

            var obj = _sellerService.FindById(id.Value);
            
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Id not found"
                });
            }

            // Se tudo existir

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel
            {
                Seller = obj,
                Departments = departments
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {

            // O id do vendedor, não pode ser diferença do id da requisição
            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Id mismatch"
                });
            }

            try
            {
                // Se tudo certo
                _sellerService.Update(seller);

                // Finalizado, redireciona para página index
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = e.Message
                });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = e.Message
                });
            }

        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }


    }
}
