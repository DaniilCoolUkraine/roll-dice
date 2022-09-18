using UnityEngine;

public class Damage : MonoBehaviour, IAbility
{
    //damage specified unit
    public void CastAbility(int level, Unit unit)
    {
        unit.Damage(level, unit);
    }
}