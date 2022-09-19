using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {START, PLAYER_TURN, ENEMY_TURN, WON, LOST}

//class defines turn-based battle system
public class BattleSystem : MonoBehaviour
{
    [SerializeField] private GameObject player;
    //list of enemies
    [SerializeField] private List<GameObject> enemy;

    //variables to store positions where to spawn 
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform enemyPosition;

    //variables to update ui
    [SerializeField] private BattleHUD playerHUD;
    [SerializeField] private BattleHUD enemyHUD;

    //triggers that check side of dice
    [SerializeField] private DiceSideChecker playerSideChecker;
    [SerializeField] private DiceSideChecker enemySideChecker;
    
    //lists to store player and enemy cubes
    [SerializeField] private List<DiceThrow> playerCubes;
    [SerializeField] private List<DiceThrow> enemyCubes;
    
    private Unit _playerInformation;
    private Unit _enemyInformation;

    public BattleState State { get; set; }

    //delegate and event to invoke after turn changes
    public delegate void OnStateChangeHandler();
    public event OnStateChangeHandler OnStateChange;
    
    private GetAbility _getAbility;
    
    private void Start()
    {
        State = BattleState.START;
        SetupBattle();
        _getAbility = new GetAbility();
    }

    //function to initiate every battle
    private void SetupBattle()
    {
        _playerInformation = Instantiate(player, playerPosition).GetComponent<Unit>();
        _enemyInformation = Instantiate(enemy[0], enemyPosition).GetComponent<Unit>();
        
        playerHUD.SetHUD(_playerInformation);
        enemyHUD.SetHUD(_enemyInformation);

        State = BattleState.PLAYER_TURN;
        StartCoroutine(PlayerTurn());
    }

    //coroutine defines player turn and abilities
    private IEnumerator PlayerTurn()
    {
        //subscribe to cube throw
        foreach (var playerCube in playerCubes)
            //check if object isn`t active 
            if (playerCube.gameObject.activeSelf)
                OnStateChange += playerCube.ThrowDice;
        
        yield return new WaitForSeconds(1f);
        
        OnStateChange?.Invoke();

        //unsubscribe from cube throw
        foreach (var playerCube in playerCubes)
            //check if object isn`t active 
            if (playerCube.gameObject.activeSelf)
                OnStateChange -= playerCube.ThrowDice;
        
        // get cube ability
        List<IAbility> cubeAbilities = _getAbility.GetCubeAbilities(playerSideChecker);
        List<int> abilityLevels = _getAbility.GetAbilityLevel(playerSideChecker);
        
        // cast ability 
        for (int i = 0; i < cubeAbilities.Count; i++)
        {
            //get ability and its level
            var ability = cubeAbilities[i];
            int level = abilityLevels[i];

            //check whether ability is damage or poison
            //if it is -- cast to enemy. else -- cast to player
            if (ability is Damage || ability is Poison)
            {
                ability.CastAbility(level, _enemyInformation);
                enemyHUD.SetHealth(_enemyInformation.GetHealth());
            }
            else
            {
                ability.CastAbility(level, _playerInformation);
                playerHUD.SetShield(_playerInformation.GetShield());
                playerHUD.SetHealth(_playerInformation.GetHealth());
            }
        }
        // update ui
        _enemyInformation.Shield(-_enemyInformation.GetShield());
        enemyHUD.SetShield(_enemyInformation.GetShield());
        // check hp and move to next step depending on result
        if (_enemyInformation.GetHealth() <= 0)
        {
            State = BattleState.WON;
            EndBattle();
        }
        else
        {
            yield return new WaitForSeconds(3f);
            State = BattleState.ENEMY_TURN;
            StartCoroutine(EnemyTurn());
        }
    }
    
    //coroutine defines enemy turn and abilities
    private IEnumerator EnemyTurn()
    {
        //subscribe to cube throw
        foreach (var enemyCube in enemyCubes)
            //check if object isn`t active 
            if (enemyCube.gameObject.activeSelf)
                OnStateChange += enemyCube.ThrowDice;

        yield return new WaitForSeconds(1f);
        
        OnStateChange?.Invoke();

        //unsubscribe from cube throw
        foreach (var enemyCube in enemyCubes)
            //check if object isn`t active 
            if (enemyCube.gameObject.activeSelf)
                OnStateChange -= enemyCube.ThrowDice;
        
        //do some battle stuff:
        // get cube ability
        List<IAbility> cubeAbilities = _getAbility.GetCubeAbilities(enemySideChecker);
        List<int> abilityLevels = _getAbility.GetAbilityLevel(enemySideChecker);
        
        // cast ability 
        for (int i = 0; i < cubeAbilities.Count; i++)
        {
            //get ability and its level
            var ability = cubeAbilities[i];
            int level = abilityLevels[i];

            //check whether ability is damage or poison
            //if it is -- cast to enemy. else -- cast to player
            if (ability is Damage || ability is Poison)
            {
                ability.CastAbility(level, _playerInformation);
                playerHUD.SetHealth(_playerInformation.GetHealth());
            }
            else
            {
                ability.CastAbility(level, _enemyInformation);
                enemyHUD.SetShield(_enemyInformation.GetShield());
                enemyHUD.SetHealth(_enemyInformation.GetHealth());
            }
        }

        // update ui
        _playerInformation.Shield(-_playerInformation.GetShield());
        playerHUD.SetShield(_playerInformation.GetShield());
        // check hp and move to next step depending on result
        if (_playerInformation.GetHealth() <= 0)
        {
            State = BattleState.LOST;
            EndBattle();
        }
        else
        {
            yield return new WaitForSeconds(3f);
            State = BattleState.PLAYER_TURN;
            StartCoroutine(PlayerTurn());
        }
    }

    private void EndBattle()
    {
        if (State == BattleState.WON)
        {
            Debug.Log("u are winner");
        }
        else if (State == BattleState.LOST)
        {
            Debug.Log("u are looser");
        }
    }
}