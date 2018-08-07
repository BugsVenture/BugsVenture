using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour {

    static BossRoom bossRoom;

    private Room currRoom;

    private DarkArea[] darkAreas;

    public static BossRoom GetInstance()
    {
        return bossRoom;
    }

    private void Awake()
    {
        if(bossRoom == null)
        {
            bossRoom = this;
        }
        currRoom = GetComponentInChildren<Room>();
        darkAreas = GetComponentsInChildren<DarkArea>();
    }

    public bool IsInDarkness()
    {
        bool isInDarkness = false;
        foreach (DarkArea dArea in darkAreas)
        {
            isInDarkness |= dArea.IsInArea;
        }
        return isInDarkness;
    }

    public Room GetRoom()
    {
        return currRoom;
    }

    public Vector3 CalculateClosestEntryPoint(Vector3 pos)
    {
        DarkArea tempArea = null;
        float dist = Mathf.Infinity;
        foreach(DarkArea dArea in darkAreas)
        {
            float tempDist = Vector3.Distance(dArea.transform.position, pos); 
            if (tempDist <dist)
            {
                tempArea = dArea;
                dist = tempDist;
            }
        }
        DarkArea.Entry tempEntry = new DarkArea.Entry(Vector3.zero, Vector3.zero);
        foreach(DarkArea.Entry entry in tempArea.GetEntries())
        {
            float tempDist = Vector3.Distance(pos, (entry.LeftPos + entry.RightPos) / 2); 
            if(tempDist < dist)
            {
                tempEntry = entry;
            }
        }
        return (tempEntry.LeftPos+tempEntry.RightPos)/2;
    }
}
