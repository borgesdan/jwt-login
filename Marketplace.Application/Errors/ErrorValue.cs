namespace Marketplace.Application.Errors
{
    public class ErrorValue
    {
        public string Message;
        public string Value;

        public ErrorValue(string value, string message)
        {
            Message = message;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}: {Message}";
        }
    }
}
