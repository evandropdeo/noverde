namespace LoanApi.Rules
{
    public class FactoryCreditPolice
    {

        public static ICreditPolice CreateCreditPolice(EnumPolice enumPolice)
        {
            ICreditPolice creditPolice = null;


            switch (enumPolice)
            {
                case EnumPolice.AGE:
                    creditPolice = FactoryPattern<ICreditPolice, AgePolice>.CreateInstance();
                    break;
                case EnumPolice.SCORE:
                    creditPolice = FactoryPattern<ICreditPolice, ScorePolice>.CreateInstance();
                    break;
                case EnumPolice.COMMITMENT:
                    creditPolice = FactoryPattern<ICreditPolice, CommitmentPolice>.CreateInstance();
                    break;
                default:
                    break;
            }

            return creditPolice;
        }
    }
}
