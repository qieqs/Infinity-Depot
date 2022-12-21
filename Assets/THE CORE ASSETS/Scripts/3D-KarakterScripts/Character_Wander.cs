using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Character_Wander : MonoBehaviour
{
    [HideInInspector]
    public bool isWalkingToInteract;
    Animator animator;
    NavMeshAgent agent;
    Character_AI manager;
    private string componentstring = "wander";

    public string LoopCommando;
    private IEnumerator routine;


    private Vector3 previousPosition;

    [HideInInspector]
    public float curSpeed;

    public float loopsnelheid;

    void Start()
    {
        manager = this.GetComponent<Character_AI>();
        agent = manager.agent;
        manager.components.Add(componentstring);
        animator = manager.animator;
    }


    void Update()
    {
        if (manager.funtionAvailable && manager.currentactivity == componentstring)
        {
            WanderTarget();
            manager.funtionAvailable = false;
        }
        else if(!manager.funtionAvailable && manager.currentactivity == "")
        {
            stoproutine();
        }
    }

    void stoproutine()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }
        agent.SetDestination(gameObject.transform.position);
        animator.SetBool(LoopCommando, false);
    }

    void calculateSpeed()
    {
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;
        animator.speed = curSpeed / loopsnelheid;
    }


    void WanderTarget()
    {
        manager.funtionAvailable = false;
        agent.speed = loopsnelheid;
        Vector3 randompoint = (Random.insideUnitSphere * 50) + transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randompoint, out hit, 100f, NavMesh.AllAreas);
        Vector3 finalPosition = hit.position;
        if(routine!= null)
        {
            StopCoroutine(routine);
        }
        routine = ReachTarget(finalPosition);
        StartCoroutine(routine);
    }
    
    private IEnumerator ReachTarget(Vector3 targetpos)
    {
        Debug.Log("start met lopen");
        ChoosewalkAnimation(true);
        agent.destination = targetpos;
        while (Vector3.Distance(targetpos, transform.position) > 1)
        {
            calculateSpeed();
            yield return null;
        }
        ChoosewalkAnimation(false);
        Debug.Log("stop met lopen");
        manager.funtionAvailable = false;
        manager.Manager();
    }

    private void ChoosewalkAnimation(bool action)
    {
        if (LoopCommando == null)
        {
            Debug.LogError("je hebt geen bewegingscommandos opgegeven. sluit de game en voeg commando booleans toe aan het script en de animator");
            StopCoroutine(routine);
        }
        else
        {
            animator.SetBool(LoopCommando, action);
        }
    }




    public void ReachSpecTarget(Vector3 targetpos)
    {
        if(routine != null)
        {
            StopCoroutine(routine);
        }
        routine = ReachSpecificTarget(targetpos);
        StartCoroutine(routine);
    }

    private IEnumerator ReachSpecificTarget(Vector3 targetpos)
    {
        isWalkingToInteract = true;
        ChoosewalkAnimation(true);
        agent.destination = targetpos;
        while (Vector3.Distance(targetpos, transform.position) > 1)
        {
            calculateSpeed();
            yield return null;
        }
        Debug.Log(" target reached");
        ChoosewalkAnimation(false);
        isWalkingToInteract = false;
    }
}
