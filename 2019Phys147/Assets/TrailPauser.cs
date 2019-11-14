using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class used to pause and resume a trail which uses the TrailRenderer component.
// This script keeps track of the amount of time the trail has been paused,
// and then adjusts the trail.time when the trail is resumed.


public class TrailPauser : MonoBehaviour
{
    TrailRenderer trail;

    float trailTime;
    float pauseTime;
    float resumeTime;



    // Start is called before the first frame update
    void Start()
    {
        trail = this.GetComponent<TrailRenderer>();
        trailTime = trail.time;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PauseTrail()
    {
        pauseTime = Time.time;
        trail.time = Mathf.Infinity;
    }

    public void ResumeTrail()
    {
        resumeTime = Time.time;
        trail.time = (resumeTime - pauseTime) + trailTime;
        Invoke("SetTrailTime", trailTime);
    }

    void SetTrailTime()
    {
        trail.time = trailTime;
    }
}
