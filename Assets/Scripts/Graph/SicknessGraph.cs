using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SicknessGraph : MonoBehaviour
{
    graf testIt;
    string GrafId1, GrafId2, GrafId3;
    int cnt;
    // Start is called before the first frame update
    void Start()
    {
        testIt = GameObject.FindGameObjectWithTag("graf").GetComponent<graf>();
        GrafId1 = testIt.CreateGraf("Hest",Color.green);
        //GrafId2 = testIt.CreateGraf("Hund",Color.blue);
        //GrafId3 = testIt.CreateGraf("Kat", Color.yellow);

        InvokeRepeating("updateGrafInvoke", 1, 1);
    }

    void updateGrafInvoke()
    {
        testIt.AddData(GrafId1, new Vector2(cnt, GetNumberOfSickPeople()));
        //testIt.AddData(GrafId2, new Vector2(cnt, 0.50f * cnt * cnt));
        //testIt.AddData(GrafId3, new Vector2(cnt, Random.Range(5,10)));
        cnt++;
        testIt.updateGraf();
    }



   

    int GetNumberOfSickPeople()
    {
        GameObject[] allPeople = GameObject.FindGameObjectsWithTag("Person");
        int numberOfSickPeople = 0;

        foreach (GameObject person in allPeople)
        {
            if (person.GetComponent<Person>().isHumanSick == true)
            {
                numberOfSickPeople++;
            }
        }

        return numberOfSickPeople;
    }
}
