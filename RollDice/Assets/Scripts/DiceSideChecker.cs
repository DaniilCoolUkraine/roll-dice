using UnityEngine;

public class DiceSideChecker : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var dicevelocity = other.gameObject.GetComponentInParent<DiceThrow>().DiceVelocity;

        if (dicevelocity == Vector3.zero)
        {
            string nameOfSide = other.name;
            switch (nameOfSide)
            {
                case "Side1Trigger":
                    Debug.Log("Side6 here!");
                    break;
                case "Side2Trigger":
                    Debug.Log("Side5 here!");
                    break;
                case "Side3Trigger":
                    Debug.Log("Side4 here!");
                    break;
                case "Side4Trigger":
                    Debug.Log("Side3 here!");
                    break;
                case "Side5Trigger":
                    Debug.Log("Side2 here!");
                    break;
                case "Side6Trigger":
                    Debug.Log("Side1 here!");
                    break;
            }
        }
    }
}
