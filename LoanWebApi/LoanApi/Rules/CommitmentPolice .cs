using LoanApi.Models;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace LoanApi.Rules
{
    public class CommitmentPolice : ICreditPolice
    {
        public Client Process(Client client)
        {
            try
            {


                CommitmentRequest commitmentRequest = new CommitmentRequest
                {
                    cpf = client.Cpf
                };
                string commitmentRequestBody = JsonConvert.SerializeObject(commitmentRequest);
                var restClient = new RestClient("https://challenge.noverde.name/commitment");
                restClient.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("x-api-key", " HQK38AzG516eDueIyvIUL3yKTW1HqMaU1PSPYzVr");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", commitmentRequestBody, ParameterType.RequestBody);
                IRestResponse response = restClient.Execute(request);
                CommitmentResult result = JsonConvert.DeserializeObject<CommitmentResult>(response.Content.ToString());
                // Parcela ultrapassa a renda comprometida:
                double rendaComprometida = client.Income - (client.Income * result.commitment);
                double parcela = 0;

                for (Int16 i = client.Terms; i <= 12; i += 3)
                {
                    client.Terms = i;
                    parcela = CalculoDaParcela(client.Amount, client.Terms, client.Score);
                    if (rendaComprometida > parcela)
                        break;
                }

                if(parcela > rendaComprometida)
                {
                    client.Result = "refused";
                    client.Refused_policy = "commitment";
                }
            }
            catch(Exception e)
            {
                throw e;
            }

            return client;

        }
        public static double CalculoDaParcela(double pv, int n, int score)
        {
            double i = CalculoDeJuros(n, score);
            double PMT = pv * (Math.Pow(1 + i, n) * i / (Math.Pow(1 + i, n) - 1));
            return PMT;
        }

        public static double CalculoDeJuros(int terms, int score)
        {
            double juros = 0;
            if (score >= 900)
            {
                switch (terms)
                {
                    case 6:
                        juros = 0.039;
                        break;
                    case 9:
                        juros = 0.042;
                        break;
                    case 12:
                        juros = 0.045;
                        break;
                }
            }
            else if (score >= 800)
            {
                switch (terms)
                {
                    case 6:
                        juros = 0.047;
                        break;
                    case 9:
                        juros = 0.050;
                        break;
                    case 12:
                        juros = 0.053;
                        break;
                }
            }
            else if (score >= 700)
            {
                switch (terms)
                {
                    case 6:
                        juros = 0.055;
                        break;
                    case 9:
                        juros = 0.058;
                        break;
                    case 12:
                        juros = 0.061;
                        break;
                }
            }
            else if (score >= 600)
            {
                switch (terms)
                {
                    case 6:
                        juros = 0.064;
                        break;
                    case 9:
                        juros = 0.066;
                        break;
                    case 12:
                        juros = 0.069;
                        break;
                }
            }
            return juros;
        }
    }
}
