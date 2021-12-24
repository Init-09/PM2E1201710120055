using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using PM2E12513.Controller;
using PM2E12513.Models;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Plugin.Connectivity;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.IO;
using Xamarin.Forms.Xaml;
using PM2E12513;

namespace PM02REC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrar : ContentPage
    {
        public Registrar()
        {
            InitializeComponent();
        }
        private async void btnguardar_Clicked(object sender, EventArgs e)
        {
            
            try
            {
                var ubicaciones = new PM2E12513.Models1.Users
                {
                    
                    nombre = this.login.Text,
                    constrasenia = this.pass.Text,
                };

                var resultado = await App.Basedatos021.GrabarUbicacion(ubicaciones);


                if (resultado == 1)
                {
                    await DisplayAlert("Mensaje", "Ingresada Exitosamente", "OK");
                }
                else
                {
                    await DisplayAlert("Mensaje", "Error, No se logro guardar Ubicacion", "OK");

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje", ex.Message.ToString(), "OK");

            }
        }


    }
}