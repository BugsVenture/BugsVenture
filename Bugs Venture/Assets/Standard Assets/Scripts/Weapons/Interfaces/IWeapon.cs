using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{

    float FireRate { get; set; }
    int BulletSpeed { get; set; }

    bool Fire { get; set; }

    void Attack();

}
