using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor.Search;

//represents a single node in the graph
public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public Action action;

    public Node(Node parent, float cost, Dictionary<string, int> states, Action action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(states);
        this.action = action;
    }
}
// the planner gets actions, goals and world states and then builds a graph of possible plans then it searches in this graph to find the cheapest plan that satisfies the given goal
public class Planner 
{
    //builds a graph and returns a queue of actions representing the best/cheapest plan 
    public Queue<Action> plan(List<Action> actions, Dictionary<string, int> goal, WorldStates states)
    {
        //filter any actions that are not achievable
        List<Action> usableActions = new List<Action>();
        foreach (Action action in actions)
        {
            if (action.IsAcheivable())
            {
                usableActions.Add(action);
            }
        }
        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0, GameWorld.Instance.GetWorldStates(), null); //root node 
        bool success = BuildGraph(start, leaves, usableActions, goal);
        if (!success)
        {
            UnityEngine.Debug.Log("NO PLAN");
            return null;
        }
        //find cheapes node
        Node cheapest = null;
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
            {
                cheapest = leaf;
            }
            else
            {
                if (leaf.cost < cheapest.cost)
                {
                    cheapest = leaf;
                }
            }
        }

        //once cheapes node is found, work up through its parents to form a plan and add them to the queue.
        //each node contains its cost+cost of its parent so the leafs contain the cost of the whole branch

        Stack<Action> result = new Stack<Action>();
        Node currNode = cheapest;
        while (currNode != null)
        {
            if (currNode.action != null)
            {
                result.Push(currNode.action);
            }
            currNode = currNode.parent;
        }
        Queue<Action> queue = new Queue<Action>();
        foreach (Action a in result)
        {
            queue.Enqueue(a);
        }
        UnityEngine.Debug.Log("the plan is :");
        foreach (Action a in queue)
        {
            UnityEngine.Debug.Log("Q: " + a.Name);
        }
        return queue;

    }

    //recursive function that builds a graph of actions based on a goal.
    private bool BuildGraph(Node parent, List<Node> leaves, List<Action> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;
        foreach(Action action in usableActions)
        {
            if (action.IsAchievableGiven(parent.state))
            {
                //each action inherits the state of its parent + the action's after effects
                Dictionary<string,int> currentState = new Dictionary<string,int>(parent.state);
                foreach(KeyValuePair<string, int> effect in action.after_effects)
                {
                    if (!currentState.ContainsKey(effect.Key))
                    {
                        currentState.Add(effect.Key, effect.Value);
                    }
                }
                //create new node representing the current action
                Node node = new Node(parent, parent.cost + action.cost, currentState, action);
                //check if goal was achieved
                if(GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    //remove this action from the available actions and build a graph for the resulting subset
                    List<Action> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if(found)
                    {
                        foundPath = true;
                    }
                }
            } 
        }
        return foundPath;

    }


    // this method removes a given action from the list of actions 
    private List<Action> ActionSubset(List<Action> actions, Action removeMe)
    {
        List<Action> subset = new List<Action>();
        foreach(Action a in actions)
        {
            if(!a.Equals(removeMe))
            {
                subset.Add(a);
            }
        }
        return subset ;
    }

    //make sure all states contained in our goal are found in the given state
    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach(KeyValuePair<string,int> g in goal)
        {
            if (!state.ContainsKey(g.Key)){
                return false;
            }
        }
        return true;
    }


}
