using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContainer : MonoBehaviour {

    private Room[] rooms;

    private static RoomContainer containerInstance;

    private void Awake()
    {
        if (containerInstance == null)
            containerInstance = this;
    }

    void Start () {
        rooms = GetComponentsInChildren<Room>();
	}
    public static RoomContainer GetInstance()
    {
        return containerInstance;
    }

    public Room GetInsideRoom(Vector3 vec)
    {
        foreach(Room room in rooms)
        {
            if (room.PosInside(vec))
                return room;

        }
        return null;
    }
}
