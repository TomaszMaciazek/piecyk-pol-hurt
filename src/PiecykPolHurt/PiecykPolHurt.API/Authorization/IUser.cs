namespace PiecykPolHurt.API.Authorization
{
    public interface IUser
    {
        public IEnumerable<string> Permissions { get; }

        public string Email { get; }
    }
}
