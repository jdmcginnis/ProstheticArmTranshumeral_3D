public static class JointTypes
{
    public enum JointNames
    {
        wristFlexionExtension, // 0
        wristRotation, // 1
        indexJoint0, // 2
        indexJoint1, // 3
        indexJoint2, // 4
        middleJoint0, // 5
        middleJoint1, // 6
        middleJoint2, // 7
        ringJoint0, // 8
        ringJoint1, // 9
        ringJoint2, // 10
        pinkyJoint0, // 11
        pinkyJoint1, // 12
        pinkyJoint2, // 13
        thumbJoint0, // 14
        thumbJoint1, // 15
        thumbJoint2 // 16
    }

    // Local axis of rotation
    public enum JointAxis
    {
        x, // 0
        y, // 1
        z // 2
    }

    // When closing the grasp, does the localRotation increase or decrease?
    public enum DirectionOfJointClosing
    {
        negative, // 0
        positive, // 1
        neutral // 2
    }
}
