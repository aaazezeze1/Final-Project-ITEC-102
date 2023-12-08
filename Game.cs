using System.Reflection;
using System.Xml.Linq;
using static System.Console;

namespace Final_Project_ITEC_102
{
    class Game
    {
        public void Start()
        {
            Title = "Cube Escape!";
            RunMainMenu();
        }
        public void RunMainMenu()
        {
            string prompt = @"
 ██████╗██╗   ██╗██████╗ ███████╗    ███████╗███████╗ ██████╗ █████╗ ██████╗ ███████╗
██╔════╝██║   ██║██╔══██╗██╔════╝    ██╔════╝██╔════╝██╔════╝██╔══██╗██╔══██╗██╔════╝
██║     ██║   ██║██████╔╝█████╗      █████╗  ███████╗██║     ███████║██████╔╝█████╗  
██║     ██║   ██║██╔══██╗██╔══╝      ██╔══╝  ╚════██║██║     ██╔══██║██╔═══╝ ██╔══╝  
╚██████╗╚██████╔╝██████╔╝███████╗    ███████╗███████║╚██████╗██║  ██║██║     ███████╗
 ╚═════╝ ╚═════╝ ╚═════╝ ╚══════╝    ╚══════╝╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝     ╚══════╝

Welcome to Cube Escape. Try to Evade and Escape.
(Use the arrow keys through options and press Enter to select an option.)";
            string[] options = { "Play", "About", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0: RunFirstChoice(); break;
                case 1: DisplayAboutInfo(); break;
                case 2: ExitGame(); break;
                default: break;
            }
        }               

