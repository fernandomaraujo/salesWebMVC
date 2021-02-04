using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;

namespace SalesWebMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        // Por padrão, já cria ação Index
        public IActionResult Index()
        {

            // Lista de departamentos
            List<Department> list = new List<Department>();

            // Adicionando departamentos
            list.Add(new Department { Id = 1, Name = "Eletronics" });
            list.Add(new Department { Id = 2, Name = "Fashion" });

            // Enviando lista para a View
            // Enviando do Controller para View
            return View(list);
        }
    }
}
