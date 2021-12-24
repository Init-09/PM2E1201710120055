using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PM02REC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMapa : ContentPage
    {
        public ViewMapa(double lat, double lon, string des_corta)
        {
            InitializeComponent();
            Position position = new Position(lat, lon);
            MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
            Xamarin.Forms.Maps.Map map = new Xamarin.Forms.Maps.Map(mapSpan);

            Pin pin = new Pin
            {
                Label = des_corta,
                Type = PinType.Place,
                Position = position
            };
            map.Pins.Add(pin);

            Content = map;
        }
    }
}