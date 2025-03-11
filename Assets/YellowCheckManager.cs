using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCheckManager : MonoBehaviour
{
    public static YellowCheckManager Instance { get; private set; }
    private GameObject yellowPrefab;
    private bool isToggling = false;
    public static GameObject activeYellowCheck = null; // ��¼��ǰ�����С�ƿ�

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        yellowPrefab = Resources.Load<GameObject>("Prefabs/yellowcheck");
        if (yellowPrefab == null)
        {
            Debug.LogError("δ�ҵ�С�ƿ�Ԥ���壬����·����");
            return;
        }
    }

    public void AttachYellowCheck(GameObject animal)
    {
        if (animal == null || HasYellowCheck(animal)) return;

        GameObject yellowCheck = Instantiate(yellowPrefab, animal.transform);
        yellowCheck.transform.localPosition = Vector3.zero;

        SpriteRenderer sr = yellowCheck.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder = -2;  // Ĭ������
        }
    }

    bool HasYellowCheck(GameObject animal)
    {
        return animal.transform.Find("yellowcheck(Clone)") != null;
    }



    public void ToggleYellowCheck(GameObject animal)
    {
        if (animal == null) return;

        if (isToggling) return; // ��ֹ��ʱ�����ظ����
        isToggling = true;

        Transform yellowCheckTransform = animal.transform.Find("yellowcheck(Clone)");
        if (yellowCheckTransform != null)
        {
            SpriteRenderer sr = yellowCheckTransform.GetComponent<SpriteRenderer>();
            //Debug.Log("��ǰ sortingOrder: " + sr.sortingOrder);

            // �����ǰС�ƿ��Ѿ�������� activeYellowCheck �����������ֱ�ӷ���
            if (activeYellowCheck != null && activeYellowCheck != yellowCheckTransform.gameObject)
            {
                Debug.Log("��������С�ƿ򼤻�����л�");
                StartCoroutine(ResetToggle()); // ��Ȼ��Ҫ�ӳ����� isToggling
                return;
            }

            if (sr.sortingOrder == -2)
            {
                sr.sortingOrder = 2;  // ��ʾС�ƿ�
                activeYellowCheck = yellowCheckTransform.gameObject; // ��¼��ǰ�����С�ƿ�
                //Debug.Log($"activeYellowCheck�Ķ��{activeYellowCheck}");
                ChangeAnimals.SetHighlightedAnimal(animal.transform);
                //Debug.Log($"������С�ƿ򸲸ǵĶ��{animal.name}");
            }
            else
            {
                sr.sortingOrder = -2; // ����С�ƿ�
                activeYellowCheck = null; // ȡ������״̬
            }
        }

        StartCoroutine(ResetToggle());
    }

    private IEnumerator ResetToggle()
    {
        yield return new WaitForSeconds(0.5f); // 0.5 ���ֹ�ظ����
        isToggling = false;
    }

}
