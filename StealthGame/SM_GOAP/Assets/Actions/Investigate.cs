using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigate : Action
{
    public GameObject player;
    public override bool PostPrefom()
    {
        Debug.Log("post preforming investigate");
        Debug.Log($"running investigate? {running}");
        //UnityEditor.EditorApplication.isPaused = true;
        GameWorld.Instance.GetWorldStates1().RemoveState("Sighted");
        return true;
    }

    public override bool PrePrefom()
    {
        GameObject tar = new GameObject();
        tar.transform.position = player.transform.position;
        target = tar;


        return true;
    }


}
