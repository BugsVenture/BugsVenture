using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BossEnemyBehaviour))]
public class BossEditor : Editor {

    public override void OnInspectorGUI()
    {
        BossEnemyBehaviour behaviour = (BossEnemyBehaviour)target;

        behaviour.chargeTime = EditorGUILayout.FloatField("Charge Time", behaviour.chargeTime);

    }
}
