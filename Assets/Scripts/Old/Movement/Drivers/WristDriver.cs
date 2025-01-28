using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristDriver : MonoBehaviour
{
    [SerializeField] private GameObject wristObj;
    [SerializeField] private GameObject wristPhysicsObj;

    private Transform wristTrans;
    private Rigidbody wristPhysicsRB;

    private void Awake()
    {
        wristTrans = wristObj.GetComponent<Transform>();
        wristPhysicsRB = wristPhysicsObj.GetComponent<Rigidbody>();
    }

    // Position of wrist has already been updated; we just follow it with our rigidbody
    public void UpdatePhysicsTransform()
    {
        wristPhysicsRB.MoveRotation(wristTrans.rotation); // TODO: Change to AddTorque(..., ForceMode.VelocityChange);

        Vector3 posDiff = wristTrans.position - wristPhysicsRB.position;
        Vector3 velDiff = (posDiff / Time.fixedDeltaTime) - wristPhysicsRB.velocity;
        wristPhysicsRB.AddForce(velDiff, ForceMode.VelocityChange);

    }

}
