using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArmGraspManager : MonoBehaviour
{
    [field: SerializeField] public float graspSpeed { get; private set; } = 1; // how fast the hand opens/closes

    public GraspTypes.GraspNames currentGrasp { get; private set; }
    public float[] currentGraspAngles { get; private set; }

    public bool graspFullyOpen; // all joints vote and set this value

    #region GraspStates
    private IndexFlexionGrasp IndexFlexionGraspState = new IndexFlexionGrasp();
    private KeyGrasp KeyGraspState = new KeyGrasp();
    private PinchGrasp PinchGraspState = new PinchGrasp();
    private PointGrasp PointGraspState = new PointGrasp();
    private PowerGrasp PowerGraspState = new PowerGrasp();
    private TripodGrasp TripodGraspState = new TripodGrasp();
    private WristExtensionGrasp WristExtensionGraspState = new WristExtensionGrasp();
    private WristFlexionGrasp WristFlexionGraspState = new WristFlexionGrasp();
    private WristRotationGrasp WristRotationGraspState = new WristRotationGrasp();
    private WristRotationAndPowerGrasp WristRotationAndPowerGraspState = new WristRotationAndPowerGrasp();
    private OpenGrasp OpenGraspState = new OpenGrasp();
    #endregion

    // Linking enum to object
    private IDictionary<GraspTypes.GraspNames, IGraspFormat> graspStates;

    public static event Action OnGraspChange;
    public static event Action OnCloseGrasp;
    public static event Action OnOpenGrasp;

    public static event Action OnBeginClosingGrasp; // when we first begin closing grasp
    public static event Action OnFullyOpenGrasp;
    
    private void Awake()
    {
        graspStates = new Dictionary<GraspTypes.GraspNames, IGraspFormat>()
            {
                {GraspTypes.GraspNames.IndexFlexion, IndexFlexionGraspState},
                {GraspTypes.GraspNames.Key, KeyGraspState},
                {GraspTypes.GraspNames.Pinch, PinchGraspState},
                {GraspTypes.GraspNames.Point, PointGraspState},
                {GraspTypes.GraspNames.Power, PowerGraspState},
                {GraspTypes.GraspNames.Tripod, TripodGraspState},
                {GraspTypes.GraspNames.WristExtension, WristExtensionGraspState},
                {GraspTypes.GraspNames.WristFlexion, WristFlexionGraspState},
                {GraspTypes.GraspNames.WristRotation, WristRotationGraspState},
                {GraspTypes.GraspNames.WristRotationAndPower, WristRotationAndPowerGraspState},
                {GraspTypes.GraspNames.Open, OpenGraspState}
            };
    }

    private void Start()
    {
        // Initially in resting state
        ChangeGrasp(GraspTypes.GraspNames.Open);
        graspFullyOpen = true;
    }

    private void OnEnable()
    {
        InputManager.OnNewGraspSelected += ChangeGrasp;
    }

    private void OnDisable()
    {
        InputManager.OnNewGraspSelected -= ChangeGrasp;
    }


    private void ChangeGrasp(GraspTypes.GraspNames newGrasp)
    {
        Debug.Log("We have changed over from grasp: " + currentGrasp.ToString() + " to " + newGrasp.ToString());
        currentGrasp = newGrasp;
        currentGraspAngles = graspStates[newGrasp].maxJointAngles;
        OnGraspChange?.Invoke();
    }

    public void CloseGrasp()
    {
        OnCloseGrasp?.Invoke();

        if (graspFullyOpen)
        {
            graspFullyOpen = false;
            OnBeginClosingGrasp?.Invoke();
        }

    }

    public void OpenGrasp()
    {
        graspFullyOpen = true; // reset value to true; joints vote and flip bit if false
        OnOpenGrasp?.Invoke();

        if (graspFullyOpen)
        {
            OnFullyOpenGrasp?.Invoke();
        }

    }
}
