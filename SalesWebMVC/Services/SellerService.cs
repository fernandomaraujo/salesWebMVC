using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        // Dependência para o DBcontext
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        // Retornar lista com todos os vendedores do banco de dados
        public List<Seller> FindAll()
        {
            // Convertendo pra lista
            return _context.Seller.ToList();
        }

        // Inserir um novo vendedor no banco de dados
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        // Encontrando vendedor por seu Id
        public Seller FindById(int id)
        {
            return _context.Seller
                .Include(obj => obj.Department)
                .FirstOrDefault(obj => obj.Id == id);
        }

        // Recomendo vendedor por seu Id
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
