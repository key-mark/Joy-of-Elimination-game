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
            Debug.LogError("δ�ҵ�С�ƿ�Ԥ���壬����·����");
            return;
        }

        // �������б�ǩΪ "1" �� "6" �Ķ���
        for (int i = 1; i <= 6; i++)
        {
            string tag = i.ToString();  // ��ǩת��Ϊ�ַ���
            GameObject[] animals = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject animal in animals)
            {
                if (!HasYellowCheck(animal))  // **����Ƿ�����С�ƿ�**
                {
                    AttachYellowCheck(animal);
                }
            }
        }
    }

    // **��鶯���Ƿ�����С�ƿ�**
    bool HasYellowCheck(GameObject animal)
    {
        return animal.transform.Find("yellowcheck(Clone)") != null;
    }

    void AttachYellowCheck(GameObject animal)
    {
        // ����С�ƿ򣬲�����Ϊ�ö�����Ӷ���
        GameObject yellowCheck = Instantiate(yellowPrefab, animal.transform);

        // ��С�ƿ����
        yellowCheck.transform.position = animal.transform.position;


        // ���� Sorting Layer �Ա�֤��ȷ��ʾ
        SpriteRenderer sr = yellowCheck.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder = -2;  // **Ĭ�ϲ���ʾ**
        }
    }
}
