using System;
using System.Collections.Generic;
using System.Text;

namespace LinQ_ćwiczenie
{
    public class Osoba
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public override string ToString()
        {
            return $"{Imie}; {Nazwisko}";
        }
    }
}
