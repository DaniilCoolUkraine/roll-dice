using UnityEngine;

//heal specified unit
public class Heal : MonoBehaviour, IAbility
{
    public void CastAbility(int level, Unit unit)
    {
        unit.Heal(level);
    }
}
