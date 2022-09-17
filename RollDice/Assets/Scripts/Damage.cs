using UnityEngine;

public class Damage : MonoBehaviour, IAbility
{
    public void CastAbility(int level)
    {
        Debug.Log($"Damaged {level}");
    }
}