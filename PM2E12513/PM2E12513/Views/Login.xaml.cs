using PM2E12513;
using PM2E12513.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM02REC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void registrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrar());
        }

        protected override bool OnBackButtonPressed()
        {

            base.OnBackButtonPressed();
            return true;

        }


        private async void ingresar_Clicked(object sender, EventArgs e)
        {



            if (String.IsNullOrWhiteSpace(login.Text) == true)
            {
                await DisplayAlert("Alerta", "El nombre es un campo obligatorio", "Ok");

            }
            else
            {
                if (String.IsNullOrWhiteSpace(pass.Text) == true)
                {
                    await DisplayAlert("Alerta", "La contraseña es un campo obligatorio", "Ok");

                }
                else
                {
                    Preferences.Set("users", login.Text);
                    Preferences.Set("pass", pass.Text);
                    Preferences.Set("Remember", false);

                    await Navigation.PushAsync(new MainPage());
                }
            }

        }
    }
}