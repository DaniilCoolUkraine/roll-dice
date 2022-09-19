using UnityEngine;

//place shield on specified unit
public class Shield : MonoBehaviour, IAbility
{
    public void CastAbility(int level, Unit unit)
    {
        unit.Shield(level);
    }
}
