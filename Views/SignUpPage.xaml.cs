namespace RockRebel.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(UserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}