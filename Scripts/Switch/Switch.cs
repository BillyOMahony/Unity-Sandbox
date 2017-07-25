using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switch : MonoBehaviour {

    public bool On;

    public abstract void Toggle();
    public abstract void ApplyState();

}
