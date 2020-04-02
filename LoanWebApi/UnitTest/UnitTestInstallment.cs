using LoanApi.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTestInstallment
    {
        [TestMethod]
        public void TestIntallmentTerms6Score700()
        {
            double instalment = CommitmentPolice.CalculoDaParcela(1000.00, 6, 700);
            int compare = Math.Round(instalment, 2).CompareTo(200.18);
            Assert.IsTrue(compare == 0);
        }

        [TestMethod]
        public void TestIntallmentTerms9Score700()
        {
            double instalment = CommitmentPolice.CalculoDaParcela(1000.00, 9, 700);
            int compare = Math.Round(instalment, 2).CompareTo(145.75);
            Assert.IsTrue(compare == 0);
        }

        [TestMethod]
        public void TestIntallmentTerms12Score700()
        {
            double instalment = CommitmentPolice.CalculoDaParcela(1000.00, 12, 700);
            int compare = Math.Round(instalment, 2).CompareTo(119.93);
            Assert.IsTrue(compare == 0);
        }

        [TestMethod]
        public void TestIntallmentTerms6Score800()
        {
            double instalment = CommitmentPolice.CalculoDaParcela(1000.00, 6, 800);
            int compare = Math.Round(instalment, 2).CompareTo(195.13);
            Assert.IsTrue(compare == 0);
        }

        [TestMethod]
        public void TestIntallmentTerms9Score800()
        {
            double instalment = CommitmentPolice.CalculoDaParcela(1000.00, 9, 800);
            int compare = Math.Round(instalment, 2).CompareTo(140.69);
            Assert.IsTrue(compare == 0);
        }

        [TestMethod]
        public void TestIntallmentTerms12Score800()
        {
            double instalment = CommitmentPolice.CalculoDaParcela(1000.00, 12, 800);
            int compare = Math.Round(instalment, 2).CompareTo(114.74);
            Assert.IsTrue(compare == 0);
        }

        [TestMethod]
        public void TestJurosTerms6Score900()
        {
            double juros = CommitmentPolice.CalculoDeJuros(6, 900);
            int compare = juros.CompareTo(0.039);
            Assert.IsTrue(compare == 0);
        }

        [TestMethod]
        public void TestJurosTerms9Score900()
        {
            double juros = CommitmentPolice.CalculoDeJuros(9, 900);
            int compare = juros.CompareTo(0.042);
            Assert.IsTrue(compare == 0);
        }
        [TestMethod]
        public void TestJurosTerms12Score900()
        {
            double juros = CommitmentPolice.CalculoDeJuros(12, 900);
            int compare = juros.CompareTo(0.045);
            Assert.IsTrue(compare == 0);
        }


        [TestMethod]
        public void TestJurosTerms6Score800()
        {
            double juros = CommitmentPolice.CalculoDeJuros(6, 800);
            int compare = juros.CompareTo(0.047);
            Assert.IsTrue(compare == 0);
        }

        [TestMethod]
        public void TestJurosTerms9Score800()
        {
            double juros = CommitmentPolice.CalculoDeJuros(9, 800);
            int compare = juros.CompareTo(0.050);
            Assert.IsTrue(compare == 0);
        }
        [TestMethod]
        public void TestJurosTerms12Score800()
        {
            double juros = CommitmentPolice.CalculoDeJuros(12, 800);
            int compare = juros.CompareTo(0.053);
            Assert.IsTrue(compare == 0);
        }
    }
}
