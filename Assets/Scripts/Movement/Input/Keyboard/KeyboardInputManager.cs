using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameObject this script is attached to is initially inactive
public class KeyboardInputManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    GraspTypes keyHeldDown = GraspTypes.Rest;

    public void InitializeKeyboardInput(PlayerInput playerInput)
    {
        playerInput.Disable();

        #region Keyboard Grasp Bindings
        playerInput.KeyboardInput_Grasps.GraspInput_Key0.performed += i => keyHeldDown = (GraspTypes)0;
        playerInput.KeyboardInput_Grasps.GraspInput_Key0.canceled += i => keyHeldDown = GraspTypes.Rest;

        playerInput.KeyboardInput_Grasps.GraspInput_Key1.performed += i => keyHeldDown = (GraspTypes)1;
        playerInput.KeyboardInput_Grasps.GraspInput_Key1.canceled += i => keyHeldDown = GraspTypes.Rest;

        playerInput.KeyboardInput_Grasps.GraspInput_Key2.performed += i => keyHeldDown = (GraspTypes)2;
        playerInput.KeyboardInput_Grasps.GraspInput_Key2.canceled += i => keyHeldDown = GraspTypes.Rest;

        playerInput.KeyboardInput_Grasps.GraspInput_Key3.performed += i => keyHeldDown = (GraspTypes)3;
        playerInput.KeyboardInput_Grasps.GraspInput_Key3.canceled += i => keyHeldDown = GraspTypes.Rest;

        playerInput.KeyboardInput_Grasps.GraspInput_Key4.performed += i => keyHeldDown = (GraspTypes)4;
        playerInput.KeyboardInput_Grasps.GraspInput_Key4.canceled += i => keyHeldDown = GraspTypes.Rest;

        playerInput.KeyboardInput_Grasps.GraspInput_Key5.performed += i => keyHeldDown = (GraspTypes)5;
        playerInput.KeyboardInput_Grasps.GraspInput_Key5.canceled += i => keyHeldDown = GraspTypes.Rest;

        playerInput.KeyboardInput_Grasps.GraspInput_Key6.performed += i => keyHeldDown = (GraspTypes)6;
        playerInput.KeyboardInput_Grasps.GraspInput_Key6.canceled += i => keyHeldDown = GraspTypes.Rest;

        playerInput.KeyboardInput_Grasps.GraspInput_Key7.performed += i => keyHeldDown = (GraspTypes)7;
        playerInput.KeyboardInput_Grasps.GraspInput_Key7.canceled += i => keyHeldDown = GraspTypes.Rest;

        playerInput.KeyboardInput_Grasps.GraspInput_Key8.performed += i => keyHeldDown = (GraspTypes)8;
        playerInput.KeyboardInput_Grasps.GraspInput_Key8.canceled += i => keyHeldDown = GraspTypes.Rest;

        playerInput.KeyboardInput_Grasps.GraspInput_Key9.performed += i => keyHeldDown = (GraspTypes)9;
        playerInput.KeyboardInput_Grasps.GraspInput_Key9.canceled += i => keyHeldDown = GraspTypes.Rest;

        playerInput.KeyboardInput_Grasps.GraspInput_Spacebar.performed += i => keyHeldDown = GraspTypes.Open;
        playerInput.KeyboardInput_Grasps.GraspInput_Spacebar.canceled += i => keyHeldDown = GraspTypes.Rest;
        #endregion

        playerInput.Enable();

        StartCoroutine(HandleKeyboardInput());
    }


    // Continuously checks every fixed frame what key is being held down
    private IEnumerator HandleKeyboardInput()
    {
        // Wait until everything is initialized
        yield return new WaitForFixedUpdate();

        while (true)
        {
            inputManager.HandleNewClassificationInput(keyHeldDown);
            yield return new WaitForFixedUpdate();
        }
    }
}
