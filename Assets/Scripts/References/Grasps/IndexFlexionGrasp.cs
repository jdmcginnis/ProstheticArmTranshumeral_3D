using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexFlexionGrasp : IGraspFormat
{
    public float[] maxJointAngles { get; set; } = new float[]
    {
        0, // WristFlexion
        0, // WristRotation
        30, // IndexJoint0
        80, // IndexJoint1
        85, // IndexJoint2
        0, // MiddleJoint0
        0, // MiddleJoint1
        0, // MiddleJoint2
        0, // RingJoint0
        0, // RingJoint1
        0, // RingJoint2
        0, // PinkyJoint0
        0, // PinkyJoint1
        0, // PinkyJoint2
        0, // ThumbJoint0
        0, // ThumbJoint1
        0 // ThumbJoint2
    };
}
