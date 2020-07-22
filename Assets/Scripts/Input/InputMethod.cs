using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputData", menuName = "Input/Keyboard", order = 0)]
public class InputMethod : ScriptableObject, IInput
{
    [SerializeField]
    KeyCode cameraSwitchLeftKey,cameraSwitchRightKey,rotatePlayerLeft,rotatePlayerRight,movePlayerForward,movePlayerBackwards,
        sendQueueStraight,sendQueueRight,sendQueueLeft,sendQueueBackward;



    public bool CameraSwitchLeft()
    {
        return Input.GetKeyDown(cameraSwitchLeftKey);
    }

    public bool CameraSwitchRight()
    {
        return Input.GetKeyDown(cameraSwitchRightKey);
    }

    public bool PlayerMoveBackwards()
    {
        return Input.GetKey(movePlayerBackwards);
    }

    public bool PlayerMoveForward()
    {
        return Input.GetKey(movePlayerForward);
    }

    public bool PlayerSwitchLeft()
    {
        return Input.GetKey(rotatePlayerLeft);
    }

    public bool PlayerSwitchRight()
    {
        return Input.GetKey(rotatePlayerRight);
    }

    public bool SendQueueBackward()
    {
        return Input.GetKeyDown(sendQueueBackward);
    }

    public bool SendQueueLeft()
    {
        return Input.GetKeyDown(sendQueueLeft);
    }

    public bool SendQueueRight()
    {
        return Input.GetKeyDown(sendQueueRight);
    }

    public bool SendQueueStraight()
    {
        return Input.GetKeyDown(sendQueueStraight);
    }
}
