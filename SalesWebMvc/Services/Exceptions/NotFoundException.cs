using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class NotFoundException : ApplicationException //para ter um controle maior em quando ocorrer erros
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
