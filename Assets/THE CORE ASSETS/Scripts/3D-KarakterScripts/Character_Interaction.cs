using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Interaction : MonoBehaviour
{
    private bool isActive;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;
    private Character_AI manager;
    private string componentstring = "action_loop";

    public string ActieCommando;


    private bool reactionIsActive;
    private bool wanderIsActive;
    private bool go;
    // Start is called before the first frame update
    void Start()
    {
        manager = this.GetComponent<Character_AI>();
        agent = manager.agent;
        manager.components.Add(componentstring);
        animator = manager.animator;

    }

    void CheckCompatibility()
    {
        for (int i = 0; i < manager.components.Count; i++)
        {
            if (manager.components[i] == "wander")
            {
                wanderIsActive = true;
            }
            if(manager.components[i] == "reaction")
            {
                reactionIsActive = true;
            }
        }
        if(wanderIsActive && reactionIsActive)
        {
            go = true;
        }
        else
        {
            Debug.LogError("de interactie werkt alleen maar als er een wander en reactie component aanwezig is");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(go == true)
        {
            if (manager.currentactivity == componentstring && isActive == false)
            {
                
                isActive = true;
            }
        }
    }

    private void SearchATarget()
    {

    }
}
