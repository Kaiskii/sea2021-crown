using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    private bool closeCamera = true;
        
    [SerializeField]
    private CinemachineVirtualCamera Camera1;

    [SerializeField]
    private CinemachineVirtualCamera Camera2;

    private void switchPriority()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            if (closeCamera) {
                Camera1.Priority = 0;
                Camera2.Priority = 1;
            } else {
                Camera1.Priority = 1;
                Camera2.Priority = 0;
            }
            closeCamera = !closeCamera;
        }
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switchPriority();
    }
}
