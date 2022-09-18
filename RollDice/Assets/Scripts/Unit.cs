using UnityEngine;

public class Unit : MonoBehaviour, IDamageable, IReinforced
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int shield;
    [SerializeField] private string unitName;

    public void TakeDamage(int amount)
    {
        if (shield > amount)
            shield -= amount;
        else if (shield > 0 && shield < amount)
        {
            amount -= shield;
            shield = 0;
            currentHealth -= amount;
        }
        else
            currentHealth -= amount;
    }

    public void Damage(int amount, IDamageable unit)
    {
        unit.TakeDamage(amount);
    }
    
    public void Heal(int amount)
    {
        currentHealth += amount;
    }

    public void Shield(int amount)
    {
        shield += amount;
    }

    public string GetName() => unitName;

    public int GetHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;

    public int GetShield() => shield;
}
