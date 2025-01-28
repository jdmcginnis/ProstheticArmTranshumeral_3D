using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandGraspManager : MonoBehaviour
{
    #region Actions
    public static event Action<GraspProperties> OnGraspChange;
    public static event Action OnOpeningGrasp;
    public static event Action OnClosingGrasp;
    public static event Action<float[]> SetNewJointAngles;
    #endregion

    #region GraspStates
    private static IndexFlexionGrasp IndexFlexionGraspState = new IndexFlexionGrasp();
    private static KeyGrasp KeyGraspState = new KeyGrasp();
    private static PinchGrasp PinchGraspState = new PinchGrasp();
    private static PointGrasp PointGraspState = new PointGrasp();
    private static PowerGrasp PowerGraspState = new PowerGrasp();
    private static TripodGrasp TripodGraspState = new TripodGrasp();
    private static WristExtensionGrasp WristExtensionGraspState = new WristExtensionGrasp();
    private static WristFlexionGrasp WristFlexionGraspState = new WristFlexionGrasp();
    private static WristRotationGrasp WristRotationGraspState = new WristRotationGrasp();
    private static WristRotationAndPowerGrasp WristRotationAndPowerGraspState = new WristRotationAndPowerGrasp();
    private static OpenGrasp OpenGraspState = new OpenGrasp();
    #endregion

    #region GraspLookupTable
    private IDictionary<GraspTypes, GraspProperties> graspStatesDict = new Dictionary<GraspTypes, GraspProperties>()
    {
        {GraspTypes.IndexFlexion, IndexFlexionGraspState},
        {GraspTypes.Key, KeyGraspState},
        {GraspTypes.Pinch, PinchGraspState},
        {GraspTypes.Point, PointGraspState},
        {GraspTypes.Power, PowerGraspState},
        {GraspTypes.Tripod, TripodGraspState},
        {GraspTypes.WristExtension, WristExtensionGraspState},
        {GraspTypes.WristFlexion, WristFlexionGraspState},
        {GraspTypes.WristRotation, WristRotationGraspState},
        {GraspTypes.WristRotationAndPower, WristRotationAndPowerGraspState},
        {GraspTypes.Open, OpenGraspState}
    };
    #endregion

    public GraspTypes currentGraspState { get; private set; } // the hand's current grasp
    public bool isHandFullyOpen; // all joints vote and set this value

    private void Start()
    {
        currentGraspState = GraspTypes.Rest;
        isHandFullyOpen = true;
    }

    // TODO: Directly sets joint angles; an alternative to gradually incrementing/decrementing
    public void SetJointAngles()
    {
        float[] newJointAngles = { 1, 1, 1 }; // TODO: Change to get array of values from InputManager
        SetNewJointAngles?.Invoke(newJointAngles);
    }

    public void OpenGraspByOneStep()
    {
        isHandFullyOpen = true; // reset value to true; joints vote and flip bit if false
        OnOpeningGrasp?.Invoke();
    }

    public void CloseGraspByOneStep()
    {
        isHandFullyOpen = false;
        OnClosingGrasp?.Invoke();
    }

    public void ChangeGrasp(GraspTypes currentGraspTarget)
    {
        if (Settings.fullyOpenToChangeGrasp && !isHandFullyOpen)
            return;

        float[] maxJointAngles = graspStatesDict[currentGraspTarget].maxJointAngles;
        float[] minJointAngles = graspStatesDict[GraspTypes.Open].maxJointAngles;
        float[] deltaJointAngles = new float[maxJointAngles.Length];
        float maxAngleValue = 0;

        for (int i = 0; i < maxJointAngles.Length; i++)
        {
            float angleDifference = maxJointAngles[i] - minJointAngles[i];
            deltaJointAngles[i] = angleDifference;

            if (maxAngleValue < angleDifference)
                maxAngleValue = angleDifference;
        }

        Debug.Log("Max Angle Value: " + maxAngleValue);
        Debug.Log("TODO: Make joints move at the same speed!");

        Debug.Log("We have changed over from grasp: " + currentGraspState.ToString() + " to " + currentGraspTarget.ToString());
        currentGraspState = currentGraspTarget;
        OnGraspChange?.Invoke(graspStatesDict[currentGraspState]);
    }
}
