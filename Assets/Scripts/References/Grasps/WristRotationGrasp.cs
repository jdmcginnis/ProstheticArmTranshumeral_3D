
public class WristRotationGrasp : GraspProperties
{
    public override float[] maxJointAngles { get; set; } = new float[]
    {
        0, // WristFlexion
        85, // WristRotation
        0, // IndexJoint0
        0, // IndexJoint1
        0, // IndexJoint2
        0, // MiddleJoint0
        0, // MiddleJoint1
        0, // MiddleJoint2
        0, // RingJoint0
        0, // RingJoint1
        0, // RingJoint2
        0, // PinkyJoint0
        0, // PinkyJoint1
        0, // PinkyJoint2
        0, // ThumbJoint0
        0, // ThumbJoint1
        0 // ThumbJoint2
    };

    public override float jointSpeed { get; set; } = 250;
}
