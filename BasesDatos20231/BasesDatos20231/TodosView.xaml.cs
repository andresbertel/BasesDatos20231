using BasesDatos20231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BasesDatos20231.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodosView : ContentPage
    {
        public List<Persona> ListadoPersonas { get; set; }

        public TodosView(List<Persona> listado)
        {
            InitializeComponent();

            ListadoPersonas = listado;


            BindingContext = this;
        }
    }
}