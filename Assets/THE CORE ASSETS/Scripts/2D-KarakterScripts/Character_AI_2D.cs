using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Character_AI_2D : MonoBehaviour
{
    [HideInInspector]
    public Character_Manager charactermanager;
    public GameObject target;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public NavMeshAgent agent;

    public GameObject frontcharacter;
    public GameObject backcharacter;

    [HideInInspector]
    public List<string> components = new List<string>();
    public string currentactivity;
    [HideInInspector]
    public bool isActive;
    [HideInInspector]
    public bool funtionAvailable;

    private IEnumerator routine;

    void Awake()
    {
        charactermanager = FindObjectOfType<Character_Manager>();
        components.Add("idle");
        agent = GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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

    void Start()
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
            if (currentactivity == "idle")
            {
                if (routine != null)
                {
                    StopCoroutine(routine);
                }
                routine = keepidle();
                StartCoroutine(routine);
            }
        }
        animator.gameObject.transform.LookAt(target.transform);
        animator.gameObject.transform.localEulerAngles = new Vector3(0, animator.gameObject.transform.localEulerAngles.y, 0);
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
    }

    public void BlockManager()
    {
        currentactivity = "";
        if (routine != null)
        {
            StopCoroutine(routine);
        }
        funtionAvailable = false;
    }
}
