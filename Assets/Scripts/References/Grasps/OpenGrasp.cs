
public class OpenGrasp : GraspProperties
{

    // Defines the hand at rest; going from making another grasp
    // Hand is naturally open; not perfectly open (not all angles are 0)
    public override float[] maxJointAngles { get; set; } = new float[]
    {
        0, // WristFlexion
        0, // WristRotation
        -5.3f, // IndexJoint0
        -3.9f, // IndexJoint1
        -5.1f, // IndexJoint2
        -2.7f, // MiddleJoint0
        -5.0f, // MiddleJoint1
        -7.4f, // MiddleJoint2
        -3.1f, // RingJoint0
        -5.3f, // RingJoint1
        -7.9f, // RingJoint2
        -5.5f, // PinkyJoint0
        -3.7f, // PinkyJoint1
        -5.4f, // PinkyJoint2
        -45.6f, // ThumbJoint0
        35.1f, // ThumbJoint1
        -6.0f // ThumbJoint2
    };

    public override float jointSpeed { get; set; } = 250;
}
