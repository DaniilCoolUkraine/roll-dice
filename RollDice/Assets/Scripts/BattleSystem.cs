using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private List<DiceThrow> playerCubes;
    [SerializeField] private List<DiceThrow> enemyCubes;
    
    private Unit playerInformation;
    private Unit enemyInformation;
    
    public BattleState State { get; set; }

    public delegate void OnStateChangeHandler();
    public event OnStateChangeHandler OnStateChange;

    private void Start()
    {
        State = BattleState.START;
        SetupBattle();
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
            OnStateChange += playerCube.ThrowDice;
        
        yield return new WaitForSeconds(1f);
        
        OnStateChange?.Invoke();
        
        foreach (var playerCube in playerCubes)
            OnStateChange -= playerCube.ThrowDice;
        
        //do some battle stuff:
        // get cube ability
        // cast ability 
        // update ui
        
        yield return new WaitForSeconds(3f);

        State = BattleState.ENEMY_TURN;
        StartCoroutine(EnemyTurn());
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
        // cast ability 
        // update ui
        
        yield return new WaitForSeconds(3f);

        State = BattleState.PLAYER_TURN;
        StartCoroutine(PlayerTurn());
    }
    
}
