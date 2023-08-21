using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public Transform Player;
    public Transform Enemy;
    public EnemyBaseState currentState;
    public EnemyPatrolState patrolState = new EnemyPatrolState();
    public EnemyInvestigateState investigateState = new EnemyInvestigateState();
    public EnemyAlertState alertState = new EnemyAlertState();

    // Start is called before the first frame update
    void Start()
    {
        patrolState.initState(this);
        alertState.initState(this);
        investigateState.initState(this);

        currentState = patrolState;
        currentState.enterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.updateState(this);
        //Debug.Log(currentState);
    }
    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        currentState.enterState(this);
    }
    public void caughtPlayer()
    {
        Debug.Log("game over:/");
    }
}
