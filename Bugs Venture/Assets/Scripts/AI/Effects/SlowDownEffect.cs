using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownEffect : BaseEffect {

    private bool isActive = false;

    public float Duration
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
            throw new System.NotImplementedException();
        }
    }

    public bool IsActive {
        get
        {
            return IsActive;
        }
        set
        {
            isActive = value;
        }
    }

    public override void ActivateEffect(IBaseEnemy enemy)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
