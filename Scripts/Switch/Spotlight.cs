using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : Switch {

    public GameObject spotlight;

    void Start()
    {
        ApplyState();
    }

    public override void Toggle()
    {
        On = !On;
        ApplyState();
    }

    public override void ApplyState()
    {
        spotlight.SetActive(On);
    }

}
