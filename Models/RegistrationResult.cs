using Book_store_1_.Models;

public class RegistrationResult
{
    public string Message { get; }
    public ApplicationUser User { get; }

    public RegistrationResult(string message)
    {
        Message = message;
    }

    public RegistrationResult(string message, ApplicationUser user) : this(message)
    {
        User = user;
    }
}