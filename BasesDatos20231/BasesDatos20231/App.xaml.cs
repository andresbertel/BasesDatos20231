using BasesDatos20231.Data;
using BasesDatos20231.Dependency;
using BasesDatos20231.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BasesDatos20231
{
    public partial class App : Application
    {

        private static PersonaDataBase _personaDataBase;

        public static PersonaDataBase PersonaDataBase
        {
            get
            {
                if (_personaDataBase == null)
                {
                    return _personaDataBase = new PersonaDataBase(DependencyService.Get<IRuta>()
                     .getRutaDB("BaseDatos.db3"));
                }
                else
                {
                    return _personaDataBase;
                }

            }
        }


        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Principal());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
