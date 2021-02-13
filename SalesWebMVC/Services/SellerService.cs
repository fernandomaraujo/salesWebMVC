using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;

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

        // Removendo vendedor por seu Id
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            // Se não existe algum registro no banco de dados, com a condição (no caso, Id)
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                // Lança exceção
                throw new NotFoundException("Id not found");
            }

            // Se existe, atualiza. Depois salva mudanças
            try
            {
                _context.Update(obj);
                _context.SaveChanges();

            } 
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }
    }
}
