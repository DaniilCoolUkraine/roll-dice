using UnityEngine;

public class Shield : MonoBehaviour, IAbility
{
    //place shield on specified unit
    public void CastAbility(int level, Unit unit)
    {
        unit.Shield(level);
    }
}
