using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //Public
    public int selectedWeapon = 0;
    
    private BaseWeapon currWeapon;

    // Use this for initialization
    void Start ()
    {
        SelectWeapon();

	}
	
    public BaseWeapon GetWeapon()
    {
        return currWeapon;
    }

	// Update is called once per frame
	void Update ()
    {
        int previousSelectedWeapon = selectedWeapon;

        //Select Weapon with Mouse ScrollWheel up
        if(Input.GetAxis("Mouse ScrollWheel") > 0f|| Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

        //Select Weapon with Mouse ScrollWheel down or Bumper
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

    }

    void SelectWeapon()
    {
        int i = 0;

        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
                weapon.gameObject.SetActive(false);
            i++; 
        }
        currWeapon = GetComponentInChildren<BaseWeapon>();
    }
}
