using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestInMainCanvas : MonoBehaviour
{

    private MidiReader midiReader;

    private void InsertionSort_01223_IfInsertingListLessThanOriginalList()
    {
        List<List<int>> input_originalList = new List<List<int>>();
        List<int> input_insertingList = new List<int>{2, 0};

        for(int i = 0; i <= 3; i++)
        {
            input_originalList.Add(new List<int>{i, 0});
        }

        List<List<int>> expectedResult = new List<List<int>>();
        for(int i = 0; i <= 3; i++)
        {
            expectedResult.Add(new List<int>{i, 0});
            if(i == 2)
            {
                expectedResult.Add(new List<int>{i, 0});
            }
        }

        List<List<int>> realResult = midiReader.InsertionSort(input_originalList, input_insertingList);

        string ers = "";
        foreach(List<int> er in expectedResult)
        {
            ers += er[0] + " ";
        }
        string rrs = "";
        foreach(List<int> rr in realResult)
        {
            rrs += rr[0] + " ";
        }


        Debug.Log("MidiReader:InsertionSort 1 test result : " + (ers == rrs));
    }

    private void InsertionSort_01223_IfInsertingListEqualOriginalList()
    {
        List<List<int>> input_originalList = new List<List<int>>();
        List<int> input_insertingList = new List<int>{3, 0};

        for(int i = 0; i <= 3; i++)
        {
            input_originalList.Add(new List<int>{i, 0});
        }

        List<List<int>> expectedResult = new List<List<int>>();
        for(int i = 0; i <= 3; i++)
        {
            expectedResult.Add(new List<int>{i, 0});
            if(i == 3)
            {
                expectedResult.Add(new List<int>{i, 0});
            }
        }

        List<List<int>> realResult = midiReader.InsertionSort(input_originalList, input_insertingList);

        string ers = "";
        foreach(List<int> er in expectedResult)
        {
            ers += er[0] + " ";
        }
        string rrs = "";
        foreach(List<int> rr in realResult)
        {
            rrs += rr[0] + " ";
        }


        Debug.Log("MidiReader:InsertionSort 2 test result : " + (ers == rrs));
    }

    private void InsertionSort_01223_IfInsertingListMoreThanOriginalList()
    {
        List<List<int>> input_originalList = new List<List<int>>();
        List<int> input_insertingList = new List<int>{4, 0};

        for(int i = 0; i <= 3; i++)
        {
            input_originalList.Add(new List<int>{i, 0});
        }

        List<List<int>> expectedResult = new List<List<int>>();
        for(int i = 0; i <= 4; i++)
        {
            expectedResult.Add(new List<int>{i, 0});
        }

        List<List<int>> realResult = midiReader.InsertionSort(input_originalList, input_insertingList);

        string ers = "";
        foreach(List<int> er in expectedResult)
        {
            ers += er[0] + " ";
        }
        string rrs = "";
        foreach(List<int> rr in realResult)
        {
            rrs += rr[0] + " ";
        }


        Debug.Log("MidiReader:InsertionSort 3 test result : " + (ers == rrs));
    }

    // Start is called before the first frame update
    private void Start()
    {
        midiReader = gameObject.GetComponent<MidiReader>();

        InsertionSort_01223_IfInsertingListLessThanOriginalList();
        InsertionSort_01223_IfInsertingListEqualOriginalList();
        InsertionSort_01223_IfInsertingListMoreThanOriginalList();
    }
}
