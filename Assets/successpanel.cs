using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class successpanel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject passpanel;  // �����������������ʼ��
    public Image photo;
    void Awake()
    {
        passpanel = GameObject.Find("PassPanel");  // �� Awake �л�ȡ����
    }
    
    public void success()
    {
        PassPanel panel = passpanel.GetComponent<PassPanel>();
        if (panel.number2 >= 6 && panel.number4 >= 10 && panel.number5 >= 9)
        {
            photo.transform.gameObject.SetActive(true);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        success();
    }
}
