using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JointNames;

public class HandJointsDriver : MonoBehaviour
{
    [SerializeField] private ArmGraspManager armGraspManager;
    [SerializeField] private JointName jointName;
    [field: SerializeField, Range(0, 2)] public float jointSpeedMod = 1; // speed of joint relative to others [Value: 0-2]

    public float graspOpenAngle; // Initial joint angle; never changes
    public float graspClosedAngle; // Desired final joint angle (dependent on intended grasp)
    public float currentJointAngle; // Pertaining to the articulation body
    public float incAmount; // How much currentJointAngle increments every FixedUpdate frame

    private ArticulationBody articulationBody;

    private void Awake()
    {
        articulationBody = this.GetComponent<ArticulationBody>();
    }

    private void OnEnable()
    {
        ArmGraspManager.OnGraspChange += SetNewMaxJointAngle;
        ArmGraspManager.OnCloseGrasp += IncrementallyCloseGrasp;
        ArmGraspManager.OnOpenGrasp += IncrementallyOpenGrasp;
    }

    private void OnDisable()
    {
        ArmGraspManager.OnGraspChange -= SetNewMaxJointAngle;
        ArmGraspManager.OnCloseGrasp -= IncrementallyCloseGrasp;
        ArmGraspManager.OnOpenGrasp -= IncrementallyOpenGrasp;
    }

    private void Start()
    {
        articulationBody.SetDriveStiffness(ArticulationDriveAxis.X, 9000);
        articulationBody.SetDriveDamping(ArticulationDriveAxis.X, 500);
        articulationBody.SetDriveTargetVelocity(ArticulationDriveAxis.X, (1.85f * jointSpeedMod));
        articulationBody.SetDriveForceLimit(ArticulationDriveAxis.X, 50000);

        // Starts in the open position
        graspOpenAngle = (new OpenGrasp()).maxJointAngles[(int)GraspTypes.GraspNames.Open];
        currentJointAngle = 0;
        incAmount = armGraspManager.graspSpeed * jointSpeedMod * Time.fixedDeltaTime;
    }

    public void IncrementallyOpenGrasp()
    {        
        if (currentJointAngle > graspOpenAngle)
        {
            currentJointAngle -= incAmount;
            articulationBody.SetDriveTarget(ArticulationDriveAxis.X, currentJointAngle);
            armGraspManager.graspFullyOpen = armGraspManager.graspFullyOpen && false;
        } else
        {
            armGraspManager.graspFullyOpen = armGraspManager.graspFullyOpen && true;
        }
    }

    public void IncrementallyCloseGrasp()
    {
        if (currentJointAngle < graspClosedAngle)
        {
            currentJointAngle += incAmount;
            articulationBody.SetDriveTarget(ArticulationDriveAxis.X, currentJointAngle);
        }
    }

    private void SetNewMaxJointAngle()
    {
        graspClosedAngle = armGraspManager.currentGraspAngles[(int)jointName];

        articulationBody.SetDriveLimits(ArticulationDriveAxis.X, graspOpenAngle, graspClosedAngle);
        articulationBody.SetDriveTarget(ArticulationDriveAxis.X, currentJointAngle);
    }

    public void RecalculateJointIncrementAmount(float newSpeedModifier)
    {
        incAmount = armGraspManager.graspSpeed * newSpeedModifier * Time.fixedDeltaTime;
    }
}
