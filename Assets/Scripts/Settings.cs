
public static class Settings
{
    public enum InputModality
    {
        keyboard, // 0
        matlabRTC, // 1
        csvFile // 2
    }

    public static readonly InputModality inputModality = InputModality.keyboard;
    public static readonly float predictionRate = 0.100f;
    public static readonly bool fullyOpenToChangeGrasp = true; // Future bug fix required!

    public static readonly bool isNoiseEnabled = true;
    // TODO: Right now it doesn't do anything when max or min is reached; what we want is to keep evaluating if noise is on
    //       For example: The hand won't be perfectly still when open, it'll be jittering

    public class GaussianNoise
    {
        public static float stdDev = 5f; // deg
    }

    public class MajorityVoting
    {
        public static bool enabled = false;
        public static int bufferSize = 5;
    }


    public static GraspTypes[] activeGrasps =
    {
        GraspTypes.IndexFlexion,
        GraspTypes.Key,
        GraspTypes.Pinch
    };

    
}
