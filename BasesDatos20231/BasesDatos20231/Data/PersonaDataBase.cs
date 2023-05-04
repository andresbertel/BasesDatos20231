using BasesDatos20231.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BasesDatos20231.Data
{
    public class PersonaDataBase
    {
        private readonly SQLiteAsyncConnection database;
        HttpClient client;

        public PersonaDataBase(String ruta)
        {

            client = new HttpClient();
            InitHttpClient();

            database = new SQLiteAsyncConnection(ruta);
            database.CreateTableAsync<Persona>().Wait();
        }

        private void InitHttpClient()
        {
            client.BaseAddress = new Uri("https://19f9-181-71-68-51.ngrok.io/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Persona>> BuscarTodos()
        {
            List<Persona> listadoPersona = null;
            HttpResponseMessage response = await client.GetAsync("api/Personas");

            if (response.IsSuccessStatusCode)
            {
              string  res = await  response.Content.ReadAsStringAsync();
              listadoPersona = JsonConvert.DeserializeObject<List<Persona>>(res);
                
            }
            return listadoPersona;

           /* return await database
                .Table<Persona>()
                .ToListAsync();*/
        }

        public async Task<Persona> BuscarUno(int id)
        {
            Persona persona = null;
            HttpResponseMessage response = await client.GetAsync($"api/Personas/{id}");

            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                persona = JsonConvert.DeserializeObject<Persona>(res);

            }
            return persona;

            /* return await database
                 .Table<Persona>()
                 .Where(X => X.Id == id)
                 .FirstOrDefaultAsync();*/
        }

        public async Task<int> EliminarPersona(Persona persona)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/personas/{persona.Id}");
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();
            return Convert.ToInt32(result);

            // return await database.DeleteAsync(persona);
        }

        public async Task<int> GuardarPersona(Persona persona)
        {

            string result = string.Empty;

            if (persona.Id != 0)
            {
              
                HttpResponseMessage response = await client.PutAsJsonAsync($"api/Personas/{persona.Id}", persona);
                response.EnsureSuccessStatusCode();

                result = await response.Content.ReadAsStringAsync();
                return Convert.ToInt32(result);

                // return await database.UpdateAsync(persona);
            }
            else
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Personas", persona);
                response.EnsureSuccessStatusCode();

                result = await response.Content.ReadAsStringAsync();              
                return Convert.ToInt32(result);

              //  return await database.InsertAsync(persona);
            }
        }


    }
}
