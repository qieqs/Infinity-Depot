using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Character_AI : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public NavMeshAgent agent;


    [HideInInspector]
    public List<string> components = new List<string>();
    public string currentactivity;
    [HideInInspector]
    public bool isActive;


    void Awake()
    {
        components.Add("idle");
        agent = GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Manager();
    }


    void Update()
    {
        if(currentactivity == "idle" && isActive == false)
        {
            StartCoroutine(keepidle());
            isActive = true;
        }
    }

    private IEnumerator keepidle()
    {
        yield return new WaitForSeconds(Random.Range(5,10));
        isActive = false;
        Manager();
    }

    public void Manager()
    {
        currentactivity = components[Random.Range(0, components.Count)];
    }

    public void BlockManager()
    {
        currentactivity = "";
    }


  
}
