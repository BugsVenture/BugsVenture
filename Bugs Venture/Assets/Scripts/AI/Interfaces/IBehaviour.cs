using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehavior {

    float SightAngle { get; set; }

    float ActivationDistance { get; set; }

    float AttackRange { get; set; }

    float FireRate { get; set; }

    float SightDistance { get; set; }

    void HearPlayer();
}
