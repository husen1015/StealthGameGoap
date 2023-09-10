using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigate : Action
{
    public GameObject player;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
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
    IEnumerator stopRunning()
    {
        float blendVal = animator.GetFloat("shouldRun");
        while (blendVal != 0)
        {
            blendVal -= Time.deltaTime;
            blendVal = Math.Max(0, blendVal);
            animator.SetFloat("shouldRun", blendVal);
            yield return null;


        }
    }

}
