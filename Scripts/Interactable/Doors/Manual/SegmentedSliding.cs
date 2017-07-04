using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentedSliding : Interactable {

    public int NumSegments;
    public float SegMoveDst;
    public float SegMoveTime;

    bool _inProgress = false;
    bool _segInProgress = false;
    bool _doorOpen = false;

    float _timePassed = 0;

    public Transform[] segs;

	// Use this for initialization
	void Start () {
        segs = new Transform[NumSegments];
        Transform CurrentSeg = transform;
        segs[0] = CurrentSeg;
        for (int i = 1; i < NumSegments; i++)
        {
            CurrentSeg = CurrentSeg.GetChild(0);
            segs[i] = CurrentSeg;
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Interact(GameObject Player) {

        StartCoroutine("ActivateDoor");

    }

    IEnumerator ActivateDoor()
    {
        if (!_inProgress)
        {
            _inProgress = true;

            float dst;

            if (_doorOpen)
            {
                dst = SegMoveDst * -1;
            }
            else
            {
                dst = SegMoveDst;
            }

            for (int i = NumSegments - 1; i >= 0; i--)
            {
                _segInProgress = true;

                Vector3 startPos = segs[i].position;

                Vector3 endPos = startPos + transform.up * dst;

                while (_segInProgress)
                {
                    _timePassed += Time.deltaTime;
                    float fracJourney = _timePassed / SegMoveTime;
                    segs[i].position = Vector3.Lerp(startPos, endPos, fracJourney);
                    if (_timePassed > SegMoveTime)
                    {
                        _timePassed = 0;
                        _segInProgress = false;
                    }

                    yield return null;
                }

                yield return new WaitForSeconds(1);

            }

            _doorOpen = !_doorOpen;
            _inProgress = false;
        }

        yield return null;
    }


}
