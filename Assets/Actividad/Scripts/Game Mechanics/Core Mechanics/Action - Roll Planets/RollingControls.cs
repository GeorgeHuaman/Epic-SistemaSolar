using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingControls : IAvatarInputActionsListener
{
    public void StartRollingControls()
    {
        SpatialBridge.inputService.StartAvatarInputCapture(false, false, false, true, this);
    }

    public void StopRollingControls()
    {
        SpatialBridge.inputService.StartAvatarInputCapture(false, false, false, false, null);
    }

    #region CUSTOM_CONTROLS

    public void OnAvatarActionInput(InputPhase inputPhase)
    {
        RollActionManager.instance.EndRoll();
    }

    public void OnAvatarAutoSprintToggled(bool on)
    {
        //No custom controls
    }

    public void OnAvatarJumpInput(InputPhase inputPhase)
    {
        //No custom controls
    }

    public void OnAvatarMoveInput(InputPhase inputPhase, Vector2 inputMove)
    {
        //No custom controls
    }

    public void OnAvatarSprintInput(InputPhase inputPhase)
    {
        //No custom controls
    }

    public void OnInputCaptureStarted(InputCaptureType type)
    {
        //No custom controls
    }

    public void OnInputCaptureStopped(InputCaptureType type)
    {
        //No custom controls
    }

    #endregion
}
