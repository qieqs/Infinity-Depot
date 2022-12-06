using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Character_Wander_2D : MonoBehaviour
{
    private bool isActive;
    Animator animator;
    NavMeshAgent agent;
    Character_AI_2D manager;
    private string componentstring = "wander";

    public string LoopCommando;

    private Vector3 previousPosition;

    [HideInInspector]
    public float curSpeed;

    public float loopsnelheid;

    void Start()
    {
        manager = this.GetComponent<Character_AI_2D>();
        agent = manager.agent;
        manager.components.Add(componentstring);
        animator = manager.animator;
    }


    void Update()
    {
        if (manager.currentactivity == componentstring && isActive == false)
        {
            WanderTarget();
            isActive = true;
        }
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
        agent.speed = loopsnelheid;
        Vector3 randompoint = (Random.insideUnitSphere * 50) + transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randompoint, out hit, 100f, NavMesh.AllAreas);
        Vector3 finalPosition = hit.position;
        Debug.Log(finalPosition);
        StartCoroutine(ReachTarget(finalPosition));
    }

    private IEnumerator ReachTarget(Vector3 targetpos)
    {
        ChoosewalkAnimation(true);
        agent.destination = targetpos;
        while (Vector3.Distance(targetpos, transform.position) > 1)
        {
            calculateSpeed();
            yield return null;
        }
        Debug.Log(" target reached");
        ChoosewalkAnimation(false);
        isActive = false;
        manager.Manager();
    }

    private void ChoosewalkAnimation(bool action)
    {
        if (LoopCommando == null)
        {
            Debug.LogError("je hebt geen bewegingscommandos opgegeven. sluit de game en voeg commando booleans toe aan het script en de animator");
            StopAllCoroutines();
        }
        else
        {
            animator.SetBool(LoopCommando, action);
        }
    }
}
