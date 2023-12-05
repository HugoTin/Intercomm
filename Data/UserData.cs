using Dapper;

public class UserData: Database, IUserData
{
    public User? Login(User? User)
    {
        if (User.Login == null) User.Login = "";
        if (User.Password == null) User.Login = "";

        string query = "SELECT * FROM Users WHERE Login = @Login AND Password = @Password";

        User? user = connection.Query<User?>(query, new{Login = User.Login, Password = User.Password  }).FirstOrDefault();

        return user;
    }
}