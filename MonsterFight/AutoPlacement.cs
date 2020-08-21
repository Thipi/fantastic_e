using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlacement : MonoBehaviour
{
    [SerializeField]
    private MonsterMaster monsterMaster;

    [SerializeField]
    Transform pivotPosForMonsters;

    //To make sure we're not seeing the tutorial again.
    [SerializeField]
    GameObject tutorial2ndPanel;

    public void BeginTheGame()
    {
        monsterMaster = GetComponent<MonsterMaster>();
        GameObject monster = Instantiate(monsterMaster.chosenMonsterForFight, pivotPosForMonsters.position, monsterMaster.chosenMonsterForFight.transform.rotation);
        monster.transform.SetParent(pivotPosForMonsters);
        monster.transform.position = new Vector3(pivotPosForMonsters.position.x, pivotPosForMonsters.position.y, pivotPosForMonsters.position.z);
    }
}
