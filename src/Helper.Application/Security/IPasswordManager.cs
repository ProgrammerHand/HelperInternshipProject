namespace Helper.Application.Security
{
    public interface IPasswordManager
    {
        string Hash(string password);
        bool Validate(string password, string hashedPassword);
    }
}
