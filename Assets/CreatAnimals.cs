using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public string prefabPath = "Prefabs/check";  // 预设体路径
    public int m_x = 6;  // 列
    public int m_y = 6;  // 行

    // 将 Animals 改为 class 类型
    public class Animals
    {
        public GameObject pointobject;  // 存储实例化的动物
        public int x;
        public int y;
    }


    void Start()
    {
        InitializeGrid();  // 初始化网格
    }

    // 初始化网格
    void InitializeGrid()
    {
        for (int j = 0; j < m_y; j++)
        {
            for (int i = 0; i < m_x; i++)
            {
                SpawnAnimal(i, j); // 创建动物
            }
        }
    }

    // 生成动物
    public void SpawnAnimal(int i, int j)
    {
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        if (prefab != null)
        {
            GameObject check = Instantiate(prefab);
            check.transform.SetParent(transform);
            check.transform.localPosition = new Vector3(i * 210 - 521, j * 205 * (-1) + 500, 0);
        }
        else
        {
            Debug.LogError("无法加载预设体：" + prefabPath);
        }
    }
}
