
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
        //Debug.Log(gameObject.name + " 被点击了！");
        //Debug.Log("点击事件触发：" + this.gameObject.name);
        GameObject clickedAnimal = this.gameObject;

        if (yellowCheckManager != null)
        {
            //Debug.Log( " 改变黄框状态！");
            yellowCheckManager.ToggleYellowCheck(gameObject); // 切换小黄框的显示状态
        }
        ChangeAnimals.clickedAnimal = this.gameObject.transform;
        ChangeAnimals.AddAnimal();
    }
}
