using LoanApi.Models;
using System;

namespace LoanApi.Rules
{
    public class AgePolice : ICreditPolice
    {
        public Client Process(Client client)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan span = DateTime.Now - client.Birthdate;
            int years = (zeroTime + span).Year - 1;
            if (years < 18)
            {
                client.Refused_policy = "age";
                client.Result = "refused";
            }

            return client;
        }
    }
}
