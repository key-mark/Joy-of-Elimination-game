using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yellowcheck : MonoBehaviour
{
    private GameObject yellowPrefab;

    void Start()
    {
        yellowPrefab = Resources.Load<GameObject>("Prefabs/yellowcheck");

        if (yellowPrefab == null)
        {
            Debug.LogError("未找到小黄框预制体，请检查路径！");
            return;
        }

        // 遍历所有标签为 "1" 到 "6" 的动物
        for (int i = 1; i <= 6; i++)
        {
            string tag = i.ToString();  // 标签转换为字符串
            GameObject[] animals = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject animal in animals)
            {
                if (!HasYellowCheck(animal))  // **检查是否已有小黄框**
                {
                    AttachYellowCheck(animal);
                }
            }
        }
    }

    // **检查动物是否已有小黄框**
    bool HasYellowCheck(GameObject animal)
    {
        return animal.transform.Find("yellowcheck(Clone)") != null;
    }

    void AttachYellowCheck(GameObject animal)
    {
        // 创建小黄框，并设置为该动物的子对象
        GameObject yellowCheck = Instantiate(yellowPrefab, animal.transform);

        // 让小黄框居中
        yellowCheck.transform.position = animal.transform.position;


        // 设置 Sorting Layer 以保证正确显示
        SpriteRenderer sr = yellowCheck.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder = -2;  // **默认不显示**
        }
    }
}
