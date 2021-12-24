using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using SQLite;
using PM02REC.Views;

namespace PM2E12513.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ubicacionesPage : ContentPage
    {
        public ubicacionesPage()
        {
            InitializeComponent();


           
        }

        // mostras lista de personas 

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var listaubicaciones = await App.Basedatos02.ObtenerListaUbicaciones();
            lstUbicaciones.ItemsSource = listaubicaciones;
            
        }

        private async void tlbeliminar_Clicked(object sender, EventArgs e)
        
        {


            var ubicacion = new Models.Ubicaciones
            {
                codigo = Convert.ToInt32(this.txtcodigo.Text),
                latitud = Convert.ToDouble(this.txtlatitud.Text),
                longitud = Convert.ToDouble(this.txtlongitud.Text),
                descripcion = this.txtdescripcion.Text,
                DescripcionCorta = this.txtdcorta.Text,
              
            };           

            await Navigation.PushAsync(new EditPage(ubicacion));

        }

        public async void lstUbicaciones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Models.Ubicaciones item = (Models.Ubicaciones)e.Item;
            //await DisplayAlert("Elemento Tocado " , "correo: " + item.DescripcionCorta, "Ok");

            this.txtcodigo.Text = Convert.ToString( item.codigo);
            this.txtlatitud.Text = Convert.ToString(item.latitud);
            this.txtlongitud.Text = Convert.ToString(item.longitud);
            this.txtdescripcion.Text = Convert.ToString(item.descripcion);
            this.txtdcorta.Text = Convert.ToString(item.DescripcionCorta);


        }


        private async void tlbmostrar_Clicked(object sender, EventArgs e)
        {
            var lat = Convert.ToDouble(txtlatitud.Text);
            var log = Convert.ToDouble(txtlongitud.Text);
            var descricion = txtdcorta.Text;

            
            MapLaunchOptions options = new MapLaunchOptions { Name = descricion };
            await Map.OpenAsync(lat, log, options);
        }

        private async void tlbver_Clicked(object sender, EventArgs e)
        {           


            var lat = Convert.ToDouble(txtlatitud.Text);
            var log = Convert.ToDouble(txtlongitud.Text);
            var descricion = txtdcorta.Text;

            await Navigation.PushAsync(new ViewMapa(lat, log, descricion));

        }
    }
    

    
}