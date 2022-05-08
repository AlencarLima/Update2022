using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }
        
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id) //procurar um vendedor por id
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)  //remover o vendedor
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj); //método que remove o obj
            _context.SaveChangesAsync(); //confirmar a remoção e salva-la
        }

        public async Task UpdateAsync(Seller obj)
        {
            if (! await _context.Seller.AnyAsync(x => x.Id == obj.Id)) //any serve para testar se há algum registro no banco de dados com a configuração inserida
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) //intercepta uma excessão do nível de acesso a dados
            {
                throw new DbConcurrencyException(e.Message); //relança a excessão a nível de acesso de serviço (segrega camadas)
            }
        }
    }
}