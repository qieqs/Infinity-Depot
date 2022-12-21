using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Character_AI : MonoBehaviour
{


    [HideInInspector]
    public Character_Manager charactermanager;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public bool funtionAvailable;

    [HideInInspector]
    public List<string> components = new List<string>();
    public string currentactivity;
    private IEnumerator routine;

    void Awake()
    {
        charactermanager = FindObjectOfType<Character_Manager>();
        components.Add("idle");
        agent = GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponentInChildren<Animator>();
        funtionAvailable = false;
        currentactivity = "";
    }

    public void addwander()
    {
        gameObject.AddComponent<Character_Wander>();
    }
    public void addActionSingle()
    {
        gameObject.AddComponent<Character_Action_Single>();
    }
    public void addActionLoop()
    {
        gameObject.AddComponent<Character_Action_Loop>();
    }
    public void addInteraction()
    {
        gameObject.AddComponent<Character_Interaction>();
    }

    private void Start()
    {
        StartCoroutine(waitbeforestarting());
    }

    private IEnumerator waitbeforestarting()
    {
        yield return new WaitForSeconds(3f);
        Manager();
    }


    void Update()
    {
        if (funtionAvailable)
        {
            if(currentactivity == "idle")
            {
                if (routine != null)
                {
                    StopCoroutine(routine);
                }
                routine = keepidle();
                StartCoroutine(routine);
            }
        }
    }

    private IEnumerator keepidle()
    {
        funtionAvailable = false;


        yield return new WaitForSeconds(Random.Range(5,10));

        funtionAvailable = false;
        Manager();
    }

    public void Manager()
    {
        currentactivity = components[Random.Range(0, components.Count)];
        funtionAvailable = true;
    }

    public void BlockManager()
    {
        currentactivity = "";
        if(routine != null)
        {
            StopCoroutine(routine);
        }
        funtionAvailable = false;
    }


  
}
