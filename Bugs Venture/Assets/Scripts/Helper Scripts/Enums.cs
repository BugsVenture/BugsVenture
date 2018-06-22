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
    RightTop, 
    LeftTop, 
    RightBottom, 
    LeftBottom
}

public enum Effects
{
    None,
    Craziness, 
    SlowDown,
}