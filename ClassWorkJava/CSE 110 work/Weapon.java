// CSE 110     : CES 110 / Online
// Assignment  : 07
// Author      : David Zemlin 1223802827
// Description : a weapon that a monster might use for bringing the pain

public class Weapon
{
	// Instance Variables
	private String name;
	private int maxDamage;
	
	// Constructors
	/**
	 * Creates a new weapon with default name and damage.
	 */
	public Weapon()
	{
		name = "Pointy Stick";
		maxDamage = 1;
	}
	
	/**
	 * Creates a new weapon with specific name and damage range.
	 * @param wepName name of this weapon.
	 * @param wepMaxDamage the max damage of this weapon.
	 */
	public Weapon(String wepName, int wepMaxDamage)
	{
		name = wepName;
		maxDamage = wepMaxDamage;
	}
	
	// Getters
	/**
	 * Returns the name of this weapon.
	 * @return name of this weapon.
	 */
	public String getName()
	{
		return name;
	}
	
	/**
	 * Returns the max damage of this weapon.
	 * @return the weapon's max damage.
	 */
	public int getMaxDamage()
	{
		return maxDamage;
	}
	
	// Setters
	/**
	 * Sets the name of the weapon.
	 * @param newName the new name for the weapon.
	 */
	public void setName(String newName)
	{
		name = newName;
	}
	
	/**
	 * Sets the max damage of the weapon.
	 * @param newMaxDamage the new max damage value.
	 */
	public void setMaxDamage(int newMaxDamage)
	{
		maxDamage = newMaxDamage;
	}
}
