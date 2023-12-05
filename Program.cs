// import library for disabling the minimize and maximize button of the console window
using System.Runtime.InteropServices;

// import library for playing music
using System.Media;

namespace Final_Project_ITEC_102
{
    internal class Program
    {
        // disable minimize and maximize button of console window
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        static void Main(string[] args)
        {
            // play music non stop once the program runs
            // music from K00sin from https://pixabay.com/music/video-games-chiptune-grooving-142242/
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer music = new SoundPlayer("chiptune-grooving-142242.wav");
                music.Load();
                music.PlayLooping();
            }

            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }

            Game myGame = new Game();
            myGame.Start();
        }
    }
}