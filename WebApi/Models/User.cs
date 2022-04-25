namespace WebApi.Models;

public class User
{
    public User(string userId, params string[] groups)
    {
        UserId = userId;
        Groups = groups;
    }

    public string UserId { get; }

    public IEnumerable<string> Groups { get; }
}