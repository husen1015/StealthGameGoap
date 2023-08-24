using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Action : MonoBehaviour
{
    public string Name = "Action";
    public float cost = 1f;
    public GameObject target; //target for the navmesh
    public string targetTag;
    public float duration = 0; //time needed to complete
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public NavMeshAgent agent;

    public Dictionary<string, int> pre_conditions;
    public Dictionary<string, int> after_effects;

    public WorldStates agentBeleifs; //internal states

    public bool running = false; //is the action currently running?

    public Action()
    {
        pre_conditions= new Dictionary<string, int>();
        after_effects= new Dictionary<string, int>();
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        //populate world states
        if(preConditions!=null)
        {
            foreach(WorldState worldState in preConditions)
            {
                pre_conditions.Add(worldState.key, worldState.value);
            }
        }
        if (afterEffects != null)
        {
            foreach (WorldState worldState in afterEffects)
            {
                after_effects.Add(worldState.key, worldState.value);
            }
        }
    }

    //currently assuming all actions are achievable. might change later
    public bool IsAcheivable()
    {
        return true;
    }
    //this method determines whether the action is achievable given a set of conditions
    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        //make sure all required conditions are found in pre_conditions
        foreach (KeyValuePair<string, int> p in pre_conditions)
        {
            if (!conditions.ContainsKey(p.Key)) { return false; }
        }
        return true;
    }
    //done before invoking the action
    public abstract bool PrePrefom();
    //run after done with the action
    public abstract bool PostPrefom();


}
