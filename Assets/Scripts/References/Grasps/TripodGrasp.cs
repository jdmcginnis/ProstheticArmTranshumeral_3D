
public class TripodGrasp : GraspProperties
{
    public override float[] maxJointAngles { get; set; } = new float[]
    {
        0, // WristFlexion
        0, // WristRotation
        53, // IndexJoint0
        43, // IndexJoint1
        45, // IndexJoint2
        53, // MiddleJoint0
        43, // MiddleJoint1
        45, // MiddleJoint2
        0, // RingJoint0
        0, // RingJoint1
        0, // RingJoint2
        0, // PinkyJoint0
        0, // PinkyJoint1
        0, // PinkyJoint2
        73, // ThumbJoint0
        0, // ThumbJoint1
        35 // ThumbJoint2
    };

    public override float jointSpeed { get; set; } = 250;
}
