
public class KeyGrasp : GraspProperties
{
    public override float[] maxJointAngles { get; set; } = new float[]
    {
        0, // WristFlexion
        0, // WristRotation
        85, // IndexJoint0
        82, // IndexJoint1
        78, // IndexJoint2
        85, // MiddleJoint0
        82, // MiddleJoint1
        78, // MiddleJoint2
        85, // RingJoint0
        82, // RingJoint1
        78, // RingJoint2
        85, // PinkyJoint0
        82, // PinkyJoint1
        78, // PinkyJoint2
        0, // ThumbJoint0
        12, // ThumbJoint1
        70 // ThumbJoint2
    };

    public override float jointSpeed { get; set; } = 250;
}
