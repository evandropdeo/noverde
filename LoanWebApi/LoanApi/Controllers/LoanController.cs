using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanApi.Controllers.Message;
using LoanApi.Models;
using LoanApi.Rules;
using System;

namespace LoanApi.Controllers
{
    // fazer testes unitários

    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ClientContext _context;

        public LoanController(ClientContext context)
        {
            _context = context;
        }

        // GET: api/LoanController/X
        [HttpGet("{id}")]
        public async Task<ActionResult<GetLoanSuccessResponse>> GetTodoItem(long id)
        {

            Client client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            var result = new GetLoanSuccessResponse();
            result.Id = client.Id.ToString();

            if (client.IsComplete)
            {
                result.Status = "completed";
                result.Result = client.Result;
                if (client.Result == "refused")
                { 
                    result.Refused_policy = client.Refused_policy;
                    result.Amount = null;
                    result.Terms = null;
                }
                else
                {
                    result.Refused_policy = null;
                    result.Amount = client.Amount.ToString();
                    result.Terms = client.Terms.ToString();
                }
            }
            else {
                result.Status = "processing";
                result.Result = null;
                result.Refused_policy = null;
                result.Amount = null;
                result.Terms = null;
            }

            return result;
        }


        [HttpPost]
        public async Task<ActionResult<LoanSuccessResponse>> PostLoan(LoanRequest request)
        {
            var errors = new LoanErrorResponse();
            // Validação:
            if (null == request)
            {
                errors.AddError("Campos requeridos");
            }
            else
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    errors.AddError("O nome é obrigatório");
                }
                if (string.IsNullOrEmpty(request.Cpf))
                {
                    errors.AddError("O CPF é obrigatório");
                }
                if (request.Birthdate == null)
                {
                    errors.AddError("A data de nascimento é obrigatória");
                }
                if (request.Amount < 1000.00 || request .Amount > 4000.00)
                {
                    errors.AddError("O valor desejado deve estar entre R$ 1.000,00 à R$ 4.000,00");
                }
                if (request.Terms != 6 && request.Terms != 9 && request.Terms != 12 )
                {
                    errors.AddError("Quantidade de parcelas pode ser: 6, 9 ou 12");
                }
                if (request.Income <= 0)
                {
                    errors.AddError("A renda mensal é obrigatória");
                }
            }

            // Retorna a lista de erros de validação
            if (errors.Errors.Count > 0)
                return BadRequest(errors);


            Client client = new Client();
            client.Amount = request.Amount;
            client.Birthdate = request.Birthdate;
            client.Cpf = request.Cpf;
            client.Income = request.Income;
            client.IsComplete = false;
            client.Name = request.Name;
            client.Terms = request.Terms;

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            /**
             * Responde com o UIID e continua o processo em backend.
             **/
            Thread backend = new Thread(() => RunCreditPolices(_context, client));
            backend.Start();

            var result = new LoanSuccessResponse
            {
                Id = client.Id.ToString()
            };

            return result;
        }

        // GET: api/loan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetLoans()
        {
            return await _context.Clients.ToListAsync();
        }

        private void RunCreditPolices(ClientContext context, Client client)
        {
            try
            {
                /**
                 * Executa a lista de politicas atuais de maneira dinâmica. Para o processo quando recusada.
                 */
                foreach(EnumPolice enumPolice in Enum.GetValues(typeof(EnumPolice)))
                {
                    client = FactoryCreditPolice.CreateCreditPolice(enumPolice).Process(client);
                    if (client.Result == "refused")
                        break;
                }

                if (client.Result != "refused")
                {
                    client.Result = "approved";
                    client.Refused_policy = null;
                }

                client.IsComplete = true;
                context.Clients.Update(client);
                context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
