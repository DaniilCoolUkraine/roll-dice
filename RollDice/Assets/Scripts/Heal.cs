using UnityEngine;

public class Heal : MonoBehaviour, IAbility
{
    //heal specified unit
    public void CastAbility(int level, Unit unit)
    {
        unit.Heal(level);
    }
}
