using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Helper class for patrolling
public class Patrol : MonoBehaviour
{

    Vector3[] pathPositions;


    void Start()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        pathPositions = new Vector3[transforms.Length-1];
        for (int i = 1; i < transforms.Length; i++)
            pathPositions[i-1] = transforms[i].position;
    }

    public int GetPathSize()
    {
        return pathPositions.Length;
    }

    public Vector3 NextPath(int index)
    {
        return pathPositions[index];
    }

};

