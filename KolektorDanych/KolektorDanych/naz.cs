using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolektorDanych
{
    class naz
    {
        public string imie = "Jan";
        public string nazwisko = "Kowalski";
        public string wiek = "60";

        public string razem()
        {
            string razem = imie + "**" + nazwisko + "  " + wiek;
            return razem;
        }
        
    }
}
