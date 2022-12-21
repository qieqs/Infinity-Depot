using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Manager : MonoBehaviour
{
    //[HideInInspector]
    public GameObject[] characterlist;
    void Start()
    {
        characterlist = GameObject.FindGameObjectsWithTag("character");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
