using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character_Action_Single : MonoBehaviour
{
    private bool isActive;
    private bool animrunning;
    private Animator animator;
    private NavMeshAgent agent;
    private Character_AI manager;
    private string componentstring = "action_single";

    public string ActieCommando;


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
            StartCoroutine(SnigularAction());
            manager.funtionAvailable = false;
        }
    }

    private IEnumerator SnigularAction()
    {
        
        animator.SetBool(ActieCommando, true);
        animrunning = true;
        while (animrunning)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
            {
                animrunning = false;
            }
            yield return null;
        }
        isActive = false;
        animator.SetBool(ActieCommando, false);
        manager.funtionAvailable = false;
        manager.Manager();
    }
}
