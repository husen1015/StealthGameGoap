using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : Action
{
    public GameObject player;
    Animator animator;
    public override bool PostPrefom()
    {
        //Debug.Log("post preforming chase");
        //Debug.Log($"running chase? {running}");
        //Debug.Log($"can see player?: {FOV.Instance.canSeePlayer}");
        //UnityEditor.EditorApplication.isPaused = true;

        //check if caught
        if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
        {
            Debug.Log("caught!");
            //play attack animation
        }
        //else player got away - investigate
        else if (!FOV.Instance.canSeePlayer)
        {
            GameWorld.Instance.GetWorldStates1().SetState("Sighted", 1);
            //animator.SetBool("run", false);
            StartCoroutine(stopRunning());

        }

        return true;
    }

    public override bool PrePrefom()
    {
        //animator.SetBool("run", true);
        //animator.SetFloat("shouldRun", 1);
        StartCoroutine(startRunning());
        target = player;

        agent.SetDestination(player.transform.position);
        return true;
    }
    private void Update()
    {
        if (target != null && running) // Check if the action is currently running
        {
            agent.SetDestination(target.transform.position);
        }
    }
    private void Start()
    {
        animator= GetComponent<Animator>();
    }
    IEnumerator startRunning()
    {
        float blendVal = animator.GetFloat("shouldRun");
        while(blendVal != 1)
        {
            blendVal += Time.deltaTime;
            blendVal = Math.Min(1, blendVal);
            animator.SetFloat("shouldRun", blendVal);
            yield return null;


        }
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
