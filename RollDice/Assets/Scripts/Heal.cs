using UnityEngine;

public class Heal : MonoBehaviour, IAbility
{
    public void CastAbility(int level)
    {
        Debug.Log($"Healed {level}");
    }
}
