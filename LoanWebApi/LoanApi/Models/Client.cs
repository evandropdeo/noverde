using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanApi.Models
{
	public class Client
	{
        public long Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime Birthdate { get; set; }
        public double Amount { get; set; }
        public Int16 Terms { get; set; }
        public double Income { get; set; }
        public bool IsComplete { get; set; }
        public string Result { get; set; }
        public string Refused_policy { get; set; }
        public int Score { get; set; }
    }
}
