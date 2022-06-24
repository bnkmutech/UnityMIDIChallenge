using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardReceiver : MonoBehaviour
{
    [SerializeField] private List<Button> targetButtons;
    [SerializeField] private GameObject indicator;
    [SerializeField] private List<KeyCode> currentKeyCodeSetting = new List<KeyCode>{KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H};

    public List<Button> GetButtons()
    {
        return targetButtons;
    }

    public GameObject GetIndicator()
    {
        return indicator;
    }

    public List<Button> GetAllButtons()
    {
        return targetButtons;
    }

    private void CheckKeyboardKeyDown()
    {
        /* click button by keyboard */
        for(int i = 0; i < currentKeyCodeSetting.Count; i++)
        {
            if(Input.GetKeyDown(currentKeyCodeSetting[i]) && !MenuEvent.isPausing)
            {
                ExecuteEvents.Execute(targetButtons[i].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        CheckKeyboardKeyDown();
    }
}
