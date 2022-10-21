using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZahlenkombinationenAusgeber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] zahlen = { 5, 6, 7, 8, 13, 14, 15, 16 };

            int gewinnfälle = 0;
            foreach (var z in zahlen)
            {
                int kombinationenAnzahl = 0;
                for (int i = 1; i <= 6; i++)
                {
                    for (int j = 1; j <= 6; j++)
                    {
                        for (int k = 1; k <= 6; k++)
                        {
                            if (i + j + k == z)
                            {
                                kombinationenAnzahl++;
                            }
                        }
                    }
                }
                Console.WriteLine($"Zahl: {z} Kombinationen: {kombinationenAnzahl}");
                gewinnfälle += kombinationenAnzahl;
            }
            
            Console.WriteLine($"Gewinnfälle insgesamt: {gewinnfälle}");
            Console.ReadKey();
        }
    }
}
