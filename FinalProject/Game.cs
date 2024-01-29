using System;

namespace FinalProject
{
    public class Game
    {
        private int health;
        private int _x;
        private int _y;
        private bool key;
        private bool running = true;
        private string playerinput;
        private char[,] map = new char[5, 5];

        public Game()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Hello nameless warrior, welcome to Mehasa City.\nWe would like to give you a befitting welcome, but the wizard of riddles has cursed our city.\nYou must find and defeat him in his castle in the north-west. It's all up to you, nameless warrior.");

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    map[i, j] = '.';
                }
            }
            LoadGame();
            PrintMap();
            Startgameloop();
        }
        private void LoadGame()
        {
            if (System.IO.File.Exists("save.txt"))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader("save.txt"))
                {
                    health = int.Parse(reader.ReadLine());
                    _x = int.Parse(reader.ReadLine());
                    _y = int.Parse(reader.ReadLine());
                    key = bool.Parse(reader.ReadLine());
                }
                map[_x, _y] = 'X';
            }
            else
            {
                health = 2;
                _x = 2;
                _y = 2;
                map[2, 2] = 'X';
                key = false;
            }
        }

        private void SaveGame()
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("save.txt"))
            {
                writer.WriteLine(health);
                writer.WriteLine(_x);
                writer.WriteLine(_y);
                writer.WriteLine(key);
            }
        }

        private void Startgameloop()
        {
            while (running)
            {
                GetInput();
                ProcessInput();
            }
        }

        private void GetInput()
        {
            playerinput = Console.ReadLine();
            Console.WriteLine();
        }

        private void ProcessInput()
        {
            switch (playerinput)
            {
                case "w":
                    Movement(-1, 0);
                    break;
                case "a":
                    Movement(0, -1);
                    break;
                case "s":
                    Movement(1, 0);
                    break;
                case "d":
                    Movement(0, 1);
                    break;
                case "exit":
                    Console.WriteLine("I hope you enjoyed our game! Press any key...");
                    Console.ReadKey();
                    running = false;
                    break;
                case "help":
                    Console.WriteLine("Here are the current commands:\nw: go north\ns: go south\na: go west\nd: go east\ndelete: Delete saved game\nsave: save current game\nexit: exit the game\nfor riddle answers:1,2,3,4,5");
                    break;
                case "save":
                    SaveGame();
                    Console.WriteLine("Game saved!");
                    break;
                case "delete":
                    File.Delete("save.txt");
                    Console.Clear();
                    running = false;
                    new Game();
                    break;
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                    break;
                default:
                    Console.WriteLine("Command not recognized. Please type 'help' for a list of available commands.");
                    break;
            }

            if (_x == 0 && _y == 0 && key == true)
            {
                switch (playerinput)
                {
                    case "1":
                        running = false;
                        Console.WriteLine("TRUE!!! You win! Press any key to exit...");
                        Console.ReadKey();
                        break;
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                        health -= 1;
                        Console.WriteLine("HaHaHa WRONG ANSWER! But I am a generous person. So I'll give you another chance.");
                        break;
                }
            }
            if (health == 0)
            {
                running = false;
                Console.WriteLine("You lose. Press any key to load to last saved game...");
                Console.ReadKey();
                Console.Clear();
                Game newGame = new Game();
            }
        }

        private void Movement(int x, int y)
        {
            int newX = _x + x;
            int newY = _y + y;
            if (newX < 0 || newX >= 5 || newY < 0 || newY >= 5)
            {
                Console.WriteLine("You can't move there!");
                return;
            }
            map[_x, _y] = '.';
            _x = newX;
            _y = newY;
            map[_x, _y] = 'X';
            PrintMap();
            Console.WriteLine($"You are now standing on {_x},{_y}");
            switch (_x, _y)
            {
                case (0, 0):
                    if (key)
                    {
                        Console.WriteLine("The castle gate has opened and there is the Wizard.\nKansu:I am Kansu, the Great Wizard of Riddles, and if you want to beat me you gotta know my riddle.\nI can be cracked but never opened. I can be found but never sought.\nWhat am I?");
                        Console.WriteLine("1) Secret\n2) Joke\n3) Riddle\n4) Mystery\n5) Puzzle");
                    }
                    else
                    {
                        Console.WriteLine("The door is locked, find the key");
                    }
                    break;
                case (3, 0):
                    Console.WriteLine("Clue 1: I am often hidden in plain sight.");
                    break;
                case (2, 2):
                    Console.WriteLine("Mehasa City");
                    break;
                case (1, 3):
                    Console.WriteLine("Clue 2: People often overlook me, yet rely on me daily.");
                    break;
                case (4, 4):
                    if (!key)
                    {
                        key = true;
                        Console.WriteLine("key found");
                    }
                    break;
            }
        }

        private void PrintMap()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(map[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
