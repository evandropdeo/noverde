using System;
using System.ComponentModel.DataAnnotations;

namespace LoanApi.Controllers.Message
{
    public class LoanRequest
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime Birthdate { get; set; }
        public double Amount { get; set; }
        public Int16 Terms { get; set; }
        public double Income { get; set; }
    }
}
