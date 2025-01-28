using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JointTypes;

public class WristJointDriver : MonoBehaviour
{
    //[SerializeField] private ArmGraspManager armGraspManager;
    //[SerializeField] private JointName jointName;
    //[field: SerializeField, Range(0, 2)] public float jointSpeedMod = 1; // speed of joint relative to others [Value: 0-2]

    //public float graspOpenAngle; // Initial joint angle; never changes
    //public float graspClosedAngle; // Desired final joint angle (dependent on intended grasp)
    //public float currentJointAngle; // Pertaining to the articulation body
    //public float incAmount; // How much currentJointAngle increments every FixedUpdate frame
    //public bool negativeAngle = false;

    //private ArticulationBody articulationBody;

    //private void Awake()
    //{
    //    articulationBody = this.GetComponent<ArticulationBody>();
    //}

    //private void OnEnable()
    //{
    //    ArmGraspManager.OnGraspChange += SetNewMaxJointAngle;
    //    ArmGraspManager.OnCloseGrasp += IncrementallyCloseGrasp;
    //    ArmGraspManager.OnOpenGrasp += IncrementallyOpenGrasp;
    //}

    //private void OnDisable()
    //{
    //    ArmGraspManager.OnGraspChange -= SetNewMaxJointAngle;
    //    ArmGraspManager.OnCloseGrasp -= IncrementallyCloseGrasp;
    //    ArmGraspManager.OnOpenGrasp -= IncrementallyOpenGrasp;
    //}

    //private void Start()
    //{
    //    articulationBody.SetDriveStiffness(ArticulationDriveAxis.X, 9000);
    //    articulationBody.SetDriveDamping(ArticulationDriveAxis.X, 500);
    //    articulationBody.SetDriveTargetVelocity(ArticulationDriveAxis.X, (1.85f * jointSpeedMod));
    //    articulationBody.SetDriveForceLimit(ArticulationDriveAxis.X, 50000);

    //    // Starts in the open position
    //    graspOpenAngle = (new OpenGrasp()).maxJointAngles[(int)GraspTypes.GraspNames.Open];
    //    currentJointAngle = 0;
    //    incAmount = armGraspManager.graspSpeed * jointSpeedMod * Time.fixedDeltaTime;
    //}

    //public void IncrementallyOpenGrasp()
    //{
    //    if (negativeAngle)
    //    {
    //        if (currentJointAngle < graspOpenAngle)
    //        {
    //            currentJointAngle += incAmount;
    //            articulationBody.SetDriveTarget(ArticulationDriveAxis.X, currentJointAngle);
    //            armGraspManager.graspFullyOpen = armGraspManager.graspFullyOpen && false;
    //        } else
    //        {
    //            armGraspManager.graspFullyOpen = armGraspManager.graspFullyOpen && true;
    //        }
    //    } else
    //    {
    //        if (currentJointAngle > graspOpenAngle)
    //        {
    //            currentJointAngle -= incAmount;
    //            articulationBody.SetDriveTarget(ArticulationDriveAxis.X, currentJointAngle);
    //            armGraspManager.graspFullyOpen = armGraspManager.graspFullyOpen && false;
    //        } else
    //        {
    //            armGraspManager.graspFullyOpen = armGraspManager.graspFullyOpen && true;
    //        }
    //    }
    //}

    //public void IncrementallyCloseGrasp()
    //{
    //    if (negativeAngle)
    //    {
    //        if (currentJointAngle > graspClosedAngle)
    //        {
    //            currentJointAngle -= incAmount;
    //            articulationBody.SetDriveTarget(ArticulationDriveAxis.X, currentJointAngle);
    //        }
    //    } else
    //    {
    //        if (currentJointAngle < graspClosedAngle)
    //        {
    //            currentJointAngle += incAmount;
    //            articulationBody.SetDriveTarget(ArticulationDriveAxis.X, currentJointAngle);
    //        }
    //    }  
    //}

    //private void SetNewMaxJointAngle()
    //{
    //    graspClosedAngle = armGraspManager.currentGraspAngles[(int)jointName];

    //    if (graspClosedAngle >= 0)
    //    {
    //        articulationBody.SetDriveLimits(ArticulationDriveAxis.X, graspOpenAngle, graspClosedAngle);
    //        negativeAngle = false;
    //    } else
    //    {
    //        articulationBody.SetDriveLimits(ArticulationDriveAxis.X, graspClosedAngle, graspOpenAngle);
    //        negativeAngle = true;
    //    }

    //    articulationBody.SetDriveTarget(ArticulationDriveAxis.X, currentJointAngle);
    //}
}
