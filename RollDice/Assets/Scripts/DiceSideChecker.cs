using System.Collections.Generic;
using UnityEngine;

public class DiceSideChecker : MonoBehaviour
{
    public List<CubeSide> cubeSide = new List<CubeSide>();
    
    private void OnTriggerStay(Collider other)
    {
        var diceVelocity = other.gameObject.GetComponentInParent<DiceThrow>().DiceVelocity;
        
        if (diceVelocity == Vector3.zero)
        {
            string nameOfSide = other.name;
            var parent = other.transform.parent.parent;

            switch (nameOfSide)
            {
                case "Side1Trigger":
                    cubeSide.Add(GetCube(parent.gameObject, 6));
                    break;
                case "Side2Trigger":
                    cubeSide.Add(GetCube(parent.gameObject, 5));
                    break;
                case "Side3Trigger":
                    cubeSide.Add(GetCube(parent.gameObject, 4));
                    break;
                case "Side4Trigger":
                    cubeSide.Add(GetCube(parent.gameObject, 3));
                    break;
                case "Side5Trigger":
                    cubeSide.Add(GetCube(parent.gameObject, 2));
                    break;
                case "Side6Trigger":
                    cubeSide.Add(GetCube(parent.gameObject, 1));
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        cubeSide.Clear();
    }

    private CubeSide GetCube(GameObject parent, int side)
    {
        return new CubeSide(parent, side);
    }
    
}

public struct CubeSide
{
    public GameObject Cube { get; }
    public int Side { get; }

    public CubeSide(GameObject cube, int side)
    {
        Cube = cube;
        Side = side;
    }
}