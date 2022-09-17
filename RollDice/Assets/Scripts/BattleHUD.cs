using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;

    public Slider health;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.GetName();
        health.maxValue = unit.GetMaxHealth();
        SetHealth(unit.GetHealth());
    }

    public void SetHealth(int hp)
    {
        health.value = hp;
    }
}
