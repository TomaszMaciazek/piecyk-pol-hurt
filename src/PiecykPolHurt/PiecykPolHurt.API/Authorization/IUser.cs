namespace PiecykPolHurt.API.Authorization;

public interface IUser
{
    public string Email { get; }
    IEnumerable<string> Permissions { get; }
}