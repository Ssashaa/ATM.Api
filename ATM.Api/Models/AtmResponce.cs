namespace ATM.Api.Models
{
    public class AtmResponce
    {
        public string Message { get; }

        public AtmResponce(string message)
        {
            Message = message;
        }
    }
}
