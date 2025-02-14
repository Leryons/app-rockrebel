namespace RockRebel.ViewModels;

public partial class UserViewModel : ObservableObject
{
    private readonly UserServices userServices;

    public UserViewModel(UserServices userServices)
    {
        this.userServices = userServices;
    }

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _lastName;

    [ObservableProperty]
    private string _password;

    [RelayCommand] //Find user
    public async Task LoginUserAsync()
    {
        try
        {
            var user = await userServices.LoginUser(_email, _password);

            if (user != null)
            {

                await Shell.Current.GoToAsync("HomePage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Credenciales incorrectas", "OK");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", "Ocurrió un error al iniciar sesión", "OK");
        }
    }

    [RelayCommand] //Add new user
    public async Task RegisterUserAsync()
    {
        try
        {
            bool isRegistered = await userServices.RegisterUser(_email, _name, _lastName, _password);

            if (isRegistered)
            {
                await Shell.Current.DisplayAlert("Éxito", "Registro completado", "OK").ContinueWith(async _ => await Shell.Current.GoToAsync("//LoginPage"));
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "El usuario ya existe", "OK");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }
}
