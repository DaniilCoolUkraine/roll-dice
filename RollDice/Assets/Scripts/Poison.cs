using UnityEngine;

//poison specified unit
public class Poison : MonoBehaviour, IAbility
{
    public void CastAbility(int level, Unit unit)
    {
        unit.Damage(level, unit);
    }
}
