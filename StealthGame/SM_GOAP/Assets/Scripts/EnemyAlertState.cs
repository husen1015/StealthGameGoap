using UnityEngine;

public class EnemyAlertState : EnemyBaseState
{
    public override void enterState(EnemyStateManager manager)
    {
        Debug.Log("ALERT!");
        agent.destination = player.position;
    }

    public override void updateState(EnemyStateManager manager)
    {
        agent.destination = player.position;
        //if distance becomes too great investigate last known location of player
        if(Vector3.Distance(enemy.transform.position, player.transform.position) > 10f)
        {
            manager.SwitchState(manager.investigateState);
        }

        //if caught player endGame
        if (Vector3.Distance(enemy.transform.position, player.transform.position) < 0.3f)
        {
            manager.caughtPlayer();
        }
    }
}
