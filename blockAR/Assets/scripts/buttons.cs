using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class buttons : MonoBehaviour
{

    public enum TestEnum { set, delete, color1, color2, color3};

    //This is what you need to show in the inspector.
    GameObject manager;
    public TestEnum choices;
    void Awake()
    {
        manager = GameObject.Find("/origin");
    }


    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void OnPointerDownDelegate(PointerEventData data)
    {
        Debug.Log("press down");
        switch (choices)
        {
            case TestEnum.set:
                Debug.Log("set");
                manager.GetComponent<managerAR>().Place();
                break;
            case TestEnum.delete:
                Debug.Log("delete");
                manager.GetComponent<managerAR>().delete();
                break;
            case TestEnum.color1:
                manager.GetComponent<managerAR>().color1();
                break;
            case TestEnum.color2:
                manager.GetComponent<managerAR>().color2();
                break;
            case TestEnum.color3:
                manager.GetComponent<managerAR>().color3();
                break;
            default:
                Debug.Log("NOTHING");
                break;
        }

    }

}
