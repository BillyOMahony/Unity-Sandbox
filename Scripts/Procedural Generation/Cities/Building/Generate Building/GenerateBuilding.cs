using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBuilding : MonoBehaviour {

    public int seed;
    public int maxFloors;
    public int numFloors;

    public GameObject groundFloor;
    public GameObject[] midFloors;
    public GameObject topFloor;
    public GameObject[] stairs;

    public float heightOffset;

    public bool midFloorsInOrder;
    public bool hasElevator;

    void Start()
    {
        Random.InitState(seed);
        numFloors = Random.Range(3, maxFloors + 1);
        Build();
    }

    void Build()
    {
        //Place floors
        GameObject floor = Instantiate(groundFloor, transform.position, transform.rotation, transform) as GameObject;

        float offset = 0;

        for (int x = 0; x < numFloors - 2; x++)
        {
            floor = Instantiate(midFloors[0], transform.position, transform.rotation, transform) as GameObject;
            floor.transform.position += new Vector3(0, offset, 0);

            offset += heightOffset;
        }
        
        floor = Instantiate(topFloor, transform.position, transform.rotation, transform) as GameObject;
        floor.transform.position += new Vector3(0, offset + heightOffset, 0);

        //Place Stairs
        GameObject stair = null;

        offset = 0;

        for (int x = 0; x < numFloors - 1; x++)
        {
            if (x % 2 == 0)
            {
                stair = Instantiate(stairs[0], transform.position, transform.rotation, transform);
            }
            else
            {
                stair = Instantiate(stairs[1], transform.position, transform.rotation, transform);
            }
            offset += heightOffset;
            stair.transform.position += new Vector3(0, offset, 0);
        }
    }

}
