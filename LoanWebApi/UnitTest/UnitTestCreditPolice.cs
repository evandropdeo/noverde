using LoanApi.Models;
using LoanApi.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTestCreditPolice
    {
        [TestMethod]
        public void TestMenorIdade()
        {
            Client client = new Client();
            client.Birthdate = DateTime.Parse("2007-01-01");
            var result = FactoryCreditPolice.CreateCreditPolice(EnumPolice.AGE).Process(client);
            Assert.AreEqual("age", result.Refused_policy);
        }

        [TestMethod]
        public void TestMaiorIdade()
        {
            Client client = new Client();
            client.Birthdate = DateTime.Parse("1900-01-01");
            var result = FactoryCreditPolice.CreateCreditPolice(EnumPolice.AGE).Process(client);
            Assert.AreNotEqual("age", result.Refused_policy);
        }


        [TestMethod]
        public void TestScore()
        {
            Client client = new Client();
            client.Cpf = "32165498700";
            var result = FactoryCreditPolice.CreateCreditPolice(EnumPolice.SCORE).Process(client);
            Assert.IsTrue(result.Score > 0 && result.Score < 1000);
        }

        [TestMethod]
        public void TestScoreMenor600()
        {
            Client client = new Client();
            client.Cpf = "12345678901";
            var result = FactoryCreditPolice.CreateCreditPolice(EnumPolice.SCORE).Process(client);
            if(result.Score < 600)
                Assert.AreEqual("score", result.Refused_policy);
            else
                Assert.AreNotEqual("score", result.Refused_policy);
        }

        [TestMethod]
        public void TestCommitmentRefused()
        {
            Client client = new Client();
            client.Cpf = "09876543210";
            client.Amount = 1500;
            client.Income = 100;
            client.Terms = 6;
            client.Score = 700;
            var result = FactoryCreditPolice.CreateCreditPolice(EnumPolice.COMMITMENT).Process(client);
            Assert.AreEqual("commitment", result.Refused_policy);
        }

        [TestMethod]
        public void TestCommitmentApproved()
        {
            Client client = new Client();
            client.Cpf = "09876543210";
            client.Amount = 1500;
            client.Income = 1000;
            client.Terms = 6;
            client.Score = 700;
            var result = FactoryCreditPolice.CreateCreditPolice(EnumPolice.COMMITMENT).Process(client);
            Assert.IsNull(result.Refused_policy);
        }
    }
}
