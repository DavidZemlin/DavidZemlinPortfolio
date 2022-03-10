// CSE 110     : CES 110 / Online
// Assignment  : 07
// Author      : David Zemlin 1223802827
// Description : a monster who loves to do battle for sport

public class Monster 
{
	// Instance Variables
	private String name;
	private int healthScore;
	private Weapon weapon;
	
	// Constructors
	public Monster(String newName, int newHealth, Weapon monstersWeapon)
	{
		name = newName;
		healthScore = newHealth;
		weapon = monstersWeapon;
	}
	
	// Getters
	/**
	 * Returns the name of this monster.
	 * @return this monster's name.
	 */
	public String getName()
	{
		return name;
	}
	
	/**
	 * Returns the health score of this monster.
	 * @return this monster's health score.
	 */
	public int getHealthScore()
	{
		return healthScore;
	}
	
	/**
	 * Returns the name of this monsters trusty weapon.
	 * @return the name of the monster's weapon.
	 */
	public String getWeaponName()
	{
		return weapon.getName();
	}
	
	// Public Methods
	/**
	 * Reduces this monsters health by the given amount of damage.
	 * @param damage amount of damage to deal.
	 */
	public void takeDamage(int damage)
	{
		healthScore -= damage;
	}
	
	/**
	 * Attacks another monster.
	 * @param enemy the target monster.
	 * @return a report of the amount of damage dealt to this monster via this method.
	 */
	public int attack(Monster enemy)
	{
		int damage = (int) (Math.random() * weapon.getMaxDamage());
		enemy.takeDamage(damage);
		return damage;
	}
}
