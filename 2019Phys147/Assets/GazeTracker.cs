using UnityEngine;
using UnityEngine.XR.WSA.Input;


//Use to track the gaze of a HoloLens user, or the focus of the mouse pointer when running on Unity.
//A cursor is placed at the location of the hit, or out in the distance if there is no hit detected.
//If the gaze (or mouse pointer) lands on an active GameObject, the varialble FocusedOject will point to that object.
//Otherwise, FocusedObject is set to null.

public class GazeTracker : MonoBehaviour
{
    public Transform cam;
    public Transform cursor;
    public float distance = 10f;

    public GameObject FocusedObject = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray;

        // First determine the ray (i.e. the direction) of the gaze
        // If running on Unity (WindowsEditor platform) then use mousePosition to determine the ray.
        // Other wise use the HoloLens to determine the ray.
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ray.origin = cam.position;
        }
        else
        {
            ray = new Ray(cam.position, cam.forward);
        }

        //print("ray " + ray);

        RaycastHit hit;

        //If the raycast hit an object...
        if (Physics.Raycast(ray, out hit, distance))
        {
            //print("Physics.Raycast hit " + hit.collider.gameObject.name.ToString());
            FocusedObject = hit.collider.gameObject;

            //Place the cursor at the hit location.
            cursor.position = hit.point;

            //Orient the cursor to the normal of the hit object.
            cursor.forward = hit.normal;
        }
        else
        {
            //Place the cursor at the furthest range along your gaze.
            cursor.position = ray.origin + (ray.direction * distance);
            cursor.forward = ray.direction;
            FocusedObject = null;
        }
    }
}


