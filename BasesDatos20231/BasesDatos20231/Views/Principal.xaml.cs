using BasesDatos20231.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BasesDatos20231.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal : ContentPage, INotifyPropertyChanged
    {
        private Persona _persona;
        public Persona Persona
        { 
            get
            {
                return _persona;
            }

            set
            {
                _persona = value;
                NotifyPropertyChanged();
            }
        }

        public Principal()
        {
            InitializeComponent();
            Persona = new Persona();

            BindingContext = this;
        }

        private async void GuardarActualizar(object sender, EventArgs e)
        {
        //LlenarPersona();

         int result = await App.PersonaDataBase.GuardarPersona(Persona);

            if (result == 0)
            {
                await DisplayAlert("Error..", "Persona no guardada / Actualizada", "Cerrar");
                Persona = new Persona();
            }
            else
            {
                await DisplayAlert("Exito..", "Persona guardada / Actualizada", "Cerrar");
            }
        }

        private void LlenarPersona()
        {
            Persona.Id = Convert.ToInt32(EntryId.Text);
            Persona.Nombres = EntryNombre.Text;
            Persona.Apellidos = EntryApellido.Text;
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
            Persona personaBuscar = await Buscar();
 

            if (personaBuscar != null)
            {
            

                this.Persona = new Persona
                {
                    Id = personaBuscar.Id,
                    Apellidos = personaBuscar.Apellidos,
                    Nombres = personaBuscar.Nombres
                };
                    
                    
                  
            /* EntryId.Text = personaBuscar.Id.ToString();
             EntryNombre.Text = personaBuscar.Name;
             EntryApellido.Text = personaBuscar.Apellido;*/
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



        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}