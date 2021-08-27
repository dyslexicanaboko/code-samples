namespace SqlConnectionTests.Models
{
    public class NumberCollection1
    {
        public NumberCollection1()
        {

        }

        public virtual void SetNumbers(params int[] numbers)
        {
            Number1 = numbers[0];
        }

        //Primary key, use ignore attribute or don't include in the model
        public int NumberCollectionId { get; set; }
        
        //Core datum
        public int Number1 { get; set; }
        
        //Use a custom ignore attribute or don't include in the model
        //public DateTime CreatedOnUtc { get; set; }
    }
}
