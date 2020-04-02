using LoanApi.Models;
using Newtonsoft.Json;
using RestSharp;

namespace LoanApi.Rules
{
    public class ScorePolice : ICreditPolice
    {
        public Client Process(Client client)
        {
            ScoreRequest scoreRequest = new ScoreRequest
            {
                cpf = client.Cpf
            };
            string scoreRequestBody = JsonConvert.SerializeObject(scoreRequest);
            var restClient = new RestClient("https://challenge.noverde.name/score");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("ContentType", "application/json");
            request.AddHeader("x-api-key", "HQK38AzG516eDueIyvIUL3yKTW1HqMaU1PSPYzVr");
            request.AddParameter("undefined", scoreRequestBody, ParameterType.RequestBody);
            IRestResponse response = restClient.Execute(request);
            ScoreResult result = JsonConvert.DeserializeObject<ScoreResult>(response.Content.ToString());

            if (result.score < 600)
            {
                client.Refused_policy = "score";
                client.Result = "refused";
            }
            client.Score = result.score;

            return client;
        }
    }
}
