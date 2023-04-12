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
    public partial class Principal : ContentPage
    {
        public Persona Persona { get; set; }

        public Principal()
        {
            InitializeComponent();
            Persona = new Persona();

            BindingContext = this;
        }

        private async void GuardarActualizar(object sender, EventArgs e)
        {
        LlenarPersona();

         int result = await App.PersonaDataBase.GuardarPersona(Persona);

            if (result == 0)
            {
                await DisplayAlert("Error..", "Persona no guardada / Actualizada", "Cerrar");
            }
            else
            {
                await DisplayAlert("Exito..", "Persona guardada / Actualizada", "Cerrar");
            }
        }

        private void LlenarPersona()
        {
            Persona.Id = Convert.ToInt32(EntryId.Text);
            Persona.Name = EntryNombre.Text;
            Persona.Apellido = EntryApellido.Text;
        }

        private async void Eliminar(object sender, EventArgs e)
        {
            Persona personaBuscar = await Buscar();

            if (personaBuscar != null)
            {
                int result = await App.PersonaDataBase.EliminarPersona(personaBuscar);

                if (result == 0)
                {
                    await DisplayAlert("Error..", "Persona no eliminada", "Cerrar");
                }
                else
                {
                    await DisplayAlert("Exito..", "Persona eliminada", "Cerrar");
                }
            }
            else
            {
              await  DisplayAlert("Buscando..","Persona no encontrada","Cerrar");
            }
        }

        private async void BuscarTodos(object sender, EventArgs e)
        {
            List<Persona> listadoPersonas =  await App.PersonaDataBase.BuscarTodos();
            await Navigation.PushAsync(new TodosView(listadoPersonas));

        }

        private async void BuscarUno(object sender, EventArgs e)
        {
            Persona personaBuscar =  await Buscar();

            if (personaBuscar != null)
            {
             EntryId.Text = personaBuscar.Id.ToString();
             EntryNombre.Text = personaBuscar.Name;
             EntryApellido.Text = personaBuscar.Apellido;
            }
            else
            {
                await DisplayAlert("Buscando..", "Persona no encontrada", "Cerrar");
            }

        }

        private async Task<Persona> Buscar()
        {

           int idPersona = Convert.ToInt32(EntryId.Text);
           Persona persona = await App.PersonaDataBase.BuscarUno(idPersona);
            return persona;
        }
      
    }
}