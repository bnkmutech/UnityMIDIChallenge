using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameOnBoardingController : MonoBehaviour
{
    public GameObject TutorialPanel;

    // Start is called before the first frame update
    void Start()
    {
        TutorialPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnController.Instance.IsGameBegin = true;
            Destroy(gameObject);
        } 
    }
}
