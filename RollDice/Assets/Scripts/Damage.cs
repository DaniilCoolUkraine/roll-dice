using UnityEngine;

//damage specified unit
public class Damage : MonoBehaviour, IAbility
{
    public void CastAbility(int level, Unit unit)
    {
        unit.Damage(level, unit);
    }
}