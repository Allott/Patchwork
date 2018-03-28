using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patchwork
{
    class Program
    {


        static void Main(string[] args)
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Data\" + "1.txt";

            Player pass = new Player("Passer");
            Player hum1 = new PlayerHuman("Human 1");
            Player hum2 = new PlayerHuman("Human 2");

            Player first = new PlayerAgentFirstOptionSO("First");
            Player high = new PlayerAgentHighestPointSO("Highest");
            Player pPT = new PlayerAgentPointPerTimeSO("PPT");
            PlayerAgentEarlyEconomySO eco = new PlayerAgentEarlyEconomySO("Economy");
            eco.TimeThreshhold = 34;
            Player react = new PlayerAgentPointPerTimeReactiveSO("React");

            Player[] playerList = {pass, hum1, hum2,
                first, high, pPT, eco, react
            }; 



            Player p1 = react;
            Player p2 = eco;


            while (true)
            {
                Console.Clear();
                Console.WriteLine("Patchwork Experimentor");
                Console.WriteLine("");
                Console.WriteLine("1 - Generate New Setup");
                Console.WriteLine("2 - Set Player1");
                Console.WriteLine("3 - Set Player2");
                Console.WriteLine("4 - Set Economy Threshhold");
                Console.WriteLine("5 - Play Single Game");
                Console.WriteLine("6 - Play 1000 Games");
                Console.WriteLine("");
                Console.WriteLine("please enter the demo you would like to run.");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");


                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Generating Setups");
                        filemaker();
                        break;

                    case "2":
                        p1 = Select(playerList);
                        break;

                    case "3":
                        p2 = Select(playerList);
                        break;

                    case "4":
                        eco.TimeThreshhold = Convert.ToInt32(Console.ReadLine());
                        break;

                    case "5":
                        Console.Clear();
                        p1.Reset();
                        p2.Reset();
                        Game game1 = new Game();
                        tilesLoadFile(game1, path);
                        game1.setPlayers(p1, p2);
                        Console.WriteLine("Winner: " + game1.rungame().GetName());
                        Console.ReadLine();
                        break;

                    case "6":
                        Console.Clear();
                        int Countp1 = 0;
                        int Countp2 = 0;
                        

                        for (int i = 0; i < 500; i++)
                        {
                            p1.Reset();
                            p2.Reset();
                            Game game2 = new Game();
                            string path2 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Data\RandomSetup" + i + ".txt";
                            tilesLoadFile(game2, path2);
                            game2.setPlayers(p1, p2);

                            string winner = game2.rungame().GetName();

                            if (winner == p1.GetName()) { Countp1++; }
                            if (winner == p2.GetName()) { Countp2++; }

                            Console.WriteLine(i + ":" + winner);
                        }

                        for (int i = 500; i < 1000; i++)//for second half of games invert who goes first
                        {
                            p1.Reset();
                            p2.Reset();
                            Game game2 = new Game();
                            string path2 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Data\RandomSetup" + i + ".txt";
                            tilesLoadFile(game2, path2);
                            game2.setPlayers(p2, p1);

                            string winner = game2.rungame().GetName();

                            if (winner == p1.GetName()) { Countp1++; }
                            if (winner == p2.GetName()) { Countp2++; }
                            if (winner == "Draw") { }

                            Console.WriteLine(i + ":" + winner);
                        }

                        

                        Console.WriteLine(p1.GetName() + " " + Countp1 + " Wins");
                        Console.WriteLine(p2.GetName() + " " + Countp2 + " Wins");
                        Console.ReadLine();
                        break;

                    case "7":
                        
                        break;

                    default:
                        break;
                }
            }

        }

        public static void tilesLoadFile(Game gameLoading, String path)
        {


            string[] fileLines = System.IO.File.ReadAllLines(path);

            if (fileLines.Length % 20 == 0)
            {
                for (int i = 0; i < (fileLines.Length / 20); i++)
                {
                    Tile tileCurrent = new Tile(
                        Convert.ToInt32(fileLines[i * 20]),
                        Convert.ToInt32(fileLines[i * 20 + 1]),
                        Convert.ToInt32(fileLines[i * 20 + 2]),
                        Convert.ToInt32(fileLines[i * 20 + 3]));

                    bool[,] s = {
                        {false,false,false,false,false},
                        {
                            Convert.ToBoolean(fileLines[i * 20 + 4 ]),
                        Convert.ToBoolean(fileLines[i * 20 + 5 ]),
                        Convert.ToBoolean(fileLines[i * 20 + 6 ]),
                        Convert.ToBoolean(fileLines[i * 20 + 7 ]),
                        Convert.ToBoolean(fileLines[i * 20 + 8 ]),
                        },
                        {
                            Convert.ToBoolean(fileLines[i * 20 + 9 ]),
                        Convert.ToBoolean(fileLines[i * 20 + 10 ]),
                        Convert.ToBoolean(fileLines[i * 20 + 11 ]),
                        Convert.ToBoolean(fileLines[i * 20 + 12 ]),
                        Convert.ToBoolean(fileLines[i * 20 + 13 ]),
                        },
                        {
                            Convert.ToBoolean(fileLines[i * 20 + 14 ]),
                        Convert.ToBoolean(fileLines[i * 20 + 15 ]),
                        Convert.ToBoolean(fileLines[i * 20 + 16 ]),
                        Convert.ToBoolean(fileLines[i * 20 + 17 ]),
                        Convert.ToBoolean(fileLines[i * 20 + 18 ]),
                        },

                        {false,false,false,false,false}

                    };

                    tileCurrent.changeShape(s);
                    gameLoading.addTile(tileCurrent);
                }
            }
            else
            {
                Console.WriteLine("Error with file format: " + path);
            }
        }

        public static void filemaker()
        {
            for (int k = 0; k < 1000; k++)
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Data\" + "1.txt";
                List<string> newfile = new List<string>(System.IO.File.ReadAllLines(path));
                newfile.RemoveRange(640, 20);
                Random random = new Random();

                //Fisher–Yates shuffle
                for (int i = 0; i < 32; i++)
                {
                    int j = random.Next(0, 32);

                    for (int x = 0; x < 20; x++)//swap
                    {
                        string v = newfile[i * 20 + x];
                        newfile[i * 20 + x] = newfile[j * 20 + x];
                        newfile[j * 20 + x] = v;
                    }


                }

                newfile.Add("33");
                newfile.Add("2");
                newfile.Add("1");
                newfile.Add("0");
                newfile.Add("True");
                newfile.Add("True");
                for (int i = 0; i < 13; i++)
                {
                    newfile.Add("False");
                }
                newfile.Add("Generated" + k);
                System.IO.File.WriteAllLines(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Data\RandomSetup" + k + ".txt", newfile);
            }
        }

        public static Player Select(Player[] l)
        {
            Console.Clear();
            for (int i = 0; i < l.Length; i++)
            {
                Console.WriteLine(i + " " + l[i].GetName());
            }

            try
            {
                return l[Convert.ToInt32(Console.ReadLine())];
            }
            catch
            {
                return l[0];
            }


            
        }
    }
}
