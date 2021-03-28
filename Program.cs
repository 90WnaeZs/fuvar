using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace fuvar
{
    class Fuvar
    {
        public int taxi_azonosító;
        public DateTime indulás;
        public int időtartam;
        public double megtett_távolság;
        public double viteldíj;
        public double borravaló;
        public string fizetés_módja;
    }

    class Program
    {
        string filePath = @"C:\Users\Zs\Desktop\fuvar.csv";
        string elsősor;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Beolvasás();
            Console.ReadLine();
        }

        private void Beolvasás() // 2. feladat
        {
            int számláló = 0;
            List<Fuvar> LIST_fuvar = new List<Fuvar>();

            using (StreamReader sr=new StreamReader(filePath))
            {
                elsősor = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string[] beolvas_tomb = sr.ReadLine().Split(';');

                    Fuvar f = new Fuvar();
                    f.taxi_azonosító = int.Parse(beolvas_tomb[0]);
                    f.indulás = Convert.ToDateTime(beolvas_tomb[1]);
                    f.időtartam = int.Parse(beolvas_tomb[2]);
                    f.megtett_távolság = double.Parse(beolvas_tomb[3]);
                    f.viteldíj = double.Parse(beolvas_tomb[4]);
                    f.borravaló = double.Parse(beolvas_tomb[5]);
                    f.fizetés_módja = beolvas_tomb[6];

                    LIST_fuvar.Add(f);
                    számláló++;
                }
            }

            // 3. feladat
            Console.WriteLine($"3. feladat: {számláló}"); 

            // 4. feladat
            double össz_bevétel = 0;
            int fuvar_számláló = 0;
            foreach (var fuvar in LIST_fuvar)
            {
                if(fuvar.taxi_azonosító==6185)
                {
                    fuvar_számláló++;
                    össz_bevétel += fuvar.viteldíj;
                    össz_bevétel += fuvar.borravaló;
                }
            }
            Console.WriteLine($"4. feladat: {fuvar_számláló} fuvar alatt: {össz_bevétel}");

            // 5. feladat
            Console.WriteLine("5. feladat: ");  
            List<string> LIST_fizetési_mód = new List<string>();
            foreach (var fuvar in LIST_fuvar)
            {
                if(!LIST_fizetési_mód.Contains(fuvar.fizetés_módja))
                {
                    LIST_fizetési_mód.Add(fuvar.fizetés_módja);
                }
            }
            foreach (var fizmod in LIST_fizetési_mód)
            {
                int a = 0;
                string fm = fizmod;
                foreach (var item2 in LIST_fuvar)
                {
                    if(fm == item2.fizetés_módja)
                    {
                        a++;
                    }
                }
                Console.WriteLine($"\t{fizmod} és {a}");
            }

            // 6. feladat
            double össz_táv=0;
            foreach (var fuvar in LIST_fuvar)
            {
                össz_táv += Math.Round((fuvar.megtett_távolság * 1.6),2);
            }
            Console.WriteLine($"6. feladat: {össz_táv}km");


            // 7. feladat
            Console.WriteLine("7. feladat: Leghosszab fuvar: ");
            int max = 0;
            Fuvar leghosszabb_fuvar = new Fuvar();
            foreach (var fuvar in LIST_fuvar)
            {
                if(fuvar.időtartam> max)
                {
                    max = fuvar.időtartam;
                }
            }

            foreach (var fuvar in LIST_fuvar)
            {
                if(fuvar.időtartam==max)
                {
                    Console.WriteLine($"\tFuvar hossza: {fuvar.időtartam} másodperc");
                    Console.WriteLine($"\tTaxi azonosító: {fuvar.taxi_azonosító}");
                    Console.WriteLine($"\tMegtett távolság: {fuvar.megtett_távolság} km");
                    Console.WriteLine($"\tViteldíj: {fuvar.viteldíj}$");
                }
            }

            // 8. feladat
            Console.WriteLine("8. feladat: hibak.txt");
            string hibatxt = @"C:\Users\Zs\Desktop\hibak.txt";
            using (StreamWriter sw = new StreamWriter(hibatxt))
            {
                
                sw.WriteLine(elsősor);
                LIST_fuvar.Sort((x, y) => DateTime.Compare(x.indulás, y.indulás));

                foreach (var fuvar in LIST_fuvar)
                {
                    if (fuvar.viteldíj > 0 && fuvar.időtartam > 0 && fuvar.megtett_távolság == 0)
                    {
                        sw.WriteLine($"{fuvar.taxi_azonosító};{fuvar.indulás};{fuvar.időtartam};{fuvar.megtett_távolság};{fuvar.viteldíj};{fuvar.borravaló};{fuvar.fizetés_módja}");
                    }
                }
            }
            



            /*var query = fuvar_list
            .SelectMany(x => x.fizetés_módja)
            .GroupBy(x => x, (y, z) => new { Name = y, Count = z.Count() });

            foreach (var item in query)
            {
                Console.WriteLine("{0} - {1}", item.Name, item.Count);
            }
            int distinct_count = fuvar_list.Distinct().Count();
            Console.WriteLine(distinct_count);*/

        }
    }
}
