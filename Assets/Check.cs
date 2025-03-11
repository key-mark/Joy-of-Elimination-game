using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    void Start()
    {
        // ȷ����ǰ�������ɶ���
        CreateAnimalIfNeeded();
    }

    public void CreateAnimalIfNeeded()
    {
        //Debug.Log("1111");
        // ���������û�ж���
        if (transform.childCount == 0)
        {
            GameObject sonAnimal = CreateAnimal();
            //Debug.Log("222");
            if (sonAnimal != null)
            {
                // ��Ϊ���ӵ��Ӷ���
                sonAnimal.transform.parent = transform;
                // �ö���λ������Ӷ���
                sonAnimal.transform.position = transform.position;
            }
            else
            {
                Debug.LogError($"���ﴴ��ʧ�ܣ�����: {gameObject.name}");
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
            Debug.LogError($"δ�ҵ� Prefabs/{num}������Ԥ����·����");
            return null;
        }
        //AddClickEvent(animalPrefab);
        StartCoroutine(AttachYellowCheckDelayed(newAnimal));
        return newAnimal;
    }


    // **ȷ��С�ƿ�����ȷ���ӣ����� Unity ����㼶��ʼ������**
    IEnumerator AttachYellowCheckDelayed(GameObject animal)
    {
        yield return new WaitForEndOfFrame(); // **�ȴ�һ֡��ȷ�������ʼ�����**

        if (animal != null)
        {
            YellowCheckManager.Instance.AttachYellowCheck(animal);
        }
    }

    // �Ƴ�ָ����������
    public void RemoveChildFromParent(GameObject target)
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject == target)
                {
                    // ǿ���Ƴ��������е�������
                    child.SetParent(null); // ����������Ϊ�޸�����
                    break;
                }
            }
        }
    }
}
