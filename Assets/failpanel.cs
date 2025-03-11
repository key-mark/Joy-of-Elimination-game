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
            Debug.LogError("ChangeAnimals �ű�δ�ҵ��������Ƿ�����ڳ����е� GameObject �ϣ�");
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
                Debug.LogError("photo ����δ��ֵ������ Inspector ����������Ӧ�� Image ����");
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
