namespace Final_Project_ITEC_102
{
    class Snake
    {
        string loseMessage = @"
██╗   ██╗ ██████╗ ██╗   ██╗    
╚██╗ ██╔╝██╔═══██╗██║   ██║   
 ╚████╔╝ ██║   ██║██║   ██║      
  ╚██╔╝  ██║   ██║██║   ██║     
   ██║   ╚██████╔╝╚██████╔╝   
   ╚═╝    ╚═════╝  ╚═════╝     

██╗      ██████╗ ███████╗███████╗   ██╗    
██║     ██╔═══██╗██╔════╝██╔════╝   ██║
██║     ██║   ██║███████╗█████╗     ██║
██║     ██║   ██║╚════██║██╔══╝     ╚═╝
███████╗╚██████╔╝███████║███████╗   ██╗
╚══════╝ ╚═════╝ ╚══════╝╚══════╝   ╚═╝ 

";

        int Height = 20, Width = 30;

        // helps with the position of the cursor the spawn coordinates of fruits
        int[] X = new int[50];
        int[] Y = new int[50];

        int fruitX, fruitY, fruitA, fruitB, fruitC, fruitD;

        // the snake's body is only one block 
        int parts = 1;

        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        char key = 'W';

        // set coordinate of player upon spawn
        Random rnd = new Random();
        public Snake()
        {
            // set coordinate of snake at 5 both x and y
            X[0] = 5;
            Y[0] = 5;
            Console.CursorVisible = false;
            // set random coordinate of fruit upon spawn
            fruitX = rnd.Next(2, (Width - 2));
            fruitY = rnd.Next(2, (Height - 2));

            // set random coordinate of the 2 new fruits
            fruitA = rnd.Next(2, (Width - 2));
            fruitB = rnd.Next(2, (Height - 2));
            fruitC = rnd.Next(3, (Width - 2));
            fruitD = rnd.Next(3, (Height - 2));
        }

        // this creates a small rectangular border
        public void WriteBoard()
        {
            Console.Clear();
            for (int i = 1; i <= (Width + 2); i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(i, 1);
                Console.Write("-");
            }
            for (int i = 1; i <= (Width + 2); i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(i, (Height + 2));
                Console.Write("-");
            }
            for (int i = 1; i <= (Height + 1); i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(1, i);
                Console.Write("|");
            }
            for (int i = 1; i <= (Height + 1); i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition((Width + 2), i);
                Console.Write("|");
            }
        }

        public void Input()
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                key = keyInfo.KeyChar;
            }
        }

        // actual snake and its coordinates
        public void WritePoint(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write("■");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine(loseMessage);
                Environment.Exit(0);
            }
        }

        public void Logic()
        {
            if (X[0] == fruitX)
            {
                if (Y[0] == fruitY)
                {
                    // if its empty the game ends when snake collects the fruit
                    fruitX = rnd.Next();
                    fruitY = rnd.Next();
                }
            }
            if (X[0] == fruitA)
            {
                if (Y[0] == fruitB)
                {
                    fruitA = rnd.Next();
                    fruitB = rnd.Next();
                }
            }
            if (X[0] == fruitC)
            {
                if (Y[0] == fruitD)
                {
                    fruitC = rnd.Next();
                    fruitD = rnd.Next();
                }
            }

            switch (key)
            {
                // keyboard controls
                case 'w':
                    Y[0]--;
                    break;
                case 'a':
                    X[0]--;
                    break;
                case 's':
                    Y[0]++;
                    break;
                case 'd':
                    X[0]++;
                    break;
            }

            // shows the fruits and the snake
            for (int i = 0; i <= (parts - 1); i++)
            {
                WritePoint(X[i], Y[i]);
                WritePoint(fruitX, fruitY);

                // show the 2 new fruits
                WritePoint(fruitA, fruitB);
                WritePoint(fruitC, fruitD);
            }

            // check collision with the border
            if (X[0] <= 1 || X[0] >= Width || Y[0] <= 1 || Y[0] >= Height)
            {
                Console.Clear();
                Console.WriteLine(loseMessage);
                Environment.Exit(0);
            }
            Thread.Sleep(100);
        }
    }
}
