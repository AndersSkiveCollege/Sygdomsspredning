using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphRunner : MonoBehaviour
{
    graf testIt;
    string GrafId1;
    int cnt;

    // Start is called before the first frame update
    void Start()
    {
        testIt = GameObject.FindGameObjectWithTag("graf").GetComponent<graf>();
        GrafId1 = testIt.CreateGraf("Syge Personer",Color.black);

        
        InvokeRepeating("updateGrafInvoke", 1, 1);
    }

    int GetNumberOfSickPeople()
    {
        GameObject[] allPeople = GameObject.FindGameObjectsWithTag("Person");
        int numberOfSickPeople = 0;

        foreach (GameObject person in allPeople)
        {
            if (person.GetComponent<Person>().isSick == true)
            {
                numberOfSickPeople++;
            }
        }

        return numberOfSickPeople;
    }

    void updateGrafInvoke()
    {
        cnt++;
        int numberOfSickPeople = GetNumberOfSickPeople();
        testIt.AddData(GrafId1, new Vector2(cnt, numberOfSickPeople));
        testIt.updateGraf();
    }
}
