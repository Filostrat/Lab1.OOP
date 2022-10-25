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

            //Max.LoseGame(Sanya, 101);

            Max.WinGame(Sanya, 50);
            Max.WinGame(Sanya, 20);
            Max.LoseGame(Sanya, 50);           
            Max.LoseGame(Sanya, 20);
            Max.LoseGame(Yarik, 20);


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
        private List<Game> Game = new();

        private double startRating;      

        public double Rating
        {
            get {
                double rating = startRating;
                foreach (var item in Game)
                {
                    rating += (item.Winner == UserName) ? +item.Rating : -item.Rating;
                }
                return rating;
            }
        }

        public GameAccount(string UserName)
        {
            this.UserName = UserName;
            startRating = 100;
        }

        public void WinGame( GameAccount opponent, double rating)
        {
            EventGame(this, opponent, rating);
        }

        public void LoseGame(GameAccount opponent, double rating)
        {
            EventGame(opponent, this, rating);
        }

        private static void EventGame(GameAccount winner, GameAccount loser, double rating)
        {
            if (rating >= 0)
            {
                if (rating <= loser.Rating)
                {
                    var game = new Game(winner.UserName, loser.UserName, winner.UserName, rating);
                    loser.Game.Add(game);
                    winner.Game.Add(game);

                    winner.GameCount++;
                    loser.GameCount++;
                }
                else
                {
                    Console.WriteLine($"The player {loser.UserName} does not have a sufficient rating");
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
            foreach (var item in Game)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }

        public void PrintRating()
        {
            Console.WriteLine($"|UserName: {UserName,5} | Rating: {Rating,4} |");
        }
    }
    public class Game
    {
        public static int AllNumberGame { get; set; } = 0;
        public readonly static List<Game> AllGame = new();

        public int NumberGame { get; set; }
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
            NumberGame = ++AllNumberGame;
            Date = DateTime.Now;
            AllGame.Add(this);
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
                if (index==item.NumberGame)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine();
        }

        public override string ToString()
        {
            return $"|Game №{NumberGame} | Who: {FirstOpponent,5} vs {SecondOpponent,5} |Rating: {Rating,4}| Winner: {Winner,5}| Date {Date,10}|"; 
        }
    }
}
