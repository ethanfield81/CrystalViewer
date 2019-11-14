using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class BounceOffFloor : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Vector3 v3Position;
    [SerializeField]
    private Vector3 v3Velocity;
    private Vector3 SavedVelocity;
    public enum ActivityType { Paused = 0, Active };
    public ActivityType Activity;
    private ConstantForce constforce;

    private TrailPauser trailscript = null;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] allChildren = this.GetComponentsInChildren<Transform>();
        //foreach (Transform child in allChildren) print("Found child gameobject " + child.gameObject.name.ToString());

        foreach (Transform child in allChildren)
        {
            if (trailscript == null)
            {
                if (child.gameObject.GetComponent<TrailPauser>() != null)
                {
                    trailscript = child.gameObject.GetComponent<TrailPauser>();
                    //print("Found TrailPauser");
                }

            }
        }


        constforce = this.GetComponent<ConstantForce>();
        Activity = ActivityType.Active;
        rb.velocity = v3Velocity;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forcevector;
        forcevector = constforce.force;
        constforce.force = new Vector3(-0.05f * this.transform.position.x,
           forcevector.y, -0.15f * this.transform.position.z);
    }


    // OnTriggerEnter is called whenever a collision is detected between 
    // this object (which is the BouncingBall in this example) and another ojbect.
    void OnTriggerEnter(Collider other)
    {

        Vector3 v3Velocity = rb.velocity;

        //  print("Detected collision between " + this.name + " and " + other.name + "\n" +
        //     "Position" + v3Position.ToString("F4") + " and " + "Velocity" + v3Velocity.ToString("F4"));

        // Reverse the vertical (y-axis) velocity to simulate an elastic collisions
        float Vx = v3Velocity.x;
        float Vy = v3Velocity.y;
        float Vz = v3Velocity.z;
        v3Velocity = new Vector3(Vx, -Vy, Vz);
        rb.velocity = v3Velocity;


    }

    // Called by GestureManager when the user performs a Select (Tap) gesture
    void OnSelect()
    {
        // If this object was tapped on, then toggle the isKninematic flag to pause/unpause the motion.
        if (Activity == ActivityType.Paused)
        {
            Activity = ActivityType.Active;
            rb.isKinematic = false;
            rb.velocity = SavedVelocity;
            if (trailscript != null) trailscript.ResumeTrail();
        }
        else
        {
            Activity = ActivityType.Paused;
            SavedVelocity = rb.velocity;
            rb.isKinematic = true;
            if (trailscript != null) trailscript.PauseTrail();


        }

    }
}
*/