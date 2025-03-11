using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using static AnimalGrid;

public class ChangeAnimals : MonoBehaviour
{
    private static Transform[] exchange = new Transform[2]; // 存放被点击的动物
    private static int i = 0; // 当前数组索引
    private static float gridSize = 1.0f; // 格子大小，决定相邻关系
    private static Transform highlightedAnimal = null; // 小黄框当前覆盖的动物
    public static Transform clickedAnimal;
    public static int qiunumber;
    public TextMeshProUGUI qiuqiutext;
    private void Start()
    {
        qiunumber = 3;
        QiuText();
    }
    public int GetQiunumber()
    {
        return qiunumber;
    }
    public  void QiuText()
    {
        qiuqiutext.text = qiunumber.ToString();
    }
    public static void SetHighlightedAnimal(Transform animal)
    {
        highlightedAnimal = animal;
    }

    // 添加点击的动物
    public static void AddAnimal()
    {
        if (clickedAnimal == null) return;

        // 先把黄框里的物体固定为 exchange[0]
        if (highlightedAnimal != null)
        {
            exchange[0] = highlightedAnimal.transform;
        }

        // 点击的物体不在小黄框内，才允许放入 exchange[1]
        if (exchange[0] != null && exchange[1] == null && clickedAnimal.transform != exchange[0])
        {
            exchange[1] = clickedAnimal.transform;
            //Debug.Log("exchange[1]为：" + exchange[1]);
            TrySwapAnimals();  // 进行交换
            
            SpriteRenderer sr = YellowCheckManager.activeYellowCheck.GetComponent<SpriteRenderer>();
            sr.sortingOrder = -2;
            YellowCheckManager.activeYellowCheck = null;

        }
    }

    // 交换逻辑
    private static void TrySwapAnimals()
    {
        Transform animal1 = exchange[0];
        Transform animal2 = exchange[1];

        //Debug.Log("交换的两个物体为：" + animal1 + " 和 " + animal2);
        if (animal1 == null || animal2 == null || animal1 == animal2) return;

        // 获取物体的世界坐标
        Vector3 worldPos1 = animal1.position;
        Vector3 worldPos2 = animal2.position;

        // 计算水平和垂直距离差，并取绝对值
        float x = Mathf.Abs(worldPos1.x - worldPos2.x);
        float y = Mathf.Abs(worldPos1.y - worldPos2.y);

       //Debug.Log("打印坐标点1" + "   x坐标" + worldPos1.x + "   y坐标" + worldPos1.y);
        //Debug.Log("打印坐标点2" + "   x坐标" + worldPos2.x + "   y坐标" + worldPos2.y);
        //Debug.Log("坐标差值：" + x + "   " + y);

        // 检查 parent 影响
        //Debug.Log("物体1的父对象：" + animal1.parent);
        //Debug.Log("物体2的父对象：" + animal2.parent);

        // 允许浮点误差
        if (Mathf.Abs(y)<0.1f && Mathf.Abs(x - 210) < 0.1f)
        {
            SwapAnimals(animal1, animal2);
            //Debug.Log("完成交换：" + animal1.name + " ↔ " + animal2.name);
            ResetExchange();
        }
        else if (Mathf.Abs(x) < 0.1f && Mathf.Abs(y - 205) < 0.1f)
        {
            SwapAnimals(animal1, animal2);
            //Debug.Log("完成交换：" + animal1.name + " ↔ " + animal2.name);
            ResetExchange();
        }
        else
        {
            Debug.Log("方块不相邻，不能交换");
            ResetExchange();
        }
    }


    // 提取重置操作
    private static void ResetExchange()
    {
        exchange[0] = null;
        exchange[1] = null;
        highlightedAnimal = null; // 重置小黄框的记录
    }


    private static void SwapAnimals(Transform animal1, Transform animal2)
    {

        // 交换父对象
        Transform parent1 = animal1.parent;
        Transform parent2 = animal2.parent;
        animal1.parent = parent2;
        animal2.parent = parent1;

        // 交换位置
        Vector3 tempPos = animal1.position;
        animal1.position = animal2.position;
        animal2.position = tempPos;

        qiunumber = qiunumber - 1;

        ChangeAnimals changeAnimalsInstance = FindObjectOfType<ChangeAnimals>();
        if (changeAnimalsInstance != null)
        {
            changeAnimalsInstance.QiuText();  // ✅ 通过实例调用
        }
        else
        {
            Debug.LogError("找不到 ChangeAnimals 组件！");
        }


    }

    //void Update()
    //{
    //    myAnimal.XXRaycast();


    //}

}
