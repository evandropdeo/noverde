using LoanApi.Models;

namespace LoanApi.Rules
{
    public interface ICreditPolice
    {
        public Client Process(Client client);
    }
}
