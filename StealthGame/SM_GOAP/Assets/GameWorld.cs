using System.Collections.Generic;
using System.Collections;

using UnityEngine;

public sealed class GameWorld 
{
    private static readonly GameWorld instance = new GameWorld();
    private static WorldStates worldStates;
    static GameWorld()
    {
        worldStates = new WorldStates();
    }
    private GameWorld()
    {

    }
    public static GameWorld Instance
    {
        get { return instance; }
    }
    public Dictionary<string, int> GetWorldStates()
    {
        return worldStates.states;
    }
}
