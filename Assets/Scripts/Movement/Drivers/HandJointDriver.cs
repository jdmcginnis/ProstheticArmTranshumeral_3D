using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNetDistributions = MathNet.Numerics.Distributions;
using static JointTypes;

public class HandJointDriver : MonoBehaviour
{
    [SerializeField] private HandGraspManager handGraspManager;
    [SerializeField] private JointNames jointName;
    [SerializeField] private JointAxis jointAxis;
    [SerializeField] private DirectionOfJointClosing jointCloseRotDirection;

    MathNetDistributions.Normal normalDist = new MathNetDistributions.Normal(0f, Settings.GaussianNoise.stdDev);

    public float minJointAngle; // deg
    public float targetAngle; // deg (true intended joint angle w/o noise)
    public float noisyTargetAngle; // deg (with the addition of noise defined in Settings)
    public float maxJointAngle; // deg
    private float jointSpeed; // deg/s
    public float jointIncAmt; // deg increment per fixed timestep
    public bool canJointMove;

    public Vector3 rotateFromEuler;
    public Vector3 rotateToEuler;

    private void OnEnable()
    {

        HandGraspManager.OnGraspChange += AcquireNewJointPropertiesFromGrasp;
        HandGraspManager.OnOpeningGrasp += HandleOpeningIncrement;
        HandGraspManager.OnClosingGrasp += HandleClosingIncrement;
        HandGraspManager.SetNewJointAngles += MoveJointToAngle;
    }

    private void OnDisable()
    {
        HandGraspManager.OnGraspChange -= AcquireNewJointPropertiesFromGrasp;
        HandGraspManager.OnOpeningGrasp -= HandleOpeningIncrement;
        HandGraspManager.OnClosingGrasp -= HandleClosingIncrement;
        HandGraspManager.SetNewJointAngles -= MoveJointToAngle;
    }

    private void Start()
    {
        // Rest joint angles never change
        minJointAngle = (new OpenGrasp()).maxJointAngles[(int)jointName];
        targetAngle = minJointAngle;

        // Reset localRotation of gameobjects in case they have been moved by the player
        Vector3 initialRot = this.transform.localRotation.eulerAngles;
        initialRot[(int)jointAxis] = minJointAngle;
        this.transform.localRotation = Quaternion.Euler(initialRot);
    }

    // Absolute value of joint angle decreases
    private void HandleOpeningIncrement()
    {
        bool canJointOpenMore = CheckIfJointCanOpenMore();
        bool isNoiseEnabled = Settings.isNoiseEnabled;

        // Incrementing joint angle if it can move more
        if (canJointOpenMore)
        {
            targetAngle -= jointIncAmt;

            // Prevents overshooting the joint limit
            if (jointCloseRotDirection == DirectionOfJointClosing.negative)
            {
                if (targetAngle > minJointAngle)
                    targetAngle = minJointAngle;
            } else
            {
                if (targetAngle < minJointAngle)
                    targetAngle = minJointAngle;
            }
        }

        // If noise enabled, append some to targetAngle, even if joint can no longer move
        if (isNoiseEnabled)
        {
            float noiseVal = (float)normalDist.Sample();

            // Note: If using noise w/non-symmetric distribution, fix to be either adding or subtracting
            noisyTargetAngle = targetAngle - noiseVal;
        }

        RotateJoint(isNoiseEnabled);
    }

    // Absolute value of joint angle increases
    private void HandleClosingIncrement()
    {
        bool canJointCloseMore = CheckIfJointCanCloseMore();
        bool isNoiseEnabled = Settings.isNoiseEnabled;

        // Incrementing joint angle if it can move more
        if (canJointCloseMore)
        {
            targetAngle += jointIncAmt;

            // Prevents overshooting the joint limit
            if (jointCloseRotDirection == DirectionOfJointClosing.negative)
            {
                if (targetAngle < maxJointAngle)
                    targetAngle = maxJointAngle;
            } else
            {
                if (targetAngle > maxJointAngle)
                    targetAngle = maxJointAngle;
            }
        }

        // If noise enabled, append some to targetAngle, even if joint can no longer move
        if (isNoiseEnabled)
        {
            float noiseVal = (float)normalDist.Sample();

            // Note: If using noise w/non-symmetric distribution, fix to be either adding or subtracting
            noisyTargetAngle = targetAngle + noiseVal;
        }

        RotateJoint(isNoiseEnabled);
    }

    private bool CheckIfJointCanOpenMore()
    {
        // Opening
        canJointMove = true;

        // Conditions where there is no movement of joint allowed
        if (jointCloseRotDirection == DirectionOfJointClosing.negative)
        {
            if (targetAngle >= minJointAngle)
            {
                canJointMove = false;
            }
        } else
        {
            if (targetAngle <= minJointAngle)
            {
                canJointMove = false;
            }
        }

        // Indicate if this joint is fully open
        handGraspManager.isHandFullyOpen = handGraspManager.isHandFullyOpen && !canJointMove;

        return canJointMove;
    }

