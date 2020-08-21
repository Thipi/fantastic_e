using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEditor : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    GameObject dummyPivotforTesting;

    [SerializeField]
    GameObject callToTap;

    MonsterMaster monsterMaster;
    private void Awake()
    {
        dummyPivotforTesting.SetActive(false);
        monsterMaster = FindObjectOfType<MonsterMaster>();
    }
    void FixedUpdate()
    {
        if (Input.GetKey("left ctrl"))
        {
            dummyPivotforTesting.SetActive(true);
            GameObject monsterClone = Instantiate(monsterMaster.chosenMonsterForFight, dummyPivotforTesting.transform.position, Quaternion.identity);
            monsterClone.transform.SetParent(dummyPivotforTesting.transform, false);
            callToTap.SetActive(false);
            }
        }
#endif
}
