using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PM2E12513.Controller;
using PM2E12513.Models;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Plugin.Connectivity;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.IO;
using PM02REC.Views;
using Xamarin.Forms.Xaml;

namespace PM2E12513
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        string base64 = "";
        public MainPage()
        {
            InitializeComponent();
            string name = Preferences.Get("users", "");
            string pass = Preferences.Get("pass", "");
            lbl.Text = "Hola " + name ;
            Localizar();
            CheckConnectivity();
        }

        private async void btncerrar_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("users");
            Preferences.Remove("pass");
            Preferences.Remove("Remember");
            Preferences.Set("Remember", true);
            //Preferences.Clear();



            await Application.Current.MainPage.Navigation.PopAsync();
            await Navigation.PushAsync(new Login());
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            ObtenerPosition();
        }

        private async void ObtenerPosition()
        {
            var ubicacionActual = CrossGeolocator.Current;

            if (ubicacionActual.IsGeolocationAvailable)
            {

                if (!ubicacionActual.IsGeolocationEnabled)
                {
                    DisplayAlert("Ubicación", "Debe encender la  ubicacion o GPS de su dispositivo", "Aceptar");

                }
            }
            else
            {
                DisplayAlert("Permiso denegado", "Para poder acceder a la localización debe pertir acceder a la ubicacion", "Aceptar");
            }
        }



        private async void Localizar()
        {
            var result = await Geolocation.GetLocationAsync(new
                GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));
            txtlatitud.Text = $"{ result.Latitude}";
            txtlongitud.Text = $"{ result.Longitude}";
        }

        private async void BtnCam_Clicked(object sender, EventArgs e)
        {
            var tomarfoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "miApp",
                Name = "Image.jpg"

            });

            if (tomarfoto != null)
            {
                imgCam.Source = ImageSource.FromStream(() => { return tomarfoto.GetStream(); });
            }

            Byte[] imagenByte = null;

            using (var stream = new MemoryStream())
            {
                tomarfoto.GetStream().CopyTo(stream);
                tomarfoto.Dispose();
                imagenByte = stream.ToArray();

                base64 = Convert.ToBase64String(imagenByte);
            }
        }


        private void CheckConnectivity()
        {
            if (CrossConnectivity.Current.IsConnected == false)
                DisplayAlert("Mensaje", "Dispositivo no conectado a Internet", "Ok");
        }

              



        private async void btn02_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ubicacionesPage());
        }

        private async void btnguardar_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(base64) == true)
            {
                await DisplayAlert("Mensaje", "Error, no hay imagen aun", "OK");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(this.txtlatitud.Text) == true)
                {
                    await DisplayAlert("Mensaje", "Datos incompletos", "OK");
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(this.txtlongitud.Text) == true)
                    {
                        await DisplayAlert("Mensaje", "Datos incompletos", "OK");
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(this.txtdescripcion.Text) == true)
                        {
                            await DisplayAlert("Mensaje", "Datos incompletos", "OK");
                        }
                        else
                        {
                            if (String.IsNullOrWhiteSpace(this.txtdcorta.Text) == true)
                            {
                                await DisplayAlert("Mensaje", "Datos incompletos", "OK");
                            }
                            else
                            {
                                try
                                {
                                    var ubicaciones = new Models.Ubicaciones
                                    {

                                        latitud = Convert.ToDouble(this.txtlatitud.Text),
                                        longitud = Convert.ToDouble(this.txtlongitud.Text),
                                        descripcion = this.txtdescripcion.Text,
                                        DescripcionCorta = this.txtdcorta.Text,
                                        Imagen = this.base64

                                    };

                                    var resultado = await App.Basedatos02.GrabarUbicacion(ubicaciones);


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
                }
            }
           
        }
    }
}
