using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doStir : MonoBehaviour
{

    
    private CameraMgr mCamera;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mCamera = Camera.main.GetComponent<CameraMgr>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            animator.SetTrigger("doStir");
            mCamera.ShakeCamera(0.01f, 5f);
        }
    }
}
