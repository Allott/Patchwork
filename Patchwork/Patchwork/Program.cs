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
            Player brtT = new PlayerAgentBTalks("BruteT");
            Player brt1 = new PlayerAgentB("Brute1");
            Player brt2 = new PlayerAgentB("Brute2");
            Player pPT = new PlayerAgentPointPerTime("PPT");
            Player pPTT = new PlayerAgentPointPerTimeTalks("PPTT");
            Player pPTT2 = new PlayerAgentPointPerTimetwo("PPTT2");//<====
            PlayerAgentEarlyEconomy eco = new PlayerAgentEarlyEconomy("Eco");
            eco.TimeThreshhold = 34;
            Player pPTR = new PlayerAgentPointPerTimeReactive("PPTR");


            while (true)
            {
                Console.Clear();
                Console.WriteLine("Patchwork Demo");
                Console.WriteLine("");
                Console.WriteLine("1 - Gameplay and Human interface");
                Console.WriteLine("2 - Basic Rational Agent implementation");
                Console.WriteLine("3 - Example experiment");
                Console.WriteLine("4 - two player game");
                Console.WriteLine("5 - PPTT testing");
                Console.WriteLine("6 - Experement PPT");
                Console.WriteLine("");
                Console.WriteLine("please enter the demo you would like to run.");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");


                switch (Console.ReadLine())
                {
                    case "1":
                        Game game1 = new Game();
                        tilesLoadFile(game1, path);
                        game1.setPlayers(hum1, pass);
                        /*
                        for (int i = 0; i < 7; i++)
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                hum1.GetGrid().shape[i+2, j+2] = true;
                            }
                        }
                        */
                        game1.rungame();
                        break;

                    case "2":
                        Game game2 = new Game();
                        tilesLoadFile(game2, path);
                        game2.setPlayers(brtT, pass);
                        game2.rungame();
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Generating Setups");
                        filemaker();

                        int p1Count = 0;
                        int p2Count = 0;
                        int drawCount = 0;

                        for (int i = 0; i < 1000; i++)
                        {
                            Game game3 = new Game();
                            string path2 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Data\RandomSetup" + i + ".txt";
                            tilesLoadFile(game3, path2);
                            brt1.Reset();
                            brt2.Reset();
                            game3.setPlayers(brt1, brt2);
                            string winner = game3.rungame().GetName();

                            if (winner == "Brute1") { p1Count++; }
                            if (winner == "Brute2") { p2Count++; }
                            if (winner == "Draw") { drawCount++; }

                            Console.WriteLine(i + ":" + winner);

                        }

                        Console.WriteLine(brt1.GetName() + " " + p1Count + " Wins");
                        Console.WriteLine(brt2.GetName() + " " + p2Count + " Wins");
                        Console.WriteLine(drawCount + " Draws");
                        Console.ReadLine();
                        break;

                    case "4":
                        Game game4 = new Game();
                        tilesLoadFile(game4, path);
                        game4.setPlayers(hum1, hum2);
                        game4.rungame();
                        break;

                    case "5":
                        Game game5 = new Game();
                        tilesLoadFile(game5, path);
                        game5.setPlayers(pPTT, pass);
                        game5.rungame();
                        break;

                    case "6":
                        Console.Clear();
                        Console.WriteLine("Generating Setups");
                        filemaker();

                        int p3Count = 0;
                        int p4Count = 0;
                        int draw2Count = 0;

                        for (int i = 0; i < 1000; i++)
                        {
                            Game game3 = new Game();
                            string path2 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Data\RandomSetup" + i + ".txt";
                            tilesLoadFile(game3, path2);
                            pPTT2.Reset();
                            pPTR.Reset();
                            game3.setPlayers(pPTT2, pPTR);
                            string winner = game3.rungame().GetName();

                            if (winner == "PPTT2") { p3Count++; }
                            if (winner == "PPTR") { p4Count++; }
                            if (winner == "Draw") { draw2Count++; }

                            Console.WriteLine(i + ":" + winner);

                        }

                        Console.WriteLine(pPTT2.GetName() + " " + p3Count + " Wins");
                        Console.WriteLine(pPTR.GetName() + " " + p4Count + " Wins");
                        Console.WriteLine(draw2Count + " Draws");
                        Console.ReadLine();
                        break;

                    case "7":
                        Console.Clear();
                        Console.WriteLine("Generating Setups");
                        filemaker();
                        Console.WriteLine("done");

                        for (int i = 1; i < 52; i++)
                        {
                            Console.Write(i);
                            int wins = 0;
                            float score = 0;

                            for (int j = 0; j < 1000; j++)
                            {
                                Game game = new Game();
                                string path2 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Data\RandomSetup" + i + ".txt";
                                tilesLoadFile(game, path2);
                                eco.Reset();
                                eco.TimeThreshhold = i;
                                pass.Reset();

                                game.setPlayers(eco, pass);
                                Player winner = game.rungame();

                                if (winner.GetName() == "Eco")
                                {
                                    score += winner.GetGrid().returnScore() + winner.GetButtons();
                                    wins++;
                                }

                            }

                            score = score / 1000;

                            Console.WriteLine("I:" + i + "/tWins:" + wins + "/tScore:" + score);

                        }
                        Console.ReadLine();
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

    }
}
