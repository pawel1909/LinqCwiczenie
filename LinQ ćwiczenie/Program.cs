using System;
using System.Collections.Generic;
using System.Linq;

namespace LinQ_ćwiczenie

    //We discussed 27 different LINQ methods, arranged into five categories:

    //    Methods to extract a single element from a sequence: First(), Last(), Single(), FirstOrDefault(), LastOrDefault(), and SingleOrDefault()
    //    Methods to extract multiple elements from a sequence: Skip(), Take(), SkipWhile(), TakeWhile(), Distinct(), Intersect(), and Where()
    //    Methods to change the order of the elements in a sequence: Reverse(), OrderBy() and ThenBy()
    //    Methods to calculate a single value based on a sequence: Count(), Sum(), Min(), Max(), Any(), All(), SequenceEqual(), and Aggregate()
    //    Methods to calculate a new sequence based on a source sequence: Cast<U>(), Select(), and SelectMany()

{
    class Program
    {
        static void Main(string[] args)
        {
            //https://e.wsei.edu.pl/pluginfile.php/65811/mod_resource/content/2/lab-linq-string-sortowanie-wg-nazwisk-pozniej-imion.html
            Console.WriteLine("hemloł");
            //krok1_1();
            //krok1_2();
            //krok1_3();
            //krok2_1();
            //krok2_2();
            //krok2_3();
            //zadanie_1_1();

            string s = "4:12,2:43,3:51,4:29,3:24,3:14,4:46,3:25,4:52,3:27";
            //string s = Console.ReadLine();
            var query = s.Split(',')
                .Select(o => o.Split(':'))
                .Select(x => (minuta: Int32.Parse(x[0]), sekunda: Int32.Parse(x[1])));

            double minuty = query.Sum(o => o.minuta);
            double sekundy = query.Sum(o => o.sekunda);
            double srednia = Math.Ceiling((minuty * 60 + sekundy) / query.Count());
            double godziny = 0;
            if (sekundy >= 60)
            {
                minuty += sekundy / 60;
                sekundy = sekundy % 60;
            }
            if (minuty >= 60)
            {
                godziny = minuty / 60;
                minuty = minuty % 60;
            }
            var l2 = $"{query.Count()},{(int)srednia / 60}:{(int)srednia % 60},{(int)godziny}:{(int)minuty}:{(int)sekundy}"
                .Split(',')
                .Select(x => x.Split(':'))
                .Select(x => x.SkipWhile(v => int.Parse(v) == 0))
                .Select(o => String.Join(":", o));


            //List<string> l = new List<string>();
            //l.Add($"{query.Count()}");
            //l.Add($"{(int)srednia/60}:{(int)srednia%60}");
            //l.Add($"{(int)godziny}:{(int)minuty}:{(int)sekundy}");


            
                //.Select(o => (lUtworow: o[0], srednia: o[1], dlugosc: o[2]))
                //.Select(x => x.lUtworow + " " + x.srednia + " " + x.dlugosc);
            
            

            
                
            //.Select(x => (minuta: Int32.Parse(x[0]), sekunda: Int32.Parse(x[1])))
            //.Select((x) => x.minuta + " " + x.minuta);
            

            

            string wynik = String.Join(" ", l2);

            



            Console.WriteLine(wynik);




        }
        static void zadanie_1_1()// notacja flow
        {
            string s = "Krzysztof Molenda, 1965-11-20; Jan Kowalski, 1987-01-01; Anna Abacka, 1972-05-20; Józef Kabacki, 2000-01-02; Kazimierz Moksa, 2001-01-02";
            var query = s.Split(';')
                .Select(osoba => osoba.Trim())
                .Select(osoba => osoba.Split(' '))
                .Select(x => (imie: x[0], nazwisko: x[1], data: x[2]))
                .OrderBy(o => o.data)
                .ThenBy(o => o.nazwisko)
                .Select(o => o.imie + " " + o.nazwisko.Trim(',') + " " + o.data);


            string wynik = String.Join(" x ", query);
            Console.WriteLine(wynik);
        }


        static void krok1_1()
        {
            string s = "Krzysztof Molenda,  Jan Kowalski, Anna Abacka , Józef Kabacki, Kazimierz Moksa";
            var listaOsob = new List<Osoba>();
            string[] osoby = s.Split(',');

            for (int i = 0; i < osoby.Length; i++)
            {
                osoby[i] = osoby[i].Trim();
                string[] temp = osoby[i].Split(' ');
                Osoba o = new Osoba();
                o.Imie = temp[0];
                o.Nazwisko = temp[1];
                listaOsob.Add(o);
            }

            foreach (var item in listaOsob)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            listaOsob.Sort((o1, o2) => String.Compare(o1.Nazwisko + o1.Imie, o2.Nazwisko + o2.Imie));

            string wynik = "";

            foreach (var item in listaOsob)
            {
                wynik += item.Imie + " " + item.Nazwisko + ", ";
            }

            Console.WriteLine(wynik);
        }

        static void krok1_2()
        {
            string s = "Krzysztof Molenda,  Jan Kowalski, Anna Abacka , Józef Kabacki, Kazimierz Moksa";

            var listaOsob = new List<Tuple<string, string>>();

            string[] osoby = s.Split(',');
            for (int i = 0; i < osoby.Length; i++)
            {
                osoby[i] = osoby[i].Trim();
                string[] temp = osoby[i].Split(' ');
                var o = new Tuple<string, string>(temp[0], temp[1]);
                listaOsob.Add(o);
            }

            listaOsob.Sort((o1, o2) => String.Compare(o1.Item2 + o1.Item1, o2.Item2 + o2.Item1));
            string wynik = String.Join(",", listaOsob);
            Console.WriteLine(wynik);
        }
        static void krok1_3()
        {
            string s = "Krzysztof Molenda,  Jan Kowalski, Anna Abacka , Józef Kabacki, Kazimierz Moksa";

            var OsobaExamle = new { imie = "", nazwisko = "" };
            var listaOsob = (new[] { OsobaExamle }).ToList();
            listaOsob.RemoveAt(0); //lista jest pusta
            string[] osoby = s.Split(',');
            for (int i = 0; i < osoby.Length; i++)
            {
                osoby[i] = osoby[i].Trim();
                string[] temp = osoby[i].Split(' ');
                var o = new { imie = temp[0], nazwisko = temp[1] };
                listaOsob.Add(o);
            }
            listaOsob.Sort((o1, o2) => String.Compare(o1.nazwisko + o1.imie, o2.nazwisko + o2.imie));

            string wynik = string.Join(",", listaOsob);

            Console.WriteLine(wynik);
        }
        static void krok2_1()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string s = "Krzysztof Molenda,  Jan Kowalski, Anna Abacka , Józef Kabacki, Kazimierz Moksa";

            var query1 = s.Split(','); //rozbijanie na osoby
            var query2 = query1.Select(osoba => osoba.Trim()); //usuwanie spacji

            //drukowanie
            query2.ToList().ForEach(x => { Console.Write(x + ", "); });
            Console.WriteLine();

            var query3 = query2
                .Select(osoba => osoba.Split(' '))
                .Select(x => (imie: x[0], nazwisko: x[1]));

            query3.ToList().ForEach(x => { Console.Write(x + ", "); });
            Console.WriteLine();

            var query4 = query3
                .OrderBy(o => o.nazwisko)
                .ThenBy(o => o.imie);
            // drukujemy
            query4.ToList().ForEach(x => { Console.Write(x + ", "); });
            Console.WriteLine();

            var query5 = query4.Select(o => o.imie + " " + o.nazwisko);
            //drukujemy
            query5.ToList().ForEach(x => { Console.Write(x + ", "); });
            Console.WriteLine();

            string wynik = String.Join(", ", query5);
            Console.WriteLine(wynik);
        }
        static void krok2_2() //jedna kwerenda, notacja flow
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string s = "Krzysztof Molenda,  Jan Kowalski, Anna Abacka , Józef Kabacki, Kazimierz Moksa";
            
            var query = s.Split(',')                                    //rozbijamy na osoby -> tablica napisów
              .Select(osoba => osoba.Trim())              //usuwamy spacje
              .Select(osoba => osoba.Split(' '))          //rozbijamy na wyrazy -> sekwencja tablic 2-elementowych
              .Select(x => (imie: x[0], nazwisko: x[1]))  //sekwencja obiektów anonimowych (imie, nazwisko)
              .OrderBy(o => o.nazwisko)                   //sortujemy wg nazwiska,
              .ThenBy(o => o.imie)                        //... a następnie wg imienia
              .Select(o => o.imie + " " + o.nazwisko);    //sekwencja naspisów postaci imie-spacja-nazwisko

            string wynik = String.Join(", ", query);
            Console.WriteLine(wynik);
        }
        static void krok2_3() // notacja query
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string s = "Krzysztof Molenda,  Jan Kowalski, Anna Abacka , Józef Kabacki, Kazimierz Moksa";

            var query = s.Split(',')
                         .Select(x => x.Trim())
                         .Select(x => x.Split(' '));
            //.Select( x => (imie: x[0], nazwisko: x[1]) ) ;

            var query1 =
                from osoba in query
                let imie = osoba[0]
                let nazwisko = osoba[1]
                orderby nazwisko, imie
                select new { imie, nazwisko };

            //string wynik = "";
            //foreach( var osoba in query1 )
            //    wynik += osoba.imie + " " + osoba.nazwisko + ", ";

            string wynik = String.Join(", ", query1.Select(o => o.imie + " " + o.nazwisko));
            Console.WriteLine(wynik);
        }

        //static void zadanie_1_1()// notacja flow
        //{
        //    string s = "Krzysztof Molenda, 1965-11-20; Jan Kowalski, 1987-01-01; Anna Abacka, 1972-05-20; Józef Kabacki, 2000-01-02; Kazimierz Moksa, 2001-01-02";
        //    var query = s.Split(';')
        //        .Select(osoba => osoba.Trim())
        //        .Select(osoba => osoba.Split(' '))
        //        .Select(x => (imie: x[0], nazwisko: x[1], data: x[2]))
        //        .OrderBy(o => o.data)
        //        .ThenBy(o => o.nazwisko)
        //        .Select(o => o.imie + " " + o.nazwisko.Trim(',') + " " + o.data);


        //    string wynik = String.Join(" x ", query);
        //    Console.WriteLine(wynik);
        //}
        static void zadanie_1_2() //notacja query
        {
            
            string s = "Krzysztof Molenda, 1965-11-20; Jan Kowalski, 1987-01-01; Anna Abacka, 1972-05-20; Józef Kabacki, 2000-01-02; Kazimierz Moksa, 2001-01-02";

            var query = s.Split(';')
                .Select(x => x.Trim())
                .Select(x => x.Split(' '));

            var query1 =
                from osoba in query
                let imie = osoba[0]
                let nazwisko = osoba[1].Trim(',')
                let data = osoba[2]
                orderby data, nazwisko
                select new { imie, nazwisko, data };

            string wynik = String.Join(", ", query1.Select(o => o.imie + " " + o.nazwisko + " " + o.data));
            Console.WriteLine(wynik);
        }
    }
}
