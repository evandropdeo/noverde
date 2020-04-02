using System.Collections.Generic;

namespace LoanApi.Controllers.Message
{
    public class LoanErrorResponse
    {
        public List<string> Errors { get; }

        public LoanErrorResponse(){
            this.Errors = new List<string>();
        }

        public void AddError(string erro)
        {
            Errors.Add(erro);
        }
    }
}
