using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Character_Action_Loop : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private Character_AI manager;
    private string componentstring = "action_loop";

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
            StartCoroutine(DoTheAction());
            manager.funtionAvailable = false;
        }
    }

    private IEnumerator DoTheAction()
    {
        animator.SetBool(ActieCommando, true);
        yield return new WaitForSeconds(Random.Range(5,10));
        animator.SetBool(ActieCommando, false);
        manager.funtionAvailable = false;
        manager.Manager();
    }
}
