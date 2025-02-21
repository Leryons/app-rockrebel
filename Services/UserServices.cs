namespace RockRebel.Services;

public class UserServices
{
    private readonly Database database;

    public UserServices(Database database)
    {
        this.database = database;

        database.sQLiteConnection.CreateTable<User>();
    }

    public async Task<User> LoginUser(string email, string password)
    {
        return  database.sQLiteConnection.Table<User>()
                                                   .Where(s => s.Email == email && s.Password == password)
                                                   .FirstOrDefault();
    }

    public async Task<bool> RegisterUser(string email, string name, string lastname, string password)
    {
        var existingUser = database.sQLiteConnection.Table<User>()
                                                 .Where(p => p.Email == email)
                                                 .FirstOrDefault();

        if (existingUser != null)
        {
            return false;
        }

        var newUser = new User
        {
            Email = email,
            Name = name,
            LastName = lastname,
            Password = password
        };

        database.sQLiteConnection.Insert(newUser);
        return true;
    }
}