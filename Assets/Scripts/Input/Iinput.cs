using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    bool CameraSwitchRight();
    bool CameraSwitchLeft();
    bool SendQueueStraight();
    bool SendQueueRight();
    bool SendQueueLeft();
    bool SendQueueBackward();

    bool PlayerSwitchLeft();
    bool PlayerSwitchRight();
    bool PlayerMoveForward();
    bool PlayerMoveBackwards();
}
