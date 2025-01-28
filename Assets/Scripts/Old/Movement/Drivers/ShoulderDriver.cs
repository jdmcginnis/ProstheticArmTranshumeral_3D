using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderDriver : MonoBehaviour
{
    [SerializeField] private GameObject shoulderTrackerObj;
    [SerializeField] private GameObject upperArmObj;
    [SerializeField] private GameObject upperArmPhysicsObj;

    private Transform shoulderTrackerTrans;
    private Transform upperArmTrans;
    private Rigidbody upperArmPhysicsRB;

    private void Awake()
    {
        shoulderTrackerTrans = shoulderTrackerObj.GetComponent<Transform>();
        upperArmTrans = upperArmObj.GetComponent<Transform>();
        upperArmPhysicsRB = upperArmPhysicsObj.GetComponent<Rigidbody>();
    }

    // We directly set positions and have separate gameobjects containing rigidbody/collider follow (preserving physics)
    public void UpdatePose()
    {
        UpdateTransform(); // no physics
        UpdatePhysicsTransform(); // preserving physics
    }

    private void UpdateTransform()
    {
        upperArmTrans.rotation = shoulderTrackerTrans.rotation;
        upperArmTrans.position = shoulderTrackerTrans.position;
    }

    private void UpdatePhysicsTransform()
    {
        upperArmPhysicsRB.MoveRotation(upperArmTrans.rotation); // TODO: Change to AddTorque(..., ForceMode.VelocityChange);

        Vector3 posDiff = upperArmTrans.position - upperArmPhysicsRB.position;
        Vector3 velDiff = (posDiff / Time.fixedDeltaTime) - upperArmPhysicsRB.velocity;
        upperArmPhysicsRB.AddForce(velDiff, ForceMode.VelocityChange);
    }

}
