namespace Lavyn.Domain
{
    public interface ICredential
    {
        string Login { get; }
        string Password { get; set; }
    }
}