        private void ExitGame()
        {
            WriteLine("\nPress any key to exit");
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        private void DisplayAboutInfo()
        {
            Clear();
            WriteLine("This game was created by Amazing Cabiles, Cyrelle Gapit, and Francen Manalo from BSCS-1B");
            WriteLine("The source code for this project is in https://github.com/aaazezeze1/Final-Project-ITEC-102");
            WriteLine("Press any key to return to the menu");
            ReadKey(true);
            RunMainMenu();
        }

        // Run Cube Escape
        private void RunFirstChoice()
        {
            Exception? exception = null;
            char[] DirectionChars = { '■', '■', '■', '■', };
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            Random random = new();
            Tile[,] map = new Tile[width, height];
            Direction? direction = null;
            Queue<(int X, int Y)> player = new();
            (int X, int Y) = (width / 2, height / 2);
            bool closeRequested = false;
            int DoorX = 0, DoorY = 0;
            int score = 0;
            int foodCollected = 0;

            string loseMessage = @"
██╗   ██╗ ██████╗ ██╗   ██╗    ██╗      ██████╗ ███████╗███████╗    ██╗
╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║     ██╔═══██╗██╔════╝██╔════╝    ██║
 ╚████╔╝ ██║   ██║██║   ██║    ██║     ██║   ██║███████╗█████╗      ██║
  ╚██╔╝  ██║   ██║██║   ██║    ██║     ██║   ██║╚════██║██╔══╝      ╚═╝
   ██║   ╚██████╔╝╚██████╔╝    ███████╗╚██████╔╝███████║███████╗    ██╗
   ╚═╝    ╚═════╝  ╚═════╝     ╚══════╝ ╚═════╝ ╚══════╝╚══════╝    ╚═╝                                                
";

            string winMessage = @"
██╗   ██╗ ██████╗ ██╗   ██╗    ██╗    ██╗██╗███╗   ██╗     ██████╗  ██████╗ ██╗    ██╗██████╗ ██╗
╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║    ██║██║████╗  ██║    ██╔════╝ ██╔════╝ ██║    ██║██╔══██╗██║
 ╚████╔╝ ██║   ██║██║   ██║    ██║ █╗ ██║██║██╔██╗ ██║    ██║  ███╗██║  ███╗██║ █╗ ██║██████╔╝██║
  ╚██╔╝  ██║   ██║██║   ██║    ██║███╗██║██║██║╚██╗██║    ██║   ██║██║   ██║██║███╗██║██╔═══╝ ╚═╝
   ██║   ╚██████╔╝╚██████╔╝    ╚███╔███╔╝██║██║ ╚████║    ╚██████╔╝╚██████╔╝╚███╔███╔╝██║     ██╗
   ╚═╝    ╚═════╝  ╚═════╝      ╚══╝╚══╝ ╚═╝╚═╝  ╚═══╝     ╚═════╝  ╚═════╝  ╚══╝╚══╝ ╚═╝     ╚═╝                                                                                                                                                                                                                                                                       
";

            try
            {
                Console.CursorVisible = false;
                Console.Clear();
                player.Enqueue((X, Y));
                map[X, Y] = Tile.Player;
                PositionObs();
                PositionFood();
                Console.SetCursorPosition(X, Y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write('■');

                while (!direction.HasValue && !closeRequested)
                {
                    GetDirection();
                }
                while (true)
                {
                    // If the console is resized before starting or during the game it automatically becomes game over
                    if (Console.WindowWidth != width || Console.WindowHeight != height)
                    {
                        Console.Clear();
                        return;
                    }
                    switch (direction)
                    {
                        case Direction.Up: Y--; break;
                        case Direction.Down: Y++; break;
                        case Direction.Left: X--; break;
                        case Direction.Right: X++; break;
                    }

                    // Player and Border Collision Mechanic
                    if (X < 0 || X >= width || Y < 0 || Y >= height || map[X, Y] is Tile.Player)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(loseMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Game Over! Score: " + score);
                        Console.WriteLine("You will be redirected shortly to the Menu");
                        Thread.Sleep(5000);
                        RunMainMenu();
                        return;
                    }

                    // Spawn Door only if the player is able to collect 30 foods
                    if (score == 30)
                    {
                        SpawnDoor();
                    }

                    Console.SetCursorPosition(X, Y);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(DirectionChars[(int)direction!]);
                    player.Enqueue((X, Y));

                    if (map[X, Y] is Tile.Food)
                    {
                        PositionFood();
                        score = score + 1;
                        foodCollected++;

                        // for adding obstacles
                        if (foodCollected % 6 == 0)
                        {
                            AddMoreObs();
                        }
                    }

                    // Player and Door Collision Mechanic
                    if (DoorX == X && DoorY == Y)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine(winMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Game Over! Score: " + score);
                        Console.WriteLine("You will be redirected shortly to the Menu");
                        Thread.Sleep(5000);
                        RunMainMenu();
                        return;
                    }

                    // Player and Obstacle Collision Mechanic
                    if (map[X, Y] is Tile.Obs)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(loseMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Game Over! Score: " + score);
                        Console.WriteLine("You will be redirected shortly to the Menu");
                        Thread.Sleep(5000);
                        RunMainMenu();
                        return;
                    }
                    else
                    {
                        (int x, int y) = player.Dequeue();
                        map[x, y] = Tile.Open;
                        Console.SetCursorPosition(x, y);
                        Console.Write(' ');
                    }
                    if (Console.KeyAvailable)
                    {
                        GetDirection();
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception e)
            {
                exception = e;
                throw;
            }

            // Arrow Keys for Direction
            void GetDirection()
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow: direction = Direction.Up; break;
                    case ConsoleKey.DownArrow: direction = Direction.Down; break;
                    case ConsoleKey.LeftArrow: direction = Direction.Left; break;
                    case ConsoleKey.RightArrow: direction = Direction.Right; break;
                }
            }

            // Show Door to the screen
            void SpawnDoor()
            {
                DoorX = random.Next(0, 1);
                DoorY = random.Next(0, 1);
                Console.SetCursorPosition(DoorX, DoorY);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write('█');
            }

            // Add Obstacles
            void PositionObs()
            {
                List<(int X, int Y)> possibleCoordinates = new();
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (map[i, j] is Tile.Open)
                        {
                            possibleCoordinates.Add((i, j));
                        }
                    }
                }
                for (int k = 0; k < height; k++)
                {
                    int index = random.Next(possibleCoordinates.Count);
                    (int X, int Y) = possibleCoordinates[index];
                    map[X, Y] = Tile.Obs;
                    Console.SetCursorPosition(X, Y);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write('*');
                }
            }

            void AddMoreObs()
            {
                List<(int X, int Y)> possibleCoordinates = new();
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (map[i, j] is Tile.Open)
                        {
                            possibleCoordinates.Add((i, j));
                        }
                    }
                }

                // Add additional obstacles
                for (int p = 0; p < 6; p++)
                {
                    int index = random.Next(possibleCoordinates.Count);
                    (int X, int Y) = possibleCoordinates[index];
                    map[X, Y] = Tile.Obs;
                    Console.SetCursorPosition(X, Y);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write('*');
                }
            }

            // Add Fruits
            void PositionFood()
            {
                List<(int X, int Y)> possibleCoordinates = new();
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (map[i, j] is Tile.Open)
                        {
                            possibleCoordinates.Add((i, j));
                        }
                    }
                }
                for (int k = 0; k < 5; k++)
                {
                    int index = random.Next(possibleCoordinates.Count);
                    (int X, int Y) = possibleCoordinates[index];
                    map[X, Y] = Tile.Food;
                    Console.SetCursorPosition(X, Y);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write('+');
                }
            }
        }

        enum Direction
        {
            Up = 0,
            Down = 1,
            Left = 2,
            Right = 3,
        }

        enum Tile
        {
            Open = 0,
            Player,
            Obs,
            Food,
        }
    }
}