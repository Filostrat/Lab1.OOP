using System;
using System.Collections.Generic;

namespace Lab1.OOP
{
    class Program
    {
        static void Main( )
        {
            var Max = new GameAccount("Max");
            var Sanya = new GameAccount("Sanya");
            var Yarik = new GameAccount("Yarik");

            Max.WinGame(Sanya, 50);
            Max.WinGame(Sanya, 20);
            Max.WinGame(Sanya, 0);
            Max.LoseGame(Sanya, 50);
            Max.LoseGame(Sanya, 20);
            Max.LoseGame(Yarik, 30);
            Yarik.LoseGame(Sanya, 100);


            Max.GetStats();
            Sanya.GetStats();
            Yarik.GetStats();

            Game.GetStats();

            Max.PrintRating();
            Sanya.PrintRating();
            Yarik.PrintRating();

            Game.GetGame(4);
        }
    }
    public class GameAccount
    {
        public string UserName { get; set; }
        public int GameCount { get ; set; }

        public List<double> CurrentRating = new();

        public GameAccount(string UserName)
        {
            this.UserName = UserName;
            CurrentRating.Add(100);
            GameCount = 0;
        }

        public void WinGame( GameAccount Opponent, double Rating)
        {
            EventGame(this, Opponent, Rating);
        }

        public void LoseGame(GameAccount Opponent, double Rating)
        {
            EventGame(Opponent, this, Rating);
        }

        private static void EventGame(GameAccount winner, GameAccount loser, double Rating)
        {
            if (Rating >= 0)
            {
                if (Rating <= loser.GetRating() - 1)
                {
                    var game = new Game(winner.UserName, loser.UserName, winner.UserName, Rating);
                    Game.AllGame.Add(game);

                    winner.CurrentRating.Add(Rating);
                    winner.GameCount++;
                    loser.CurrentRating.Add(-Rating);
                    loser.GameCount++;
                }
                else
                {
                    Console.WriteLine($"The player {loser} does not have a sufficient rating");
                }
            }
            else
            {
                Console.WriteLine("You can't play for a negative rating");
            }
        }       

        public void GetStats()
        {
            Console.WriteLine($"Game statistics for {UserName}");
            foreach (var item in Game.AllGame)
            {
                if (item.FirstOpponent == UserName || item.SecondOpponent== UserName)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine();
        }

        public double GetRating()
        {
            double Rating = 0;

            foreach (var item in CurrentRating)
            {
                Rating += item;
            }
            return Rating;
        }

        public void PrintRating()
        {
            Console.WriteLine($"|UserName: {UserName,5} | Rating: {GetRating(),4} |");
        }
    }
    public class Game
    {
        public static int AllNumderGame { get; set; } = 0;
        public static List<Game> AllGame = new();

        public int NumderGame { get; set; }
        public string FirstOpponent { get; set; }
        public string SecondOpponent { get; set; }
        public string Winner { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }

        public Game(string FirstOpponent, string SecondOpponent,string Winner, double Rating)
        {
            this.FirstOpponent = FirstOpponent;
            this.SecondOpponent = SecondOpponent;
            this.Rating = Rating;
            this.Winner = Winner;           
            NumderGame = ++AllNumderGame;
            Date = DateTime.Now;
        }

        public static void GetStats()
        {
            Console.WriteLine($"General game statistics");
            foreach (var item in AllGame)
            {
                Console.WriteLine(item.ToString()); 
            }
            Console.WriteLine();
        }

        public static void GetGame(int index)
        {
            Console.WriteLine($"\nGame by number {index}");
            foreach (var item in AllGame)
            {
                if (index==item.NumderGame)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine();
        }

        public override string ToString()
        {
            return $"|Game №{NumderGame} | Who: {FirstOpponent,5} vs {SecondOpponent,5} |Rating: {Rating,4}| Winner: {Winner,5}| Date {Date,10}|"; 
        }
    }
}
