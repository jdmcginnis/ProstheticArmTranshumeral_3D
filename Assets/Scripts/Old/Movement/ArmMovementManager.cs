using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovementManager : MonoBehaviour
{
    [SerializeField] private ShoulderDriver shoulderDriver;
    [SerializeField] private ElbowDriver elbowDriver;
    [SerializeField] private WristDriver wristDriver;

    private void FixedUpdate()
    {
        shoulderDriver.UpdatePose();
        elbowDriver.UpdatePose();
        wristDriver.UpdatePhysicsTransform();
    }
}
