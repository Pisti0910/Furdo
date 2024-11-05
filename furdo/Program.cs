using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace furdo
{
    internal class Program
    {
        struct Tvendeg
        {
            public int id { get; set; }
            public int reszleg { get; set; }
            public int kibe { get; set; }
            public int ora { get; set; }
            public int perc { get; set; }
            public int mp { get; set; }
        }
        static Tvendeg[] data = new Tvendeg[800];
        static int n = 0;
        static void Main(string[] args)
        {
            F1();
            F2();
            F3();
            F4();
            F5();
            F6();
            F7();
            
            Console.ReadKey();
        }

        private static void F1()
        {
            StreamReader sr = new StreamReader("furdoadat.txt", Encoding.UTF8);
            while (sr.Peek() > -1)
            {
                string[] egysor = sr.ReadLine().Split(' ');
                data[n].id = Convert.ToInt32(egysor[0]);
                data[n].reszleg = Convert.ToInt32(egysor[1]);
                data[n].kibe = Convert.ToInt32(egysor[2]);
                data[n].ora = Convert.ToInt32(egysor[3]);
                data[n].perc = Convert.ToInt32(egysor[4]);
                data[n].mp = Convert.ToInt32(egysor[5]);
                n++;
            }
            sr.Close();
        }

        private static void F2()
        {
            Console.WriteLine("\n2. feladat");
            Console.WriteLine($"Az első vendég {data[0].ora}:{data[0].perc}:{data[0].mp}-kor lépett ki az öltözőből.");
            int utolso = data[n - 1].id;
            int i = n - 1;
            while (utolso == data[i].id)
            {
                i--;
            }
            Console.WriteLine($"Az utolsó vendég {data[i + 1].ora}:{data[i + 1].perc}:{data[i + 1].mp}-kor lépett ki az öltözőből.");
        }

        private static void F3()
        {
            Console.WriteLine("\n3. feladat");
            int db = 0;
            int dbkibe = 1;
            for (int i = 1; i < n; i++)
            {
                if (data[i].id == data[i - 1].id)
                    dbkibe++;
                else
                {
                    if (dbkibe == 4)
                        db++;
                    dbkibe = 1;
                }
            }
            Console.WriteLine($"A fürdőben {db} vendég járt csak egy részlegen.");
        }

        private static void F7()
        {
            Console.WriteLine("\n7. feladat");
            int db1 = 0;
            int db2 = 0;
            int db3 = 0;
            int db4 = 0;
            bool voltott1 = false;
            bool voltott2 = false;
            bool voltott3 = false;
            bool voltott4 = false;
            int i = 0;
            while (i < n)
            {
                while (i + 1 < n && data[i].id == data[i + 1].id)
                {
                    if (data[i].reszleg == 1 && data[i].kibe == 0 && !voltott1)
                    {
                        db1++;
                        voltott1 = true;
                    }
                    if (data[i].reszleg == 2 && data[i].kibe == 0 && !voltott2)
                    {
                        db2++;
                        voltott2 = true;
                    }
                    if (data[i].reszleg == 3 && data[i].kibe == 0 && !voltott3)
                    {
                        db3++;
                        voltott3 = true;
                    }
                    if (data[i].reszleg == 4 && data[i].kibe == 0 && !voltott4)
                    {
                        db4++;
                        voltott4 = true;
                    }
                    i++;
                }
                i++;
                voltott1 = false;
                voltott2 = false;
                voltott3 = false;
                voltott4 = false;
            }
            Console.WriteLine($"Uszoda: {db1}");
            Console.WriteLine($"Szaunák: {db2}");
            Console.WriteLine($"Gyógyvizes medencék: {db3}");
            Console.WriteLine($"Strand: {db4}");
        }

        private static void F6()
        {
            StreamWriter sw = new StreamWriter("szauna.txt");
            int i = 0;
            int ido = 0;
            int o = 0;
            int p = 0;
            int mp = 0;
            while (i < n)
            {
                ido = 0;
                while (i + 1 < n && data[i].id == data[i + 1].id)
                {
                    if (data[i].reszleg == 2 && data[i].kibe == 0)
                    {
                        ido += tomp(data[i + 1].ora, data[i + 1].perc, data[i + 1].mp) - tomp(data[i].ora, data[i].perc, data[i].mp);
                        i++;
                    }
                    i++;
                }
                if (ido > 0)
                {
                    o = ido / 3600;
                    ido = ido - o * 3600;
                    p = ido / 60;
                    ido = ido - p * 60;
                    mp = ido;
                    sw.WriteLine($"{data[i].id} {o}:{p}:{mp}");
                }
                i++;
            }
            sw.Close();
        }

        private static int tomp(int a, int b, int c)
        {
            return a * 3600 + b * 60 + c;
        }

        private static void F5()
        {
            Console.WriteLine("\n5. feladat");
            int db1 = 0;
            int db2 = 0;
            int db3 = 0;
            for (int i = 0; i<n;i++)
            {
                if (data[i].reszleg == 0 && data[i].kibe == 1)
                    if (data[i].ora < 9)
                        db1++;
                    else if (data[i].ora < 16)
                        db2++;
                    else
                        db3++;
                Console.WriteLine($"6-9 óra között {db1} vendég");
                Console.WriteLine($"9-16 óra között {db2} vendég");
                Console.WriteLine($"16-20 óra között {db3} vendég");
            }
        }

        private static void F4()
        {
            Console.WriteLine("\n4. feladat");
            Console.WriteLine("A legtöbb időt eltöltő vendég:");
            int maxv = 0;
            int i = 0;
            TimeSpan maxido = new TimeSpan(0, 0, 0);
            TimeSpan belep;
            TimeSpan kilep;
            i = 0;
            while (i < n)
            {
                belep = new TimeSpan(data[i].ora, data[i].perc, data[i].mp);
                while (i + 1 < n && data[i + 1].id == data[i].id)
                    i++;
                kilep = new TimeSpan(data[i].ora, data[i].perc, data[i].mp);
                if (kilep - belep > maxido)
                {
                    maxido = kilep - belep;
                    maxv = data[i].id;
                }
                i++;
            }
            Console.WriteLine($"{maxv}. vendég {maxido}");
        }
    }
}
