using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElbowDriver : MonoBehaviour
{
    [SerializeField] private GameObject elbowTrackerObj;
    [SerializeField] private GameObject lowerArmObj;
    [SerializeField] private GameObject lowerArmPhysicsObj;

    private Transform elbowTrackerTrans;
    private Transform lowerArmTrans;
    private Rigidbody lowerArmPhysicsRB;

    private void Awake()
    {
        elbowTrackerTrans = elbowTrackerObj.GetComponent<Transform>();
        lowerArmTrans = lowerArmObj.GetComponent<Transform>();
        lowerArmPhysicsRB = lowerArmPhysicsObj.GetComponent<Rigidbody>();
    }

    // We directly set positions and have separate gameobjects containing rigidbody/collider follow (preserving physics)
    public void UpdatePose()
    {
        UpdateTransform(); // no physics
        UpdatePhysicsTransform(); // preserving physics
    }

    private void UpdateTransform()
    {
        lowerArmTrans.rotation = elbowTrackerTrans.rotation;
    }

    private void UpdatePhysicsTransform()
    {
        lowerArmPhysicsRB.MoveRotation(lowerArmTrans.rotation); // TODO: Change to AddTorque(..., ForceMode.VelocityChange);

        Vector3 posDiff = lowerArmTrans.position - lowerArmPhysicsRB.position;
        Vector3 velDiff = (posDiff / Time.fixedDeltaTime) - lowerArmPhysicsRB.velocity;
        lowerArmPhysicsRB.AddForce(velDiff, ForceMode.VelocityChange);
    }
}
