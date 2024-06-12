using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGraspFormat
{
    public abstract float[] maxJointAngles { get; set; }
}
