using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Character_AI_2D : MonoBehaviour
{
    public GameObject target;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
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
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        Manager();
    }

    void Update()
    {
        if (currentactivity == "idle" && isActive == false)
        {
            StartCoroutine(keepidle());
            isActive = true;
        }
        animator.gameObject.transform.LookAt(target.transform);
        animator.gameObject.transform.localEulerAngles = new Vector3(0, animator.gameObject.transform.localEulerAngles.y, 0);
    }

    private IEnumerator keepidle()
    {
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
    }
}
