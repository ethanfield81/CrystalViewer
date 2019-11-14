using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to detect Mouse clicks when running an application on Unity.
// Helpful when trying to debug Unity code designed to use the gestures feature of the HoloLens

// This version sets RightCLickDetected to true whenever a right click is detected. 
// RightClickDtected will remain true until it is set back to false 
// (presumably from another bit of code which is is waiting to detect a mouse click.

public class MouseDetector : MonoBehaviour
{
    public bool RightClickDetected = false;
    public bool LeftClickDetected = false;
    public bool MiddleClickDetected = false;

    private void Start()
    {
        ////Note: application.platform can be used to detect which platform is running this code.
        ////  As an example, when running on Unity, the platform is equal to runtimeplatform.windowseditor.
        ////  I do not know what the value of 
        //print("Application.platform = " + Application.platform);
        //print("RuntimePlatform.windowseditor = " + RuntimePlatform.WindowsEditor);
        //// Then
        //if (Application.platform == RuntimePlatform.WindowsEditor)
        //{
        //    print("Code is running on a WindowsEditor platform.");
        //}
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftClickDetected = true;
            //print("Inout.GetMouseButtonDown(0) detected in MouseDetector");
            //print("Input.mousePosition " + Input.mousePosition);
        }
        if (Input.GetMouseButtonDown(1))
        {
            RightClickDetected = true;
            //Debug.Log("Pressed secondary button.");
        }

        if (Input.GetMouseButtonDown(2))
        {
            MiddleClickDetected = true;
            //Debug.Log("Pressed middle click.");

        }
    }
}