using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    private MeshRenderer mr;
    private Room[] roomParts;
    private bool hasParts = false; 
    private Vector3[] partSizes;

    // Use this for initialization
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        Room[] tempParts = GetComponentsInChildren<Room>();
        if (tempParts.Length > 1)
        {
            roomParts = tempParts;
            hasParts = true;
        }
    }


    public Vector3 GetRandomPosInsideRoom()
    {
        return new Vector3(Random.Range(mr.bounds.center.x - mr.bounds.extents.x, mr.bounds.center.x + mr.bounds.extents.x),
                           mr.bounds.center.y,
                           Random.Range(mr.bounds.center.z - mr.bounds.extents.z, mr.bounds.center.z + mr.bounds.extents.z));
    }

    public Vector3 GetRandomPosInsideQuadrant(Quadrants quad)
    {
        switch (quad)
        {
            case Quadrants.LeftTop:
                return new Vector3(Random.Range(mr.bounds.center.x - mr.bounds.extents.x, mr.bounds.center.x),
                           mr.bounds.center.y,
                           Random.Range(mr.bounds.center.z - mr.bounds.extents.z,mr.bounds.center.z));
            case Quadrants.RightTop:
                return new Vector3(Random.Range(mr.bounds.center.x, mr.bounds.center.x + mr.bounds.extents.x),
                                   mr.bounds.center.y,
                                   Random.Range(mr.bounds.center.z - mr.bounds.extents.z, mr.bounds.center.z)); 
            case Quadrants.LeftBottom:
                return new Vector3(Random.Range(mr.bounds.center.x - mr.bounds.extents.x, mr.bounds.center.x),
                                   mr.bounds.center.y,
                                   Random.Range(mr.bounds.center.z, mr.bounds.center.z + mr.bounds.extents.z));
            case Quadrants.RightBottom:
                return new Vector3(Random.Range(mr.bounds.center.x, mr.bounds.center.x + mr.bounds.extents.x),
                                    mr.bounds.center.y,
                                    Random.Range(mr.bounds.center.z, mr.bounds.center.z + mr.bounds.extents.z));
        }
        return Vector3.zero; 
    }

    public bool PosInside(Vector3 vec)
    {
        if (vec.x < mr.bounds.center.x + mr.bounds.extents.x && vec.x > mr.bounds.center.x - mr.bounds.extents.x)
        {
            if (vec.z < mr.bounds.center.z + mr.bounds.extents.z && vec.z > mr.bounds.center.z - mr.bounds.extents.z)
            {
                return true;
            }
        }
        return false;
    }
    public bool PosInsideQuadrant(Vector3 vec, Quadrants quad)
    {
        switch (quad)
        {
            case Quadrants.LeftTop:
                if (vec.x < mr.bounds.center.x && vec.x > mr.bounds.center.x - mr.bounds.extents.x)
                {
                    if(vec.z < mr.bounds.center.z && vec.z > mr.bounds.center.z - mr.bounds.extents.z)
                    {
                        return true;
                    }
                }
                break;
            case Quadrants.RightTop:
                if(vec.x < mr.bounds.center.x + mr.bounds.extents.x && vec.x > mr.bounds.center.x)
                {
                    if(vec.z < mr.bounds.center.z && vec.z > mr.bounds.center.z - mr.bounds.extents.z)
                    {
                        return true;
                    }
                }
                break;
            case Quadrants.LeftBottom:
                if(vec.x < mr.bounds.center.x && vec.x > mr.bounds.center.x - mr.bounds.extents.x)
                {
                    if(vec.z < mr.bounds.center.z +mr.bounds.extents.z && vec.z > mr.bounds.center.z)
                    {
                        return true;
                    }
                }
                break;
            case Quadrants.RightBottom:
                if(vec.x < mr.bounds.center.x + mr.bounds.extents.x && vec.x > mr.bounds.center.x)
                {
                    if(vec.z < mr.bounds.center.z + mr.bounds.extents.z && vec.z > mr.bounds.center.z)
                    {
                        return true;
                    }
                }
                break;
        }
        return false; 
    }
}

