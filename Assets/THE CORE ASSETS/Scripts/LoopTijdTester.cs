using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LoopTijdTester : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public NavMeshAgent agent;
    public string loopCommando;
    [HideInInspector]
    public Transform origin;
    [HideInInspector]
    public Transform target;

    [HideInInspector]
    public float hSliderValue = 0.0F;
    private Vector3 previousPosition;
    [HideInInspector]
    public float curSpeed;

    public float loopsnelheid;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        WanderTarget();
    }


    void Update()
    {
        
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 800, 100), "welkom in de loop snelheid tester. Hier gaan we kijken hoe we de beweegsnelheid kunnen synchroniseren met de animatiesnelheid");
        GUI.Label(new Rect(25, 35, 500, 50), "huidige bewegingssnelheid " + loopsnelheid.ToString());
        GUI.backgroundColor = Color.green;
        hSliderValue = GUI.HorizontalSlider(new Rect(25, 55, 200, 30), hSliderValue, 0.0F, 10.0F);
        loopsnelheid = hSliderValue;

        //GUI.Label(new Rect(20, 0, 500, 50), "huidige bewegingssnelheid " + loopsnelheid.ToString());
        //GUI.Label(new Rect(20, 50, 500, 50), "onthoudt de snelheid en schrijf het opnieuw in het 'loopsnelheid' slot" + loopsnelheid.ToString());
        agent.speed = loopsnelheid;
    }

    void WanderTarget()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(target.position, out hit, 100f, NavMesh.AllAreas);
        Vector3 finalPosition = hit.position;
        Debug.Log(finalPosition);
        StartCoroutine(ReachTarget(finalPosition));
    }

    private void ChoosewalkAnimation(bool action)
    {
        if (loopCommando == null)
        {
            Debug.LogError("je hebt geen bewegingscommandos opgegeven. sluit de game en voeg commando booleans toe aan het script en de animator");
            StopAllCoroutines();
        }
        else
        {
            animator.SetBool(loopCommando, action);
        }
    }

    private IEnumerator ReachTarget(Vector3 targetpos)
    {
        ChoosewalkAnimation(true);
        agent.destination = targetpos;
        while (Vector3.Distance(targetpos, transform.position) > 1)
        {
            //calculateSpeed();
            yield return null;
        }
        Debug.Log(" target reached");
        ChoosewalkAnimation(false);
        ResetLoop();
    }

    private void ResetLoop()
    {
        this.transform.position = origin.position;
        WanderTarget();
    }

    void calculateSpeed()
    {
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;
        animator.speed = curSpeed / loopsnelheid;
    }
}
