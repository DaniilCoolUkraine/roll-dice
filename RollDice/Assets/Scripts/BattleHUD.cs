using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;

    public Slider health;
    public Slider shield;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.GetName();
        health.maxValue = unit.GetMaxHealth();
        SetHealth(unit.GetHealth());
    }

    public void SetHealth(int amount)
    {
        health.value = amount;
    }

    public void SetShield(int amount)
    {
        shield.maxValue = amount;
        shield.value = amount;
    }
}
