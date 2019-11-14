using UnityEngine;
using UnityEngine.XR.WSA.Input;


// Used to manage the gesture feature of the HoloLens (or, if running on Unity, use the mouse inputs as
// a substitute for the HoloLens gestures.
// A GazeTracker and MouseDetector are needed in order to use this class.
public class GestureManager : MonoBehaviour
{
    public static GestureManager Instance { get; private set; }

    public GazeTracker gazeTracker;
    public MouseDetector mouseDetector;

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }
    public GameObject PreviousFocusedObject { get; private set; }

    GestureRecognizer recognizer;

    // Use this for initialization
    void Start()
    {
        Instance = this;

        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();

        // Resigister a SendMessageUpwards with the gesture recognizer. 
        // This will be called whenever a Tapped gesture is detected by the HoloLens
        recognizer.Tapped += (args) =>
        {
            // Send an OnSelect message to the focused object and its ancestors.
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
            }
        };
        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        FocusedObject = gazeTracker.FocusedObject;

        if (Application.platform == RuntimePlatform.WindowsEditor) // Do this is running on Unity
        {
            // Send an OnSelect message to the focused object and its ancestors if a mouse right click was detected.
            if (FocusedObject != null && mouseDetector.RightClickDetected == true)
            {
                FocusedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
            }
           mouseDetector.RightClickDetected = false;
        }
        else // Do this if running on the HoloLens
        {

            // If the focused object changed this frame,
            // start detecting fresh gestures again.
            if (FocusedObject != PreviousFocusedObject)
            {
                // If in HoloLens Message is sent to FocusedObject through the registration of GestureManagers.Trapped 
                recognizer.CancelGestures();
                recognizer.StartCapturingGestures();
            }
        }
    }
}