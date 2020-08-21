using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFireButton : MonoBehaviour
{
    BattleManager battleman;

    public void Fire()
    {
        battleman = FindObjectOfType<BattleManager>();
        battleman.PlayerReleasedTargetingAndFires();
    }
}
