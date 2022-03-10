// CSE 110     : CES 110 / Online
// Assignment  : 08
// Author      : David Zemlin 1223802827
// Description : a class that contains basic player info

public class Player 
{
	// instance variables
	private String name;
	private double attackStat;
	private double blockStat;
	
	// constructors
	public Player(String playerName, double attack, double block)
	{
		name = playerName;
		attackStat = attack;
		blockStat = block;
	}
	
	// getters
	public String getName()
	{
		return name;
	}
	
	public double getAttackStat()
	{
		return attackStat;
	}
	
	public double getBlockStat()
	{
		return blockStat;
	}
	
	//setters
	public void setAttackStat(double newAttack)
	{
		attackStat = newAttack;
	}
	
	public void setBlockStat(double newBlock)
	{
		blockStat = newBlock;
	}
	
	// public methods
	public void printPlayerInfo()
	{
		System.out.printf("%s (attack = %.2f, block = %.2f)%n", name, attackStat, blockStat);
	}
}
