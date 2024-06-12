using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchGrasp : IGraspFormat
{
    public float[] maxJointAngles { get; set; } = new float[]
    {
        0, // WristFlexion
        0, // WristRotation
        30, // IndexJoint0
        39, // IndexJoint1
        64, // IndexJoint2
        0, // MiddleJoint0
        0, // MiddleJoint1
        0, // MiddleJoint2
        0, // RingJoint0
        0, // RingJoint1
        0, // RingJoint2
        0, // PinkyJoint0
        0, // PinkyJoint1
        0, // PinkyJoint2
        48, // ThumbJoint0
        5, // ThumbJoint1
        20 // ThumbJoint2
    };
}
