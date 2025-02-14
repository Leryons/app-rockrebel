namespace RockRebel.Services;

public class UserServices
{
    private readonly Database database;

    public UserServices(Database database)
    {
        this.database = database;

        database.sQLiteAsyncConnection.CreateTableAsync<User>().Wait();
    }

    public async Task<User> LoginUser(string email, string password)
    {
        return await database.sQLiteAsyncConnection.Table<User>()
                                                   .Where(s => s.Email == email && s.Password == password)
                                                   .FirstOrDefaultAsync();
    }

    public async Task<bool> RegisterUser(string email, string name, string lastname, string password)
    {
        var existingUser = await database.sQLiteAsyncConnection.Table<User>()
                                                 .Where(p => p.Email == email)
                                                 .FirstOrDefaultAsync();

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

        await database.sQLiteAsyncConnection.InsertAsync(newUser);
        return true;
    }
}