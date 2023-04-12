using BasesDatos20231.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasesDatos20231.Data
{
    public class PersonaDataBase
    {
        private readonly SQLiteAsyncConnection database;

        public PersonaDataBase(String ruta)
        {
            database = new SQLiteAsyncConnection(ruta);
            database.CreateTableAsync<Persona>().Wait();
        }

        public async Task<List<Persona>> BuscarTodos()
        {
            return await database
                .Table<Persona>()
                .ToListAsync();
        }

        public async Task<Persona> BuscarUno(int id)
        {
            return await database
                .Table<Persona>()
                .Where(X => X.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> EliminarPersona(Persona persona)
        {
            return await database.DeleteAsync(persona);
        }

        public async Task<int> GuardarPersona(Persona persona)
        {
            if (persona.Id != 0)
            {
                return await database.UpdateAsync(persona);
            }
            else
            {
                return await database.InsertAsync(persona);
            }
        }


    }
}
