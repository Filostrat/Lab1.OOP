using System;
using System.Collections.Generic;

namespace Lab1.OOP
{
    class Program
    {
        static void Main(string[] args)
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
        public string UserName { get; private set; }
        public int GameCount { get ; private set; }

        public List<double> CurrentRating = new List<double>();

        public GameAccount(string UserName)
        {
            this.UserName = UserName;
            CurrentRating.Add(100);
            GameCount = 0;
        }

        public void WinGame( GameAccount Opponent, double Rating)
        {
            if (Rating >= 0)
            {
                if (Rating <= Opponent.GetRating()-1)
                {
                    var game = new Game(UserName, Opponent.UserName, UserName, Rating);
                    Game.AllGame.Add(game);

                    Opponent.CurrentRating.Add(-Rating);
                    Opponent.GameCount++;
                    CurrentRating.Add(Rating);
                    GameCount++;
                }
                else
                {
                    Console.WriteLine($"The player {Opponent.UserName} does not have a sufficient rating");
                }
            }
            else
            {
                Console.WriteLine("You can't play for a negative rating");
            }

        }
        public void LoseGame(GameAccount Opponent, double Rating)
        {
            if (Rating >=0)
            {
                if (Rating <= GetRating()-1)
                {
                    var game = new Game(UserName, Opponent.UserName, Opponent.UserName, Rating);
                    Game.AllGame.Add(game);

                    Opponent.CurrentRating.Add(Rating);
                    Opponent.GameCount++;
                    CurrentRating.Add(-Rating);
                    GameCount++;
                }
                else
                {
                    Console.WriteLine($"The player {UserName} does not have a sufficient rating");
                }
            }
            else
            {
                Console.WriteLine("You can't play for a negative rating");
            }
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

        public void GetStats()
        {
            Console.WriteLine($"Game statistics for {UserName}");
            foreach (var item in Game.AllGame)
            {
                if (item.OpponentOne == UserName || item.OpponentTwo== UserName)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine();
        }

        public void PrintRating()
        {
            Console.WriteLine($"{UserName} Rating {GetRating()}");
        }
    }
    public class Game
    {
        public static int AllNumderGame { get; set; } = 0;
        public static List<Game> AllGame = new();

        public int NumderGame { get; set; }
        public string OpponentOne { get; set; }
        public string OpponentTwo { get; set; }
        public string Winner { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }

        public Game(string OpponentOne, string OpponentTwo,string Winner, double Rating)
        {
            this.OpponentOne = OpponentOne;
            this.OpponentTwo = OpponentTwo;
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
            return $"Game №{NumderGame} | Who: {OpponentOne,5} vs {OpponentTwo,5} |Rating: {Rating,4}| Winner: {Winner,5}| Date {Date,10}|"; 
        }
    }
}
