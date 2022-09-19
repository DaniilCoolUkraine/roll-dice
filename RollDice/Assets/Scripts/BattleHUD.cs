using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;

    public Slider health;
    public Slider shield;

    //initial ui set
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.GetName();
        health.maxValue = unit.GetMaxHealth();
        SetHealth(unit.GetHealth());
    }

    //update health
    public void SetHealth(int amount)
    {
        health.value = amount;
    }

    //update shield
    public void SetShield(int amount)
    {
        shield.maxValue = amount;
        shield.value = amount;
    }
}
