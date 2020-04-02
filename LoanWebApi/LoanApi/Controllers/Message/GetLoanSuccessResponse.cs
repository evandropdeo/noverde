using System;

namespace LoanApi.Controllers.Message
{
    public class GetLoanSuccessResponse
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public string Refused_policy { get; set; }
        public string Amount { get; set; }
        public string Terms { get; set; }
    }
}
