namespace SqlConnectionTests.Models
{
    public class NumberCollection2
        : NumberCollection1
    {
        public NumberCollection2()
        {

        }

        public NumberCollection2(int number1, int number2)
        {
            Number1 = number1;
            Number2 = number2;
        }

        //Core datum 2
        public int Number2 { get; set; }
    }
}
