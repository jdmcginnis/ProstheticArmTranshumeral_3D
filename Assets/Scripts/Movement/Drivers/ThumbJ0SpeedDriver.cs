using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Overloads the preset speed in JointManager to account thumb movement of various grasps
public class ThumbJ0SpeedDriver : MonoBehaviour
{
    private HandJointsDriver thumbJointDriver;

    IDictionary thumbSpeedPerGrasp = new Dictionary<GraspTypes.GraspNames, float>
        {
            { GraspTypes.GraspNames.IndexFlexion, 1f },
            { GraspTypes.GraspNames.Key, 1f },
            { GraspTypes.GraspNames.Pinch, 2f },
            { GraspTypes.GraspNames.Point, 1f },
            { GraspTypes.GraspNames.Power, 0.55f },
            { GraspTypes.GraspNames.Tripod, 2f },
            { GraspTypes.GraspNames.WristExtension, 1f },
            { GraspTypes.GraspNames.WristFlexion, 1f },
            { GraspTypes.GraspNames.WristRotation, 1f },
            { GraspTypes.GraspNames.WristRotationAndPower, 0.55f }
        };

    private void Awake()
    {
        thumbJointDriver = this.GetComponent<HandJointsDriver>();
    }

    private void ChangeThumbJointSpeed(GraspTypes.GraspNames newGrasp)
    {
        float newSpeed = (float)thumbSpeedPerGrasp[newGrasp];
        thumbJointDriver.RecalculateJointIncrementAmount(newSpeed);
    }
}
