using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject keyboardInputManagerObj;
    [SerializeField] private HandGraspManager handGraspManager;

    private PlayerInput playerInput;
    private MajorityVotingBuffer majorityVotingBuffer;

    private void Awake()
    {
        // Verify all input managers are initially inactive
        keyboardInputManagerObj.SetActive(false);

        ConfigureMajorityVoting();

        ConfigureInputModalities();
    }

    private void ConfigureMajorityVoting()
    {
        if (Settings.MajorityVoting.enabled)
        {
            int bufferSize = Settings.MajorityVoting.bufferSize;
            majorityVotingBuffer = new MajorityVotingBuffer(bufferSize);
        }
    }

    private void ConfigureInputModalities()
    {
        if (Settings.inputModality == Settings.InputModality.keyboard)
        {
            InitializeKeyboardInput();
        }
    }

    private void InitializeKeyboardInput()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();
        }

        keyboardInputManagerObj.SetActive(true);
        keyboardInputManagerObj.GetComponent<KeyboardInputManager>().InitializeKeyboardInput(playerInput);
    }

    public void HandleNewInput(GraspTypes newGraspInput)
    {
        GraspTypes graspPrediction;

        if (Settings.MajorityVoting.enabled)
        {
            graspPrediction = majorityVotingBuffer.HandleNewPrediction(newGraspInput);
        } else
        {
            graspPrediction = newGraspInput;
        }

        // If we are resting, do nothing
        if (graspPrediction == GraspTypes.Rest)
            return;

        if (graspPrediction == GraspTypes.Open)
            handGraspManager.OpenGraspByOneStep();
        else if (graspPrediction == handGraspManager.currentGraspState)
            handGraspManager.CloseGraspByOneStep();
        else if (handGraspManager.isHandFullyOpen || !Settings.fullyOpenToChangeGrasp)
            handGraspManager.ChangeGrasp(graspPrediction);

    }


}


//

//public static event Action<GraspTypes.GraspNames> OnNewGraspSelected;

//[SerializeField] private ArmGraspManager armGraspManager;
//[SerializeField] private GameObject keyboardInputObj;
//[SerializeField] private GameObject matlabInputObj;


//private void Awake()
//{

//    if (Settings.usingKeyboard)
//    {
//        keyboardInputObj.SetActive(true);
//        matlabInputObj.SetActive(false);
//    } else
//    {
//        Time.fixedDeltaTime = Settings.predictionRate;
//        keyboardInputObj.SetActive(false);
//        matlabInputObj.SetActive(true);
//    }

//}


//private void OnEnable()
//{


//    if (Settings.usingKeyboard)
//    {
//        keyboardInputObj.GetComponent<KeyboardInputManager>().InitializeKeyboardInput(playerInput);
//    } else
//    {
//        matlabInputObj.GetComponent<UnityMainThreadDispatcher>().InitializeMainThreadDispatcher();
//        matlabInputObj.GetComponent<MatlabConnection>().InitializeMatlabInput();
//    }
//}


//private void OnDisable()
//{
//    playerInput.Disable();

//    if (!Settings.usingKeyboard)
//        matlabInputObj.GetComponent<MatlabConnection>().CloseOutEverything();
//}
