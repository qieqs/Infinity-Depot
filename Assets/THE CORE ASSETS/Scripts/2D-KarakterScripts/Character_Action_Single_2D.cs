using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character_Action_Single_2D : MonoBehaviour
{
    private bool isActive;
    private bool animrunning;
    private Animator animator;
    private NavMeshAgent agent;
    private Character_AI_2D manager;
    private string componentstring = "action_single";

    public string ActieCommando;

    void Start()
    {
        manager = this.GetComponent<Character_AI_2D>();
        agent = manager.agent;
        manager.components.Add(componentstring);
        animator = manager.animator;
    }


    void Update()
    {
        if (manager.funtionAvailable && manager.currentactivity == componentstring)
        {
            StartCoroutine(SingularAction());
            manager.funtionAvailable = false;
        }
    }

    private IEnumerator SingularAction()
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
