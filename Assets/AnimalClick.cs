
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class AnimalClick : MonoBehaviour
{
    private YellowCheckManager yellowCheckManager;
    void Start()
    {
        yellowCheckManager = FindObjectOfType<YellowCheckManager>();
        UnityAction<BaseEventData> click = new UnityAction<BaseEventData>(MyClick);
        EventTrigger.Entry myclick = new EventTrigger.Entry();
        myclick.eventID = EventTriggerType.PointerClick;
        myclick.callback.AddListener(click);

        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        trigger.triggers.Add(myclick);
    }


    public void MyClick(BaseEventData data)
    {
        //Debug.Log(gameObject.name + " ������ˣ�");
        //Debug.Log("����¼�������" + this.gameObject.name);
        GameObject clickedAnimal = this.gameObject;

        if (yellowCheckManager != null)
        {
            //Debug.Log( " �ı�ƿ�״̬��");
            yellowCheckManager.ToggleYellowCheck(gameObject); // �л�С�ƿ����ʾ״̬
        }
        ChangeAnimals.clickedAnimal = this.gameObject.transform;
        ChangeAnimals.AddAnimal();
    }
}