    private bool CheckIfJointCanCloseMore()
    {
        canJointMove = true;

        // Conditions where there is no movement of joint allowed
        if (jointCloseRotDirection == DirectionOfJointClosing.negative)
        {
            if (targetAngle <= maxJointAngle)
                canJointMove = false;
        } else
        {
            if (targetAngle >= maxJointAngle)
                canJointMove = false;
        }

        return canJointMove;
    }

    // Ignores joint limits
    private void MoveJointToAngle(float[] newJointAngles)
    {
        targetAngle = minJointAngle + newJointAngles[(int)jointName];
        RotateJoint(false);
    }

    // Rotates the joint to the desired angle
    private void RotateJoint(bool isNoiseEnabled)
    {
        // Too apathetic to check how this works, but it does
        float NormalizeAngle(float angle)
        {
            return (angle > 180) ? angle - 360 : angle;
        }

        rotateFromEuler = transform.localEulerAngles;
        rotateToEuler = rotateFromEuler;


        // my edition here
        if (isNoiseEnabled)
            rotateToEuler[(int)jointAxis] = noisyTargetAngle;
        else
            rotateToEuler[(int)jointAxis] = targetAngle;



        transform.localEulerAngles = rotateToEuler;

        // Normalize after setting
        rotateToEuler[(int)jointAxis] = NormalizeAngle(transform.localEulerAngles[(int)jointAxis]);
        transform.localEulerAngles = rotateToEuler;



        //Quaternion rotateFrom = this.transform.localRotation;
        //rotateFromEuler = rotateFrom.eulerAngles;

        //if (jointCloseRotDirection == DirectionOfJointClosing.negative)
        //{
        //    rotateFromEuler.x = (rotateFromEuler.x - 360) % 360;
        //    rotateFromEuler.y = (rotateFromEuler.y - 360) % 360;
        //    rotateFromEuler.z = (rotateFromEuler.z - 360) % 360;
        //}

        //rotateToEuler = rotateFromEuler;

        //if (isNoiseEnabled)
        //    rotateToEuler[(int)jointAxis] = noisyTargetAngle;
        //else
        //    rotateToEuler[(int)jointAxis] = targetAngle;

        //Quaternion rotateTo = Quaternion.Euler(rotateToEuler);

        //this.transform.localRotation = rotateTo;

        // rotateFrom.SetFromToRotation(rotateFromEuler, rotateToEuler);




        //// BUG: Wraps around when it hits 360 degrees

        //Quaternion rotateFrom = this.transform.localRotation;

        ////if (rotateFrom.w < 0f)
        ////{
        ////    // The rotation is greater than 180 degrees (long way around the sphere). Convert to shortest path.
        ////    rotateFrom.x = -rotateFrom.x;
        ////    rotateFrom.y = -rotateFrom.y;
        ////    rotateFrom.z = -rotateFrom.z;
        ////    rotateFrom.w = -rotateFrom.w;

        ////}

        //// Vector3 rotateFromEuler = rotateFrom.eulerAngles;
        //rotateFromEuler = rotateFrom.eulerAngles;

        ////rotateFromEuler.x = rotateFromEuler.x % 360;
        ////rotateFromEuler.y = rotateFromEuler.y % 360;
        ////rotateFromEuler.z = rotateFromEuler.z % 360;

        //rotateToEuler = rotateFromEuler;

        //if (isNoiseEnabled)
        //    rotateToEuler[(int)jointAxis] = noisyTargetAngle;
        //else
        //    rotateToEuler[(int)jointAxis] = targetAngle;

        //rotateToEuler.x = rotateToEuler.x % 360;
        //rotateToEuler.y = rotateToEuler.y % 360;
        //rotateToEuler.z = rotateToEuler.z % 360;

        //Quaternion rotateTo = Quaternion.Euler(rotateToEuler);

        ////if (rotateTo.w < 0f)
        ////{
        ////    // The rotation is greater than 180 degrees (long way around the sphere). Convert to shortest path.
        ////    rotateTo.x = -rotateTo.x;
        ////    rotateTo.y = -rotateTo.y;
        ////    rotateTo.z = -rotateTo.z;
        ////    rotateTo.w = -rotateTo.w;

        ////}

        //this.transform.localRotation = rotateTo;

    }

    private void AcquireNewJointPropertiesFromGrasp(GraspProperties newGraspObj)
    {
        // Grasp objects define angles using 0 degrees as the reference point
        // maxJointAngle = newGraspObj.maxJointAngles[(int)jointName];
        maxJointAngle = minJointAngle + newGraspObj.maxJointAngles[(int)jointName];
        jointSpeed = newGraspObj.jointSpeed;
        jointIncAmt = jointSpeed * Time.fixedDeltaTime;

        // so all user-specified joint angles are positive (needed due to local rotational axes)
        if (jointCloseRotDirection == DirectionOfJointClosing.negative)
        {
            maxJointAngle = -maxJointAngle;
            jointIncAmt = -jointIncAmt;
        }
    }



}