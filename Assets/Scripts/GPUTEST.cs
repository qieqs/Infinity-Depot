using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUTEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Prints using the following format - "ATI Radeon X1600 OpenGL Engine" on MacBook Pro running Mac OS X 10.4.8
        print(SystemInfo.graphicsDeviceName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
