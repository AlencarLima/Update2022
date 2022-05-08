﻿using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;


namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context; //dependencia do entityframework

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Department> FindAll() //método para retornar todos os departamentos
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}