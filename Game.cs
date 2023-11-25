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

        private void RunMainMenu()
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

        private void RunFirstChoice()
        {
            if (OperatingSystem.IsWindows())
            {
                Console.WindowHeight = 30;
                Console.WindowWidth = 50;
            }

            // call the Snake class that cointans the code for the main game
            Snake snake = new Snake();
            while (true)
            {
                snake.WriteBoard();
                snake.Input();
                snake.Logic();
            }

            Console.ReadKey();
        }
    }
}
