using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Character_Interaction : MonoBehaviour
{
    [HideInInspector]
    public Character_Manager charactermanager;
    [HideInInspector]
    public Character_Wander characterWander;

    private Animator animator;
    private NavMeshAgent agent;
    private Character_AI manager;
    private string componentstring = "interaction";


    public string ActieCommando;
    private IEnumerator routine;
    private Character_AI targetAI;
    private Character_AI_2D TargetAI2D;


    private bool wanderIsActive;
    private bool AI2D;
    private bool AI3D;
    void Start()
    {
        manager = this.GetComponent<Character_AI>();
        agent = manager.agent;
        manager.components.Add(componentstring);
        animator = manager.animator;
        charactermanager = manager.charactermanager;
        checkForWalking();
    }

    void checkForWalking()
    {
        characterWander = this.GetComponent<Character_Wander>();
        if (characterWander != null)
        {
            wanderIsActive = true;
        }
        else
        {
            Debug.LogError("het interactie script werkt alleen maar als er een wander en reactie component aanwezig is");
        }
    }


    void Update()
    {
        if(wanderIsActive == true)
        {
            if (manager.funtionAvailable && manager.currentactivity == componentstring)
            {
                SearchATarget();
            }
            else if(!manager.funtionAvailable && manager.currentactivity == "")
            {

            }
        }
    }

    void stoproutine()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }
        agent.SetDestination(gameObject.transform.position);
        animator.SetBool(ActieCommando, false);
    }

    private void SearchATarget()
    {
        manager.funtionAvailable = false;
        float mindist = Mathf.Infinity;
        GameObject closestobject = null;
        for (int i = 0; i < charactermanager.characterlist.Length; i++)
        {
            float distance = Vector3.Distance(this.transform.position, charactermanager.characterlist[i].transform.position);
            if (distance < mindist && distance != 0)
            {
                closestobject = charactermanager.characterlist[i];
                mindist = distance;
            }
        }
        Debug.Log(mindist);
        if (mindist < 30)
        {
            CheckCompatibility(closestobject);
        }
        else
        {
            manager.funtionAvailable = false;
            manager.Manager();
        }
    }

    void CheckCompatibility(GameObject target)
    {
        targetAI = target.GetComponent<Character_AI>();
        TargetAI2D= target.GetComponent<Character_AI_2D>();

        if (targetAI != null && targetAI.currentactivity != ActieCommando)
        {
            AI3D = true;
            targetAI.BlockManager();
            //het karakter heeft een AI
            StartCoroutine(StartInteraction(target));
        }
        else if (TargetAI2D != null && TargetAI2D.currentactivity != ActieCommando)
        {
            AI2D = true; 
            TargetAI2D.BlockManager();
            //het karakter heeft een AI
            StartCoroutine(StartInteraction(target));
        }
        else
        {
            //het karakter heeft geen AI
            finishinteraction();
        }
    }


    private IEnumerator StartInteraction(GameObject target)
    {
        Vector3 randompoint = target.transform.position + target.transform.forward * 3;
        NavMeshHit hit;
        NavMesh.SamplePosition(randompoint, out hit, 1f, NavMesh.AllAreas);
        Vector3 finalPosition = hit.position;
        characterWander.ReachSpecTarget(finalPosition);

        while (characterWander.isWalkingToInteract)
        {
            yield return null;
        }

        Vector3 _direction = (target.transform.position - transform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);
        Quaternion currentRotation = transform.rotation;

        float timeElapsed = 0;
        float lerpduration = 3;
        while (timeElapsed < lerpduration)
        {
            transform.rotation = Quaternion.Slerp(currentRotation, _lookRotation, timeElapsed / lerpduration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        animator.SetBool(ActieCommando, true);
        yield return new WaitForSeconds(Random.Range(9,10));


        animator.SetBool(ActieCommando, false);

        if (AI3D)
        {
            targetAI.Manager();
        }
        if (AI2D)
        {
            TargetAI2D.Manager();
        }

        finishinteraction();
    }

    void finishinteraction()
    {
        AI3D = false;
        AI2D = false;
        manager.funtionAvailable = false;
        manager.Manager();
    }


}
