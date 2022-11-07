using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CombatState { START, PLAYER_TURN, ENEMY_TURN, END}

public class CombatSystem : MonoBehaviour
{
    GameManager _GameManager;
    Camera CameraMain;
    public LayerMask MovementMask;    

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    Unit playerUnit;
    Unit enemyUnit;

    public CombatState state;

    public CombatHUD playerHUD;

    private bool playerMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        _GameManager = GameManager.Instance;
        CameraMain = Camera.main;
    }
    void Update()
    {
        if (playerMoving)
        {
            Ray ray = CameraMain.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, MovementMask))
            {
                DrawMoveLine(hit.point);
            }               
        }
    }

    void SetupCombat()
    {
        state = CombatState.START;
        _GameManager.SetGameState(GameState.Combat);

        playerUnit = playerPrefab.GetComponent<Unit>();
        enemyUnit = enemyPrefab.GetComponent<Unit>();

        Debug.Log("Combat has Started");

        state = CombatState.PLAYER_TURN;
        PlayerTurn();
    }

    IEnumerator PlayerMove()
    {
        Debug.Log("Player Moves");        

        yield return new WaitForSeconds(0f);
    }

    void DrawMoveLine(Vector3 targetPos) {
        LineRenderer line = playerPrefab.GetComponent<LineRenderer>();
        Transform transform = playerPrefab.transform;
        NavMeshAgent agent = playerPrefab.GetComponent<NavMeshAgent>();

        NavMeshPath path = new NavMeshPath();
        
        if (agent.CalculatePath(targetPos, path))
        {
            line.enabled = true;

            line.SetPosition(0, transform.position);

            if(path.corners.Length < 2) //if the path has 1 or no corners, there is no need
                return;

            line.SetVertexCount(path.corners.Length); //set the array of positions to the amount of corners

            for(var i = 1; i < path.corners.Length; i++){
                line.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position
            }  
        }
        else
        {
            //don't draw a line if there is no viable path
            line.enabled = false;
        }
      
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        Debug.Log("Player Attacks");

        yield return new WaitForSeconds(0f);

        if (isDead)
        {
            EndBattle();
        }
        else
        {
            state = CombatState.ENEMY_TURN;
            StartCoroutine(EnemyTurn());            
        }
        
    }

    IEnumerator EnemyTurn()
    {
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);    

        Debug.Log("Enemy Attacks");

        Camera.main.GetComponent<CameraController>().target = enemyPrefab.transform;

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            EndBattle();
        }
        else
        {
            state = CombatState.PLAYER_TURN;
            PlayerTurn();            
        }
        
    }

    void EndBattle()
    {
        state = CombatState.END;
        _GameManager.SetGameState(GameState.World);

        Debug.Log("Battle is over");
    }

    void PlayerTurn()
    {
        Camera.main.GetComponent<CameraController>().target = playerPrefab.transform;
        Debug.Log("Player Turn");

        playerMoving = false;
    }

    public void OnAttackButton()
    {
        if (CombatState.PLAYER_TURN != state)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnMoveButton()
    {
        if (CombatState.PLAYER_TURN != state)
        {
            return;
        }

        playerMoving = true;

        StartCoroutine(PlayerMove());
    }

    public void OnStartCombatButton()
    {
        SetupCombat();
    }    
}
