using UnityEngine;

public class Poison : MonoBehaviour, IAbility
{
    //poison specified unit
    public void CastAbility(int level, Unit unit)
    {
        unit.Damage(level, unit);
    }
}
