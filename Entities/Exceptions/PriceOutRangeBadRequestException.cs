namespace Entities.Exceptions
{
    public abstract partial class BadRequestException
    {
        public class PriceOutRangeBadRequestException : BadRequestException
        {
            public PriceOutRangeBadRequestException() : base("Maximum price should be less than 1000 and greater then 10.")
            {
            }
        }
    }
}