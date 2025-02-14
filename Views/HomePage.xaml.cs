namespace RockRebel.Views;

public partial class HomePage : ContentPage
{
	public HomePage(SongViewModel songViewModel)
	{
		InitializeComponent();
		BindingContext = songViewModel;
    }
}