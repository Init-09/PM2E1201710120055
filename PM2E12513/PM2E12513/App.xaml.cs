using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E12513.Controller;
using PM2E12513.Models;
using System.IO;
using PM02REC.Views;
using Xamarin.Essentials;

namespace PM2E12513
{
    public partial class App : Application
    {

        static BaseUsers basedatos002;

        public static BaseUsers Basedatos021
        {
            get
            {

                if (basedatos002 == null)
                {
                    basedatos002 = new BaseUsers(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PMUsers.db3"));
                }

                return basedatos002;

            }


        }


        static BaseSQLite basedatos;

        public static BaseSQLite Basedatos02
        {
            get
            {

                if (basedatos == null)
                {
                    basedatos = new BaseSQLite(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PM2E12513.db3"));
                }

                return basedatos;

            }


        }

        

        public App()
        {
            InitializeComponent();


            if ((Preferences.Get("Remember", true) == true))
            {
                MainPage = new NavigationPage(new Login());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {

            if ((Preferences.Get("Remember", true) == true))
            {
                MainPage = new NavigationPage(new Login());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
