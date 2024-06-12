using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGrasp : IGraspFormat
{
    public float[] maxJointAngles { get; set; } = new float[]
    {
        0, // WristFlexion
        0, // WristRotation
        0, // IndexJoint0
        0, // IndexJoint1
        0, // IndexJoint2
        85, // MiddleJoint0
        82, // MiddleJoint1
        78, // MiddleJoint2
        85, // RingJoint0
        82, // RingJoint1
        78, // RingJoint2
        85, // PinkyJoint0
        82, // PinkyJoint1
        78, // PinkyJoint2
        45, // ThumbJoint0
        30, // ThumbJoint1
        32 // ThumbJoint2
    };
}
