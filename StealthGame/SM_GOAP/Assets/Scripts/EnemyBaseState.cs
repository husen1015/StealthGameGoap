using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseState
{
    public enum states
    {
        PATROL,
        ALERT,
        INVESTIGATE
    }

    protected Transform enemy;
    protected Transform player;
    protected NavMeshAgent agent;
    public virtual void initState(EnemyStateManager manager)
    {
        enemy = manager.Enemy;
        player = manager.Player;
        agent = manager.GetComponent<NavMeshAgent>();
    }
    /// <summary>
    /// this mehtod is called whenever this state is entered
    /// </summary>
    public abstract void enterState(EnemyStateManager manager);

    /// <summary>
    /// this function runs every frame and updates behavior based on the current state of the game
    /// </summary>
    /// <param name="manager"></param>
    public abstract void updateState(EnemyStateManager manager);
}
