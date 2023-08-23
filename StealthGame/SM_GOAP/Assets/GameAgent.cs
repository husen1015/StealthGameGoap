using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;

public class SubGoal
{
    //each goal can have multiple subgoals represented by this class
    public Dictionary<string, int> SubGoals;
    public bool repeat; // signales whether or not this goal should be triggered again after its satisfies 
    public SubGoal(string goal, int priority, bool repeat) 
    {
        SubGoals= new Dictionary<string, int>();
        SubGoals.Add(goal, priority);
        this.repeat= repeat;
    }
}
public class GameAgent : MonoBehaviour
{
    public List<Action> actions = new List<Action>();
    public Dictionary<SubGoal, int> SubGoals = new Dictionary<SubGoal, int>();

    Planner planner;
    Queue<Action> actionsQueue;
    public Action currAction;
    SubGoal currentGoal;

    protected void Start()
    {
        //get actions assigned in inspector 
        Action[] actionsArr = this.GetComponents<Action>();
        foreach(Action action in actionsArr)
        {
            actions.Add(action);
        }
    }


    bool invoked = false; //action invoked?
    void CompleteAction()
    {
        Debug.Log("completeing action");
        currAction.running= false;
        currAction.PostPrefom();
        invoked = false;
    }
    private void LateUpdate()
    {
        
        //if agent has an action
        if(currAction!= null && currAction.running)
        {
            float distanceToTarget = Vector3.Distance(currAction.target.transform.position, transform.position);
            
            if(currAction.agent.hasPath && distanceToTarget < 2f)
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", currAction.duration);
                    invoked = true;
                }
            }
            return;
        }

        //agent has no plan
        if(planner == null || actionsQueue == null)
        {
            planner = new Planner();
            //sort goals according to improtance in a descendign order
            var sortedGoals = from entry in SubGoals orderby entry.Value descending select entry;
            //try to create a plan for a goal sttarting from the most important goal
            //UnityEditor.EditorApplication.isPlaying = false;
            foreach (KeyValuePair<SubGoal, int> sortedGoal in sortedGoals)
            {
                actionsQueue = planner.plan(actions, sortedGoal.Key.SubGoals, null);
                if(actionsQueue != null)
                {
                    currentGoal = sortedGoal.Key;
                    break;
                }
            }
        }
        //completed all actions
        if(actionsQueue != null && actionsQueue.Count == 0) 
        {
            if (!currentGoal.repeat)
            {
                SubGoals.Remove(currentGoal);
            }
            planner = null;
        }
        //there are still actions to do
        if(actionsQueue != null && actionsQueue.Count> 0)
        {
            currAction = actionsQueue.Dequeue();    
            if(currAction.PrePrefom()) 
            {
                //if navmesh tag not assigned in inspector assign it with targetTag
                if(currAction.target == null && currAction.targetTag != "")
                {
                    currAction.target = GameObject.FindWithTag(currAction.targetTag);
                }
                if(currAction.target != null)
                {
                    currAction.running= true;
                    currAction.agent.SetDestination(currAction.target.transform.position);
                }
            }
            else { actionsQueue = null; }//this will force us to get a new planner and try again
        }
    }
}
