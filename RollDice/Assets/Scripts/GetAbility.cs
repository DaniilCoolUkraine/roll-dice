using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

//class to get ability of cube and its level
public class GetAbility
{
    public List<IAbility> GetCubeAbilities(DiceSideChecker sideChecker)
    {
        List<IAbility> _cubeAbilities = new List<IAbility>();
        
        //delete duplicated objects
        List<CubeSide> cubeSide = sideChecker.cubeSide.Distinct().ToList();
        if (cubeSide.Count != 0)
        {
            foreach (var cube in cubeSide)
            {
                int childOrder = cube.Side;
                GameObject child = cube.Cube.transform.GetChild(childOrder - 1).gameObject;
                
                _cubeAbilities.Add(child.GetComponent<IAbility>());
            }
        }

        return _cubeAbilities;
    }

    public List<int> GetAbilityLevel(DiceSideChecker sideChecker)
    {
        List<int> _abilityLevels = new List<int>();
        
        List<CubeSide> cubeSide = sideChecker.cubeSide.Distinct().ToList();
        if (cubeSide.Count != 0)
        {
            foreach (var cube in cubeSide)
            {
                int childOrder = cube.Side;
                GameObject child = cube.Cube.transform.GetChild(childOrder - 1).gameObject;
                
                //find number inside ability name
                string extractedNum = Regex.Match(child.transform.GetChild(1).name, @"\d+").Value;
                int level = Int32.Parse(extractedNum);

                _abilityLevels.Add(level);
            }
        }

        return _abilityLevels;
    }
}