using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class failpanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Image photo;
    public int number;
    public void fail()
    {
        ChangeAnimals changeAnimalsInstance = FindObjectOfType<ChangeAnimals>();
        if (changeAnimalsInstance == null)
        {
            Debug.LogError("ChangeAnimals 脚本未找到，请检查是否挂载在场景中的 GameObject 上！");
            return;
        }

        number = changeAnimalsInstance.GetQiunumber();

        if (number <= 0)
        {
            if (photo != null)
            {
                photo.transform.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("photo 变量未赋值，请在 Inspector 面板中拖入对应的 Image 对象！");
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fail();
    }
}
