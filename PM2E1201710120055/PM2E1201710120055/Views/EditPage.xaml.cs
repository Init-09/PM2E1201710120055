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
using PM02REC.Views;
using Xamarin.Forms.Xaml;
using PM2E12513;
using PM2E12513.Views;

namespace PM02REC.Views
{

    public partial class EditPage : ContentPage
    {
        public int codigo;
        public double latitud, longitud;
        public string  imagen, larga, corta;
        
            public EditPage(Ubicaciones ubicacion)
            {
                InitializeComponent();
                Localizar();
                CheckConnectivity();

            codigo = ubicacion.codigo;
            latitud = ubicacion.latitud;
            longitud = ubicacion.longitud;
            imagen = ubicacion.Imagen;
            larga = ubicacion.descripcion;
            corta = ubicacion.DescripcionCorta;

            txtid.Text = Convert.ToString(ubicacion.codigo);           
            txtlatitud.Text = Convert.ToString(ubicacion.latitud);
            txtlongitud.Text = Convert.ToString(ubicacion.longitud);
            txtdescripcion.Text = ubicacion.descripcion;
            txtdcorta.Text = ubicacion.DescripcionCorta;
        }



        private async void tlbeliminar_Clicked(object sender, EventArgs e)

        {

            var ubicacion = new Ubicaciones
            {
                codigo = codigo,
                latitud = latitud,
                longitud = longitud,
                descripcion = larga,
                DescripcionCorta = corta,

            };

            if (await App.Basedatos02.EliminarUbicacion(ubicacion) != 0)
                await DisplayAlert("Eliminar Persona", "Persona Eliminada Correctamente", "Ok");
            else
                await DisplayAlert("Eliminar Persona", "Error al Eliminar Persona!!", "Ok");

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

       

        private void CheckConnectivity()
        {
            if (CrossConnectivity.Current.IsConnected == false)
                DisplayAlert("Mensaje", "Dispositivo no conectado a Internet", "Ok");
        }





        private async void btn02_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ubicacionesPage());
        }

        private async void btnguardar_Clicked(object sender, EventArgs e)
        {

            try
            {
                var ubicaciones = new PM2E12513.Models.Ubicaciones
                {
                    codigo = Convert.ToInt32(this.txtid.Text),
                    latitud = Convert.ToDouble(this.txtlatitud.Text),
                    longitud = Convert.ToDouble(this.txtlongitud.Text),
                    descripcion = this.txtdescripcion.Text,
                    DescripcionCorta = this.txtdcorta.Text,

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
