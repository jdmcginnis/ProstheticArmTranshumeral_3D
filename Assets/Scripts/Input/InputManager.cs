using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    public static event Action<GraspTypes.GraspNames> OnNewGraspSelected;

    [SerializeField] private ArmGraspManager armGraspManager;
    [SerializeField] private GameObject keyboardInputObj;
    [SerializeField] private GameObject matlabInputObj;

    private const int bufferSize = 3;
    private GraspTypes.GraspNames[] majorityVoteBuffer;
    private int bufferCounter = 0;

    private void Awake()
    {
        majorityVoteBuffer = Enumerable.Repeat(GraspTypes.GraspNames.Rest, bufferSize).ToArray();

        if (Settings.usingKeyboard)
        {
            keyboardInputObj.SetActive(true);
            matlabInputObj.SetActive(false);
        } else
        {
            Time.fixedDeltaTime = Settings.predictionRate;
            keyboardInputObj.SetActive(false);
            matlabInputObj.SetActive(true);
        }

    }


    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();
        }

        if (Settings.usingKeyboard)
        {
            keyboardInputObj.GetComponent<KeyboardInputManager>().InitializeKeyboardInput(playerInput);
        } else
        {
            matlabInputObj.GetComponent<UnityMainThreadDispatcher>().InitializeMainThreadDispatcher();
            matlabInputObj.GetComponent<MatlabConnection>().InitializeMatlabInput();
        }
    }


    private void OnDisable()
    {
        playerInput.Disable();

        if (!Settings.usingKeyboard)
            matlabInputObj.GetComponent<MatlabConnection>().CloseOutEverything();
    }


    // Called from external script whenever we have a new prediction
    // Executes majority voting and uses the output as our new predicted grasp
    public void HandleNewInput(GraspTypes.GraspNames graspPrediction)
    {
        // Debug.Log("New Input: " + graspPrediction.ToString());

        // Majority Voting
        majorityVoteBuffer[bufferCounter] = graspPrediction;

        GraspTypes.GraspNames majorityPrediction = majorityVoteBuffer
            .GroupBy(i => i) //place all identical values into groups
            .OrderByDescending(g => g.Count()) //order groups by the size of the group desc
            .Select(g => g.Key) //key of the group is representative of items in the group
            .First(); //first in the list is the most frequent (modal) value

        bufferCounter = (bufferCounter + 1) % bufferSize;

        Debug.Log("Majority Prediction: " + majorityPrediction.ToString());

        // If we are resting, we change nothing
        if (majorityPrediction == GraspTypes.GraspNames.Rest)
            return;

        if (majorityPrediction == GraspTypes.GraspNames.Open)
            armGraspManager.OpenGrasp();
        else if (majorityPrediction == armGraspManager.currentGrasp)
            armGraspManager.CloseGrasp();
        else if (armGraspManager.graspFullyOpen)
            OnNewGraspSelected?.Invoke(majorityPrediction);

    }

}
