using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class GetAbility
{
    public List<IAbility> GetCubeAbilities(DiceSideChecker sideChecker)
    {
        List<IAbility> _cubeAbilities = new List<IAbility>();
        
        List<CubeSide> cubeSide = sideChecker.cubeSide.Distinct().ToList();
        if (cubeSide.Count != 0)
        {
            foreach (var cube in cubeSide)
            {
                int childOrder = cube.Side;
                GameObject child = cube.Cube.transform.GetChild(childOrder - 1).gameObject;
                
                _cubeAbilities.Add(child.GetComponent<IAbility>());
                //child.GetComponent<IAbility>().CastAbility(GetLevel(child.transform.GetChild(1).name), null);
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
                
                string extractedNum = Regex.Match(child.transform.GetChild(1).name, @"\d+").Value;
                int level = Int32.Parse(extractedNum);

                _abilityLevels.Add(level);
            }
        }

        return _abilityLevels;
    }
}