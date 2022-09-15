using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour, IAbility
{
    public void CastAbility(int level)
    {
        Debug.Log($"Poison {level}");
    }
}
