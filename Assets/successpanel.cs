using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class successpanel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject passpanel;  // 仅声明，不在这里初始化
    public Image photo;
    void Awake()
    {
        passpanel = GameObject.Find("PassPanel");  // 在 Awake 中获取对象
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
