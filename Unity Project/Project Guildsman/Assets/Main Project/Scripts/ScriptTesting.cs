using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTesting : MonoBehaviour
{
    DebugControls controls;
    private void Awake()
    {
        controls = new DebugControls();
        controls.Test.TestInput.performed += ctx => myTest();
    }
    private void OnEnable()
    {
        controls.Test.Enable();
    }
    private void OnDisable()
    {
        controls.Test.Disable();
    }

    void myTest()
    {
        
    }
}
