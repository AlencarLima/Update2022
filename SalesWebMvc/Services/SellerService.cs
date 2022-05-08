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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }
        
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id) //procurar um vendedor por id
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }
        public void Remove(int id)  //remover o vendedor
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj); //método que remove o obj
            _context.SaveChanges(); //confirmar a remoção e salva-la
        }

        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id)) //any serve para testar se há algum registro no banco de dados com a configuração inserida
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e) //intercepta uma excessão do nível de acesso a dados
            {
                throw new DbConcurrencyException(e.Message); //relança a excessão a nível de acesso de serviço (segrega camadas)
            }
        }
    }
}