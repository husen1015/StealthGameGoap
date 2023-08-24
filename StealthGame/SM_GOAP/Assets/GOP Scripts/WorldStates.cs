
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WorldState
{
    public string key;
    public int value;

    public WorldState(string key, int value) 
    {
        this.key = key;
        this.value = value;
    }
}
public class WorldStates 
{
    //holds the states of the world
    public Dictionary<string, int> states;
    public WorldStates()
    {
        states = new Dictionary<string, int>();
    }
    public bool HasState(string key)
    {
        return states.ContainsKey(key); 
    }
    void addState(string key, int value)
    {
        states.Add(key, value);
    }

    //modify new or existing state with value
    public void ModifyState(string key, int value)
    {
        if (states.ContainsKey(key)){
            states[key] += value;
            if (states[key] <= 0)
            {
                states.Remove(key);
            }
            else
            {
                states.Add(key, value);
            }
        }

    }
    public void RemoveState(string key)
    {
        if (states.ContainsKey(key))
        {
            states.Remove(key);
        }
    }

    //set state value to given value
    public void SetState(string key, int value)
    {
        if (states.ContainsKey(key))
        {
            states[key] = value;
        }
        else
        {
            states.Add(key, value);
        }
    }

    public Dictionary<string,int> GetStates()
    {
        return states;
    }
}
