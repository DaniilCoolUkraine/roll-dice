using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public enum BattleState {START, PLAYER_TURN, ENEMY_TURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> enemy;

    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform enemyPosition;

    [SerializeField] private BattleHUD playerHUD;
    [SerializeField] private BattleHUD enemyHUD;

    [SerializeField] private DiceSideChecker playerSideChecker;
    [SerializeField] private DiceSideChecker enemySideChecker;
    
    [SerializeField] private List<DiceThrow> playerCubes;
    [SerializeField] private List<DiceThrow> enemyCubes;

    private Unit playerInformation;
    private Unit enemyInformation;

    public BattleState State { get; set; }

    public delegate void OnStateChangeHandler();
    public event OnStateChangeHandler OnStateChange;

    private GetAbility getAbility;
    
    private void Start()
    {
        State = BattleState.START;
        SetupBattle();
        getAbility = new GetAbility();
    }

    private void SetupBattle()
    {
        playerInformation = Instantiate(player, playerPosition).GetComponent<Unit>();
        enemyInformation = Instantiate(enemy[0], enemyPosition).GetComponent<Unit>();
        
        playerHUD.SetHUD(playerInformation);
        enemyHUD.SetHUD(enemyInformation);

        State = BattleState.PLAYER_TURN;
        StartCoroutine(PlayerTurn());
    }

    private IEnumerator PlayerTurn()
    {
        foreach (var playerCube in playerCubes)
            if (playerCube.gameObject.activeSelf)
                OnStateChange += playerCube.ThrowDice;
        
        yield return new WaitForSeconds(1f);
        
        OnStateChange?.Invoke();
        
        foreach (var playerCube in playerCubes)
            if (playerCube.gameObject.activeSelf)
                OnStateChange -= playerCube.ThrowDice;
        
        //do some battle stuff:
        // get cube ability
        List<IAbility> cubeAbilities = getAbility.GetCubeAbilities(playerSideChecker);
        List<int> abilityLevels = getAbility.GetAbilityLevel(playerSideChecker);
        
        // cast ability 
        for (int i = 0; i < cubeAbilities.Count; i++)
        {
            var ability = cubeAbilities[i];
            int level = abilityLevels[i];

            if (ability is Damage || ability is Poison)
            {
                ability.CastAbility(level, enemyInformation);
                enemyHUD.SetHealth(enemyInformation.GetHealth());
            }
            else
            {
                ability.CastAbility(level, playerInformation);
                playerHUD.SetShield(playerInformation.GetShield());
                playerHUD.SetHealth(playerInformation.GetHealth());
            }
        }
        // update ui
        enemyInformation.Shield(-enemyInformation.GetShield());
        enemyHUD.SetShield(enemyInformation.GetShield());
        // check hp
        if (enemyInformation.GetHealth() <= 0)
        {
            State = BattleState.WON;
            Debug.Log("u are winner");
        }
        else
        {
            yield return new WaitForSeconds(3f);
            State = BattleState.ENEMY_TURN;
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        foreach (var enemyCube in enemyCubes)
            if (enemyCube.gameObject.activeSelf)
                OnStateChange += enemyCube.ThrowDice;

        yield return new WaitForSeconds(1f);
        
        OnStateChange?.Invoke();

        foreach (var enemyCube in enemyCubes)
            if (enemyCube.gameObject.activeSelf)
                OnStateChange -= enemyCube.ThrowDice;
        
        //do some battle stuff:
        // get cube ability
        List<IAbility> cubeAbilities = getAbility.GetCubeAbilities(enemySideChecker);
        List<int> abilityLevels = getAbility.GetAbilityLevel(enemySideChecker);
        
        // cast ability 
        for (int i = 0; i < cubeAbilities.Count; i++)
        {
            var ability = cubeAbilities[i];
            int level = abilityLevels[i];

            if (ability is Damage || ability is Poison)
            {
                ability.CastAbility(level, playerInformation);
                playerHUD.SetHealth(playerInformation.GetHealth());
            }
            else
            {
                ability.CastAbility(level, enemyInformation);
                enemyHUD.SetShield(enemyInformation.GetShield());
                enemyHUD.SetHealth(enemyInformation.GetHealth());
            }
        }

        // update ui
        playerInformation.Shield(-playerInformation.GetShield());
        playerHUD.SetShield(playerInformation.GetShield());
        // check hp
        if (playerInformation.GetHealth() <= 0)
        {
            State = BattleState.LOST;
            Debug.Log("u are looser");
        }
        else
        {
            yield return new WaitForSeconds(3f);
            State = BattleState.PLAYER_TURN;
            StartCoroutine(PlayerTurn());
        }
    }
}