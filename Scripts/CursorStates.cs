using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorStates : Singleton<CursorStates> {

    protected CursorStates() { }

    CursorLockMode cursorMode;

    void Start()
    {
        LockCursor();
    }

    /// <summary>
    /// Applies Cursor state
    /// </summary>
    void ApplyState()
    {
        Cursor.lockState = cursorMode;
        Cursor.visible = (CursorLockMode.Locked != cursorMode);
        Debug.Log(cursorMode.ToString());
    }

    /// <summary>
    /// Locks the Cursor to center of the screen
    /// </summary>
    public void LockCursor()
    {
        cursorMode = CursorLockMode.Locked;
        ApplyState();
    }

    /// <summary>
    /// Allows cursor movement within bounds of the window
    /// </summary>
    public void UnlockCursor()
    {
        cursorMode = CursorLockMode.Confined;
        ApplyState();
    }

}

