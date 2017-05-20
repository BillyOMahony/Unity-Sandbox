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

    void ApplyState()
    {
        Cursor.lockState = cursorMode;
        Cursor.visible = (CursorLockMode.Locked != cursorMode);
    }

    public void LockCursor()
    {
        cursorMode = CursorLockMode.Locked;
        ApplyState();
    }

    public void UnlockCursor()
    {
        cursorMode = CursorLockMode.Confined;
        ApplyState();
    }

}

