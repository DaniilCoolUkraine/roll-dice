using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IAbility
{
    public void CastAbility(int level)
    {
        Debug.Log($"Shield {level}");
    }
}
