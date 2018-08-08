using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkArea : MonoBehaviour {

    public List<Light> lights = new List<Light>();

    private bool isActive = false; 

    public struct Entry
    {
        public Entry(Vector3 vec, Vector3 vec2)
        {
            LeftPos = vec;
            RightPos = vec2;
        }
        public Vector3 RightPos, LeftPos;
    }

    public bool IsInArea = false;

    private List<GameObject> objects = new List<GameObject>();
    BoxCollider bCollider;

    private List<Entry> entries = new List<Entry>();

    private void Awake()
    {
        bCollider = GetComponent<BoxCollider>();
        CalculateEntries();
    }

    public void SetActive()
    {
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
        isActive = true;
    }

    public List<Entry> GetEntries()
    {
        return entries;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            if (other.gameObject.tag == "Boss")
            {
                IsInArea = true;
            }
            else
            {
                AddObject(other.gameObject);
            }
        }
    }

    private void AddObject(GameObject obj)
    {
        if(obj.GetComponent<MeshRenderer>() != null)
        {

            objects.Add(obj);
        }
    }

    private void CalculateEntries()
    {
        Vector3 LeftTop = new Vector3(this.transform.position.x - bCollider.bounds.extents.x , this.transform.position.y, this.transform.position.z + bCollider.bounds.extents.z);
        Vector3 RightTop = new Vector3(this.transform.position.x + bCollider.bounds.extents.x, this.transform.position.y, this.transform.position.z + bCollider.bounds.extents.z);
        Vector3 LeftBottom = new Vector3(this.transform.position.x - bCollider.bounds.extents.x, this.transform.position.y, this.transform.position.z - bCollider.bounds.extents.z);
        Vector3 RightBottom = new Vector3(this.transform.position.x + bCollider.bounds.extents.x, this.transform.position.y, this.transform.position.z - bCollider.bounds.extents.z);
        CalculateEntry(LeftTop, RightTop);
        CalculateEntry(LeftTop, LeftBottom);
        CalculateEntry(LeftBottom, RightBottom);
        CalculateEntry(RightTop, RightBottom);
    }
    private void CalculateEntry(Vector3 left, Vector3 right)
    {
        RaycastHit hit;
        if (Physics.Linecast(left, right, out hit))
        {
            if (hit.distance > 1)
            {
                entries.Add(new Entry(left, hit.point));
            }
            else
            {
                CalculateFromCenter(left, right, hit.collider.gameObject);
            }
            CalculateEntry(right, left);
        }
        else
        {
            entries.Add(new Entry(left, right));
        }
    }

    private void CalculateFromCenter(Vector3 left, Vector3 right, GameObject go)
    {
        RaycastHit hit;
        Vector3 center = (left+right)/2;
        if(Vector3.Distance(center, right) <1)
        {
            return;
        }
        if(Vector3.Distance(center, left) <1)
        {
            return;
        }
        if(Physics.Linecast(center, right, out hit))
        {
            if(hit.collider.gameObject == go)
            {
                CalculateFromCenter(center, right, go);
            }
            else
            {
                Vector3 rightObjectHit = hit.point;
                if(Physics.Raycast(rightObjectHit, (hit.point - left.normalized), out hit))
                {
                    if (hit.distance < 1)
                        return; 
                    else
                    {
                        entries.Add(new Entry(rightObjectHit, hit.point));
                    }
                }
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (isActive)
        {
            if (other.gameObject.tag == "Boss")
            {
                IsInArea = false;
            }
        }
    }
}
