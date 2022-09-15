using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class ShowAbility : MonoBehaviour
{
    private DiceSideChecker side;
    
    private void Awake()
    {
        side = FindObjectOfType<DiceSideChecker>();
    }
    void Update()
    {
        
    }

    public void Show()
    {
        List<CubeSide> cubeSide = side.cubeSide.Distinct().ToList();

        if (cubeSide.Count != 0)
        {
            foreach (var side in cubeSide)
            {
                int childOrder = side.Side;
                GameObject child = side.Cube.transform.GetChild(childOrder - 1).gameObject;
                child.GetComponent<IAbility>().CastAbility(GetLevel(child.transform.GetChild(1).name));
            }
        }
    }
    
    private int GetLevel(string name)
    {
        string extractedNum = Regex.Match(name, @"\d+").Value;
        int level = Int32.Parse(extractedNum);
        return level;
    }
}
