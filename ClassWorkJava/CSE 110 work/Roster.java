// CSE 110     : CES 110 / Online
// Assignment  : 08
// Author      : David Zemlin 1223802827
// Description : a class that manages a list of players on a team

import java.util.ArrayList;
import java.io.FileReader;
import java.io.BufferedReader;
import java.io.IOException;

public class Roster 
{
	// instance variables
	private ArrayList<Player> players;
	
	// constructors
	public Roster()
	{
		players = new ArrayList<Player>();
	}
	
	/**
	 * Constructs this roster with a specific roster.dat file as a parameter.
	 * @param fileName name of the file to be used
	 */
	public Roster(String fileName) throws IOException
	{
		players = new ArrayList<Player>();
		importRoster(fileName);
	}
	
	// public methods
	/**
	 * Creates a new Player and adds it to the roster's list.
	 * @param name of the new player
	 * @param attack stat of the new player
	 * @param block stat of the new player
	 */
	public void addPlayer(String name, double attack, double block)
	{
		players.add(new Player(name, attack, block));
	}
	
	/**
	 * Gives a count of all players in the team roster.
	 * @return number of players
	 */
	public int countPlayers()
	{
		return players.size();
	}
	
	/**
	 * Searches for a player by name and returns a reference to them.
	 * @param name name to be searched for
	 * @return player with designated name (null if no player matches)
	 */
	public Player getPlayerByName(String name)
	{
		Player who = null;
		for (Player player: players)
		{
			if (name.equals(player.getName()))
			{
				who = player;
			}
		}
		return who;
	}
	
	/**
	 * Finds the top 2 attackers on the team and print's their stats.
	 */
	public void printTopAttackers()
	{
		printTopTwo(true);
	}
	
	/**
	 * Finds the top 2 blockers on the team and print's their stats.
	 */
	public void printTopBlockers()
	{
		printTopTwo(false);
	}
	
	/**
	 * Prints the information of all players on the loaded roster
	 */
	public void printAllPlayers()
	{
		for (Player player: players)
		{
			player.printPlayerInfo();
		}
	}
	
	// private methods
	/**
	 * Finds and prints the best 2 players based on their attack or block stats.
	 * @param findAttack set true if searching by attack stat; set false if searching by block stat
	 */
	private void printTopTwo(boolean findAttack)
	{
		int best = 0;
		int nextBest = 0;
		
		// first check if the list is more than 1 player long
		if (players.size() == 0)
		{
			System.out.print("No players on the team.");
		}
		else if (players.size() == 1)
		{
			System.out.print(players.get(0).getName());
		}
		
		// ... else, list is more than 1 player long, so find the top 2
		else
		{
			// find the best
			for (int i = 1; i < players.size(); i++)
			{
				// search by attack stat or...
				if(findAttack && players.get(i).getAttackStat() > players.get(best).getAttackStat())
				{
					best = i;
				}
				
				// ... search by block stat
				else if(!findAttack && players.get(i).getBlockStat() > players.get(best).getBlockStat())
				{
					best = i;
				}
			}
			
			// if the best and default next best are the same, change the next best to a different player index
			if (best == nextBest)
			{
				nextBest -= 1;
				if (nextBest < 0)
				{
					nextBest = players.size() - 1;
				}
			}
			
			// check all players that are not the best, to find the next best
			for (int i = 0; i < players.size(); i++)
			{
				// search by attack stat or...
				if(findAttack && players.get(i).getAttackStat() > players.get(nextBest).getAttackStat() && i != best)
				{
					nextBest = i;
				}
				
				// ... search by block stat
				if(!findAttack && players.get(i).getBlockStat() > players.get(nextBest).getBlockStat() && i != best)
				{
					nextBest = i;
				}
			}
			players.get(best).printPlayerInfo();
			players.get(nextBest).printPlayerInfo();
		}
	}
	
	/**
	 * Imports a new roster from a .dat file
	 * @param fileName name of roster file
	 * @throws NumberFormatException if the file is not in the proper format
	 */
	private void importRoster(String fileName) throws IOException
	{
		try (FileReader reader = new FileReader(fileName); BufferedReader inFile= new BufferedReader(reader))
		{
			players.clear();
			String line = inFile.readLine();
			while (line != null)
			{
				String name = "";
				String attack = "";
				String block = "";
				boolean attackFound = false;
				int startPoint = 0;
				
				// break up the line into a name, attack, and block
				for (int i = 0; i < line.length(); i++)
				{
					if (!attackFound && Character.isDigit(line.charAt(i)))
					{
						name = line.substring(0, i - 1);
						startPoint = i;
						attackFound = true;
					}
					else if (attackFound && Character.isWhitespace(line.charAt(i)))
					{
						attack = line.substring(startPoint, i);
						block = line.substring(i).trim();
					}
				}
				
				// add player information, throw an exception if file format was incorrect
				if (name.length() == 0 || attack.length() == 0 || block.length() == 0)
				{
					throw new NumberFormatException("Roster file not in expected format");
				}
				else
				{
					addPlayer(name, Double.parseDouble(attack), Double.parseDouble(block));
				}
				line = inFile.readLine();
			}
		}
		// all other exceptions are caught here
		catch (IOException exception)
		{
			System.out.println(exception);
		}
	}
}
