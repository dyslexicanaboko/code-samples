namespace SqlConnectionTests.Models
{
    public class NumberCollection2
        : NumberCollection1
    {
        public int Number2 { get; set; }

        public override void SetNumbers(params int[] numbers)
        {
            Number2 = numbers[1];

            base.SetNumbers(numbers);
        }
    }

    public class NumberCollection3
        : NumberCollection2
    {
        public int Number3 { get; set; }

        public override void SetNumbers(params int[] numbers)
        {
            Number3 = numbers[2];

            base.SetNumbers(numbers);
        }
    }

    public class NumberCollection4
        : NumberCollection3
    {
        public int Number4 { get; set; }

        public override void SetNumbers(params int[] numbers)
        {
            Number4 = numbers[3];

            base.SetNumbers(numbers);
        }
    }

    public class NumberCollection5
        : NumberCollection4
    {
        public int Number5 { get; set; }

        public override void SetNumbers(params int[] numbers)
        {
            Number5 = numbers[4];

            base.SetNumbers(numbers);
        }
    }

    public class NumberCollection6
        : NumberCollection5
    {
        public int Number6 { get; set; }

        public override void SetNumbers(params int[] numbers)
        {
            Number6 = numbers[5];

            base.SetNumbers(numbers);
        }
    }

    public class NumberCollection7
        : NumberCollection6
    {
        public int Number7 { get; set; }

        public override void SetNumbers(params int[] numbers)
        {
            Number7 = numbers[6];

            base.SetNumbers(numbers);
        }
    }

    public class NumberCollection8
        : NumberCollection7
    {
        public int Number8 { get; set; }

        public override void SetNumbers(params int[] numbers)
        {
            Number8 = numbers[7];

            base.SetNumbers(numbers);
        }
    }

    public class NumberCollection9
        : NumberCollection8
    {
        public int Number9 { get; set; }

        public override void SetNumbers(params int[] numbers)
        {
            Number9 = numbers[8];

            base.SetNumbers(numbers);
        }
    }

    public class NumberCollection10
        : NumberCollection9
    {
        public int Number10 { get; set; }

        public override void SetNumbers(params int[] numbers)
        {
            Number10 = numbers[9];

            base.SetNumbers(numbers);
        }
    }
}