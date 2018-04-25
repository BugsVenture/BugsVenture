﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //Public
    public int selectedWeapon = 0;

    // Use this for initialization
    void Start ()
    {
        SelectWeapon();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        int previousSelectedWeapon = selectedWeapon;

        //Select Weapon with Mouse ScrollWheel up
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
            selectedWeapon++;
        }

        //Select Weapon with Mouse ScrollWheel down
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        //Select Weapon with Bumper down
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

        //Select Weapon with Bumper up
        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }


        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++; 
        }
    }
}
