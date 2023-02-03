namespace PiecykPolHurt.API.Authorization;

public interface IUser
{
    public int? Id { get; }
    public string Email { get; }
    IEnumerable<string> Permissions { get; }
}