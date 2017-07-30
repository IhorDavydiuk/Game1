using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card_game_21
{

    enum Znachen
    {
        Valet = 2,
        Dama,
        Korol,
        Shest = 6,
        Sem,
        Vosem,
        Devait,
        Desiat,
        Tuz

    }
    enum Mast
    {
        Bubni,
        Piki,
        Chervi,
        Trefi
    }
    struct Karta
    {
        public Mast mast;
        public Znachen znachen;


        public Karta(Znachen znachen, Mast mast)
        {
            this.znachen = znachen;
            this.mast = mast;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Karta[] koloda = new Karta[36];

            int num_mass = 0;
            for (Mast i = Mast.Bubni; i <= Mast.Trefi; i++)
            {
                for (Znachen j = Znachen.Valet; j <= Znachen.Tuz; j++)
                {
                    if ((int)j == 5) continue;
                    koloda[num_mass] = new Karta(j, i);
                    num_mass++;
                }
            }

            Random rnd = new Random();

            Console.Write("=========== GAME TWENTY-ONE ===========");

            //  ИГРА ИГРОКА

            Console.WriteLine(@"
Who first receives cards? You = 1; 
                          Opponent = 2;");

            int first_person_int;
            bool first_person = true;

            for (int i = 0; i < 10; i++)
            {

                bool first_person_bool = int.TryParse(Console.ReadLine(), out first_person_int);
                if (first_person_bool && first_person_int == 1)
                {
                    first_person = true;
                    break;
                }
                else if (first_person_bool && first_person_int == 2)
                {
                    first_person = false;
                    break;
                }
                else
                {
                    Console.Write("Incorrect.Enter the value again '1' or '2'.Enter: ");
                }
            }

            char continue_game_char = 'y';
            bool continue_game_bool = true;
            int pobed_igrok = 0, pobed_komp = 0;
            do
            {
                Console.WriteLine(@"
  =========================================================
||_________________________VICTORY_________________________||
||        YOU                               OPPONENT       ||
||         {0}                                    {1}          ||
  ========================================================="
, pobed_igrok, pobed_komp);

                for (int i = 0; i < koloda.Length; i++)
                {
                    int rn = rnd.Next(koloda.Length);
                    Karta temp = koloda[i];
                    koloda[i] = koloda[rn];
                    koloda[rn] = temp;
                }

                int summ_ochkov_igrok = 0, summ_ochkov_komp = 0;
                int num_kart = 0;

                for (int z = 0; z < 2; z++)
                {
                    if (first_person)
                    {
                        Console.WriteLine(" ============================");
                        Console.WriteLine("|\t     YOU");
                        Console.WriteLine("|============================");
                        for (int i = 0; i < 2; i++)
                        {
                            Console.WriteLine("| " + koloda[num_kart].znachen + " " + koloda[num_kart].mast);
                            Console.WriteLine("|----------------------------");
                            summ_ochkov_igrok += (int)koloda[num_kart].znachen;
                            num_kart++;
                        }

                        for (; num_kart < koloda.Length;)
                        {
                            if (summ_ochkov_igrok <= 21)
                            {
                                Console.Write("|\tNext('y' or 'n')? ");
                                char otv_ch;
                                bool otv_bl = char.TryParse(Console.ReadLine(), out otv_ch);
                                Console.WriteLine("|----------------------------");
                                if (summ_ochkov_igrok <= 21 && (otv_ch == 'y' || otv_ch == 'Y') && otv_bl)
                                {
                                    Console.WriteLine("| " + koloda[num_kart].znachen + " " + koloda[num_kart].mast);
                                    Console.WriteLine("|----------------------------");
                                    summ_ochkov_igrok += (int)koloda[num_kart].znachen;
                                    num_kart++;
                                }

                                else if (summ_ochkov_igrok <= 21 && (otv_ch == 'n' || otv_ch == 'N') && otv_bl)
                                {
                                    Console.WriteLine("|\t\t    |Pass");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("|Enter 'y' or 'n' ");
                                    Console.WriteLine("|----------------------------");
                                }
                            }
                            else
                            {
                                Console.WriteLine("|\t\t    |More");
                                break;
                            }
                        }
                        first_person = !first_person;
                        Console.WriteLine("|\t\t    |Sum: " + summ_ochkov_igrok);
                        Console.WriteLine(" ============================");
                    }
                    else
                    {
                        Console.WriteLine(" \t\t\t\t============================ ");
                        Console.WriteLine(" \t\t\t\t\t OPPONENT\t    |");
                        Console.WriteLine(" \t\t\t\t============================|");
                        for (int i = 0; i < 2; i++)
                        {
                            Console.WriteLine(" \t\t\t\t {0} {1}", koloda[num_kart].znachen, koloda[num_kart].mast);
                            Console.WriteLine(" \t\t\t\t----------------------------|");
                            summ_ochkov_komp += (int)koloda[num_kart].znachen;
                            num_kart++;
                        }

                        for (; num_kart < koloda.Length;)
                        {
                            if (summ_ochkov_komp <= 21)
                            {
                                bool otv_bool = true;
                                if (summ_ochkov_komp == 21 || summ_ochkov_komp == 20 || summ_ochkov_komp == 19 || summ_ochkov_komp == 18)
                                {
                                    otv_bool = false;
                                }
                                else if (summ_ochkov_komp >= 15 && summ_ochkov_komp < 18)
                                {
                                    if (rnd.Next(10) % 2 == 0)
                                    {
                                        otv_bool = true;
                                    }
                                    else otv_bool = false;
                                }
                                else otv_bool = true;

                                if (otv_bool)
                                {
                                    Console.WriteLine(" \t\t\t\t\t Next? y\t    |");
                                    Console.WriteLine(" \t\t\t\t----------------------------|");
                                    Console.WriteLine(" \t\t\t\t " + koloda[num_kart].znachen + " " + koloda[num_kart].mast);
                                    Console.WriteLine(" \t\t\t\t----------------------------|");
                                    summ_ochkov_komp += (int)koloda[num_kart].znachen;
                                    num_kart++;
                                }

                                else
                                {
                                    Console.WriteLine(" \t\t\t\t\t Next? n\t    |");
                                    Console.WriteLine(" \t\t\t\t----------------------------|");
                                    Console.WriteLine("\t\t\t\t\t\t   |Pass    |");
                                    break;
                                }

                            }
                            else
                            {
                                Console.WriteLine(" \t\t\t\t\t Next? y\t    |");
                                Console.WriteLine(" \t\t\t\t----------------------------|");
                                Console.WriteLine("\t\t\t\t\t\t   |More    |");
                                break;
                            }
                        }

                        first_person = !first_person;
                        Console.WriteLine("\t\t\t\t\t\t   |Sum: " + summ_ochkov_komp + " |");
                        Console.WriteLine(" \t\t\t\t============================");
                    }
                }
                if (summ_ochkov_igrok <= 21 && summ_ochkov_komp <= 21)
                {
                    if (summ_ochkov_igrok > summ_ochkov_komp)
                    {
                        Console.WriteLine(@"
  =========================================================
||                                                         ||
||                          YOU WIN !!!                    ||
||                                                         ||
  =========================================================");
                        pobed_igrok += 1;
                    }
                    else if (summ_ochkov_igrok < summ_ochkov_komp)
                    {
                        Console.WriteLine(@"
  =========================================================
||                                                         ||
||                          YOU LOSE !!!                   ||
||                                                         ||
  =========================================================");
                        pobed_komp += 1;
                    }
                    else
                    {
                        Console.WriteLine(@"
  =========================================================
||                                                         ||
||                         EQUALLY !!                      ||
||                                                         ||
  =========================================================");
                    }
                }
                else if (summ_ochkov_igrok <= 21 && summ_ochkov_komp > 21)
                {
                    Console.WriteLine(@"
  =========================================================
||                                                         ||
||                          YOU WIN !!!                    ||
||                                                         ||
  =========================================================");
                    pobed_igrok += 1;
                }
                else if (summ_ochkov_igrok > 21 && summ_ochkov_komp <= 21)
                {
                    Console.WriteLine(@"
  =========================================================
||                                                         ||
||                          YOU LOSE !!!                   ||
||                                                         ||
  =========================================================");
                    pobed_komp += 1;
                }
                else if (summ_ochkov_igrok > 21 && summ_ochkov_komp > 21)
                {
                    if (summ_ochkov_igrok > summ_ochkov_komp)
                    {
                        Console.WriteLine(@"
  =========================================================
||                                                         ||
||                          YOU LOSE !!!                   ||
||                                                         ||
  =========================================================");
                        pobed_komp += 1;
                    }
                    else if (summ_ochkov_igrok < summ_ochkov_komp)
                    {
                        Console.WriteLine(@"
  =========================================================
||                                                         ||
||                          YOU WIN !!!                    ||
||                                                         ||
  =========================================================");
                        pobed_igrok += 1;
                    }
                    else
                    {
                        Console.WriteLine(@"
  =========================================================
||                                                         ||
||                         EQUALLY !!                      ||
||                                                         ||
  =========================================================");
                    }

                }

                Console.Write("Do you want to continue?Enter y/n:");
                for (int i = 0; i < 100; i++)
                {

                    continue_game_bool = char.TryParse(Console.ReadLine(), out continue_game_char);
                    if (continue_game_bool && (continue_game_char == 'y' || continue_game_char == 'Y'))
                    {
                        break;
                    }
                    else if (continue_game_bool && (continue_game_char == 'n' || continue_game_char == 'N'))
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("Incorrect.Enter the value again 'y' or 'n'.Enter: ");
                    }
                }
            }
            while (continue_game_bool && (continue_game_char == 'y' || continue_game_char == 'Y'));

            Console.WriteLine(@"
  =========================================================
||_________________________VICTORY_________________________||
||        YOU                               OPPONENT       ||
||         {0}                                    {1}          ||
  ========================================================="
, pobed_igrok, pobed_komp);
            Console.ReadLine();

        }
    }
}
