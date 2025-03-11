using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public string prefabPath = "Prefabs/check";  // Ԥ����·��
    public int m_x = 6;  // ��
    public int m_y = 6;  // ��

    // �� Animals ��Ϊ class ����
    public class Animals
    {
        public GameObject pointobject;  // �洢ʵ�����Ķ���
        public int x;
        public int y;
    }


    void Start()
    {
        InitializeGrid();  // ��ʼ������
    }

    // ��ʼ������
    void InitializeGrid()
    {
        for (int j = 0; j < m_y; j++)
        {
            for (int i = 0; i < m_x; i++)
            {
                SpawnAnimal(i, j); // ��������
            }
        }
    }

    // ���ɶ���
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
            Debug.LogError("�޷�����Ԥ���壺" + prefabPath);
        }
    }
}
