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
        public async Task<List<Seller>> FindAllAsync()
        {
            // Convertendo pra lista
            return await _context.Seller.ToListAsync();
        }

        // Inserir um novo vendedor no banco de dados
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        // Encontrando vendedor por seu Id
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller
                .Include(obj => obj.Department)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        // Removendo vendedor por seu Id
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);

            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {

            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            // Se não existe algum registro no banco de dados, com a condição (no caso, Id)
            if (!hasAny)
            {
                // Lança exceção
                throw new NotFoundException("Id not found");
            }

            // Se existe, atualiza. Depois salva mudanças
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();

            } 
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }
    }
}
