using System;
using System.Collections.Generic;

class Program {
  public static void Main (string[] args) {

		Game game = new Game();
		
    Console.WriteLine ("Welcome to the cookie game!");
		
		do
		{
			game.SwitchPlayers();
			game.PlayerChoice();
			game.IsItARasin();
			
		} while (!game.gameOver);
		
		Console.ReadLine();
  }
}

public static class GameLogic
{
	public static Game SwitchPlayers(this Game game)
	{
		if (game.CurrentPlayerIndex == 2)
		{
			game.CurrentPlayerIndex = 0;
		}
		
		return game;
	}
	
	public static Game PlayerChoice(this Game game)
	{
		Player player = game.Players[game.CurrentPlayerIndex];
		Console.WriteLine($"Player { player.Number } please choose a number between 1 and 9");
		int playerChoice = Convert.ToInt32(Console.ReadLine());
		playerChoice--;

		game.CoosenCookie = game.Cookies[playerChoice];
		
		while (game.Cookies[playerChoice].AlreadyPicked)
		{
			Console.WriteLine("Sorry that cookie has already been picked. Try Again");
			Console.WriteLine("Please choose a number between 1 and 9");
			playerChoice = Convert.ToInt32(Console.ReadLine());
			playerChoice--;
		}

		game.Cookies[playerChoice].AlreadyPicked = true;
		
		return game;
	}

	public static Game IsItARasin(this Game game)
	{
		if (game.CoosenCookie.TypeOfCookie == CookieType.Rasin)
		{
			Console.WriteLine("Your chose the rasin cookie! Ewww! You lose!");
			game.gameOver = true;
		}
		else
		{
			Console.WriteLine("You chose a chocolate cookie! Yum!");
			game.CurrentPlayerIndex++;
		}
		
		return game;
	}
}

public class Game
{
	public List<Cookie> Cookies { get; set; } = new List<Cookie>();
	public List<Player> Players { get; set; } = new List<Player>();
	public int CurrentPlayerIndex { get; set; }
	public Cookie CoosenCookie { get; set; }
	public bool gameOver = false;
	
	

	public Game()
	{
		PopulateCookies();
		PopulatePlayers();
		CurrentPlayerIndex = 0;
		
	}

	public void PopulatePlayers()
	{
		int playerNum = 1;
		
		for (var i = 0; i < 2; i++)
		{
			Player player = new Player(playerNum);
			Players.Add(player);

			playerNum++;
		}
	}

	public void PopulateCookies()
	{
		Random rnd = new Random();
		int rndNum = rnd.Next(0, 9);

		for (var i = 0; i < 9; i++)
		{
			Cookie cookie = new Cookie();
			if (i == rndNum)
			{
				cookie.TypeOfCookie = CookieType.Rasin;
			}

			Cookies.Add(cookie);
		}
	}
}

public class Player
{
	public int Number { get; set; }

	public Player(int number)
	{
		Number = number;
	}
}

public class Cookie
{
	public CookieType TypeOfCookie { get; set; }
	public bool AlreadyPicked { get; set; }

	public Cookie()
	{
		AlreadyPicked = false;
	}
}

public enum CookieType
{
	Chocolate, Rasin
}