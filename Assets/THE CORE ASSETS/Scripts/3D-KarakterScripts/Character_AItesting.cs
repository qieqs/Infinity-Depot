using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Character_AItesting : MonoBehaviour
{
    [HideInInspector]
    public Character_Manager charactermanager;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public NavMeshAgent agent;

    public bool funtionAvailable;

    //[HideInInspector]
    public List<string> components = new List<string>();
    public string currentactivity;
    [HideInInspector]
    public bool isActive;


    void Awake()
    {
        charactermanager = FindObjectOfType<Character_Manager>();
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
        if (funtionAvailable == true)
        {
            if (currentactivity == "idle" && isActive == false)
            {
                StartCoroutine(keepidle());
                isActive = true;
            }
        }
    }

    private IEnumerator keepidle()
    {
        funtionAvailable = false;
        yield return new WaitForSeconds(Random.Range(5, 10));
        isActive = false;
        Manager();
    }

    public void Manager()
    {
        currentactivity = components[Random.Range(0, components.Count)];
        funtionAvailable = true;
        //Debug.Log("choosing a new function");
    }

    public void BlockManager()
    {
        currentactivity = "";
    }



}