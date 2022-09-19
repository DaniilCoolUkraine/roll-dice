using System.Collections.Generic;
using UnityEngine;

//class to check side of cube
public class DiceSideChecker : MonoBehaviour
{
    //list to store multiple cubes and its sides 
    public List<CubeSide> cubeSide = new List<CubeSide>();
    
    private void OnTriggerStay(Collider other)
    {
        var diceVelocity = other.gameObject.GetComponentInParent<DiceThrow>().DiceVelocity;
     
        //check if dice velocity is zero
        if (diceVelocity == Vector3.zero)
        {
            //get name of the side
            string nameOfSide = other.name;
            //get parent of trigger 
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

    //cube factory
    private CubeSide GetCube(GameObject parent, int side)
    {
        return new CubeSide(parent, side);
    }
    
}

//struct to store cube and side facing up
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