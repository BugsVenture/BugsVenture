using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates
{
    Idle,
    OnWayToPlayer,
    StartAttack,
    IsAttacking,
    IsSearching, 
    Patrol,
    ExamineSound, 
    GotEffect
}

public enum Quadrants
{
    LeftTop, 
    RightTop, 
    RightBottom, 
    LeftBottom
}

public enum Effects
{
    None,
    Craziness, 
    SlowDown,
}