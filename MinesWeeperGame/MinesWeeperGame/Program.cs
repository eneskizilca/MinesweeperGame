using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesWeeperGame
{
    class Program
    {
        static void Main(string[] args)
        {
            const int boyut = 20;
            char[,] alan = new char[boyut, boyut];
            bool[,] mayinlar = new bool[boyut, boyut];
            bool[,] ziyaretEdildi = new bool[boyut, boyut];
            Random rnd = new Random();
            int mayinSayisi = 60;

            // Alanı doldur ve mayınları yerleştir
            for (int i = 0; i < boyut; i++)
                for (int j = 0; j < boyut; j++)
                    alan[i, j] = '-';

            for (int i = 0; i < mayinSayisi;)
            {
                int x = rnd.Next(0, boyut);
                int y = rnd.Next(0, boyut);

                if (!mayinlar[x, y])
                {
                    mayinlar[x, y] = true;
                    i++;
                }
            }

            // Oyun döngüsü
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mayın Tarlası (20x20)\n");

                // Sütun numaralarını yazdır
                Console.Write("   "); // Başlangıç boşluğu
                for (int i = 1; i <= boyut; i++)
                    Console.Write((i % 10) + " "); // 1'den 9'a kadar, sonra 0 tekrar eder
                Console.WriteLine();

                // Haritayı yazdır
                for (int i = 0; i < boyut; i++)
                {
                    Console.Write((i + 1).ToString("D2") + " "); // Satır numaralarını yazdır
                    for (int j = 0; j < boyut; j++)
                        Console.Write(alan[i, j] + " ");
                    Console.WriteLine();
                }

                Console.Write("\nX koordinatını girin (1-20): ");
                int xSecim = int.Parse(Console.ReadLine()) - 1;
                Console.Write("Y koordinatını girin (1-20): ");
                int ySecim = int.Parse(Console.ReadLine()) - 1;

                if (xSecim < 0 || xSecim >= boyut || ySecim < 0 || ySecim >= boyut)
                {
                    Console.WriteLine("Geçersiz koordinat. Tekrar deneyin.");
                    Console.ReadKey();
                    continue;
                }

                if (mayinlar[xSecim, ySecim])
                {
                    Console.Clear();
                    Console.WriteLine("Mayına bastınız! Oyun bitti.");
                    alan[xSecim, ySecim] = '*';

                    // Tüm mayınları göster
                    for (int i = 0; i < boyut; i++)
                    {
                        for (int j = 0; j < boyut; j++)
                        {
                            if (mayinlar[i, j])
                                alan[i, j] = '*';
                        }
                    }
                    break;
                }
                else
                {
                    // Otomatik açılmayı başlat
                    AcHucresi(xSecim, ySecim, alan, mayinlar, ziyaretEdildi, boyut);
                }
            }

            // Final haritasını göster
            Console.Clear();
            Console.WriteLine("Oyun Alanı:");

            // Sütun numaralarını yazdır
            Console.Write("   "); // Başlangıç boşluğu
            for (int i = 1; i <= boyut; i++)
                Console.Write((i % 10) + " "); // 1'den 9'a kadar, sonra 0 tekrar eder
            Console.WriteLine();

            // Haritayı yazdır
            for (int i = 0; i < boyut; i++)
            {
                Console.Write((i + 1).ToString("D2") + " "); // Satır numaralarını yazdır
                for (int j = 0; j < boyut; j++)
                    Console.Write(alan[i, j] + " ");
                Console.WriteLine();
            }

            Console.WriteLine("Oyun sona erdi!");
            Console.ReadKey();
        }

        static void AcHucresi(int x, int y, char[,] alan, bool[,] mayinlar, bool[,] ziyaretEdildi, int boyut)
        {
            if (x < 0 || x >= boyut || y < 0 || y >= boyut || ziyaretEdildi[x, y])
                return;

            ziyaretEdildi[x, y] = true;

            int mayinCevreSayisi = 0;

            // Çevredeki mayınları say
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int yeniX = x + i;
                    int yeniY = y + j;

                    if (yeniX >= 0 && yeniX < boyut && yeniY >= 0 && yeniY < boyut && mayinlar[yeniX, yeniY])
                    {
                        mayinCevreSayisi++;
                    }
                }
            }

            alan[x, y] = char.Parse(mayinCevreSayisi.ToString());

            // Eğer çevrede hiç mayın yoksa, komşuları otomatik aç
            if (mayinCevreSayisi == 0)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int yeniX = x + i;
                        int yeniY = y + j;
                        AcHucresi(yeniX, yeniY, alan, mayinlar, ziyaretEdildi, boyut);
                    }
                }
            }
        }     
    }
}
