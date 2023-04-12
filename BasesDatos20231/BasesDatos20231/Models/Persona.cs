using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasesDatos20231.Models
{
    public class Persona
    {
       [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public String Name { get; set; }

        public String Apellido { get; set; }

    }
}
