using System;
using UnityEngine;

public class PlayerInputManager : SingletonMonobehaviour<PlayerInputManager>
{
    public event EventHandler OnEscButtonPressed;
    public event EventHandler OnLeftMouseButtonPressed;
    public event EventHandler OnLeftAltButtonPressed;

    //======================================================================
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnEscButtonPressed?.Invoke(this, EventArgs.Empty);

        if (Input.GetMouseButtonDown(0))
            OnLeftMouseButtonPressed?.Invoke(this, EventArgs.Empty);

        if (Input.GetKeyDown(KeyCode.LeftAlt))
            OnLeftAltButtonPressed?.Invoke(this, EventArgs.Empty);
    }
}