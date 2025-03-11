using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    void Start()
    {
        // 确保当前盒子生成动物
        CreateAnimalIfNeeded();
    }

    public void CreateAnimalIfNeeded()
    {
        //Debug.Log("1111");
        // 如果盒子里没有动物
        if (transform.childCount == 0)
        {
            GameObject sonAnimal = CreateAnimal();
            //Debug.Log("222");
            if (sonAnimal != null)
            {
                // 设为盒子的子对象
                sonAnimal.transform.parent = transform;
                // 让动物位置与盒子对齐
                sonAnimal.transform.position = transform.position;
            }
            else
            {
                Debug.LogError($"动物创建失败！盒子: {gameObject.name}");
            }
        }
    }

    GameObject CreateAnimal()
    {
        int num = Random.Range(1, 7);
        GameObject animalPrefab = Resources.Load("Prefabs/" + num) as GameObject;
        GameObject newAnimal = Instantiate(animalPrefab);
        if (animalPrefab == null)
        {
            Debug.LogError($"未找到 Prefabs/{num}，请检查预制体路径！");
            return null;
        }
        //AddClickEvent(animalPrefab);
        StartCoroutine(AttachYellowCheckDelayed(newAnimal));
        return newAnimal;
    }


    // **确保小黄框能正确附加，避免 Unity 物体层级初始化问题**
    IEnumerator AttachYellowCheckDelayed(GameObject animal)
    {
        yield return new WaitForEndOfFrame(); // **等待一帧，确保物体初始化完成**

        if (animal != null)
        {
            YellowCheckManager.Instance.AttachYellowCheck(animal);
        }
    }

    // 移除指定的子物体
    public void RemoveChildFromParent(GameObject target)
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject == target)
                {
                    // 强制移除父物体中的子物体
                    child.SetParent(null); // 设置子物体为无父物体
                    break;
                }
            }
        }
    }
}
