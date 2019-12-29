using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlanks : MonoBehaviour
{
    public Component[] hingeJoints;

    private void Start()
    {
        hingeJoints = GetComponentsInChildren<HingeJoint>();
    }

    private void OnCollisionExit(Collision collision)
    {
        foreach(HingeJoint joint in hingeJoints)
        {
            if(joint != null)
                joint.breakForce = 2f;
        }
    }
}
