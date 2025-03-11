using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCheckManager : MonoBehaviour
{
    public static YellowCheckManager Instance { get; private set; }
    private GameObject yellowPrefab;
    private bool isToggling = false;
    public static GameObject activeYellowCheck = null; // 记录当前激活的小黄框

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
            Debug.LogError("未找到小黄框预制体，请检查路径！");
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
            sr.sortingOrder = -2;  // 默认隐藏
        }
    }

    bool HasYellowCheck(GameObject animal)
    {
        return animal.transform.Find("yellowcheck(Clone)") != null;
    }



    public void ToggleYellowCheck(GameObject animal)
    {
        if (animal == null) return;

        if (isToggling) return; // 防止短时间内重复点击
        isToggling = true;

        Transform yellowCheckTransform = animal.transform.Find("yellowcheck(Clone)");
        if (yellowCheckTransform != null)
        {
            SpriteRenderer sr = yellowCheckTransform.GetComponent<SpriteRenderer>();
            //Debug.Log("当前 sortingOrder: " + sr.sortingOrder);

            // 如果当前小黄框已经激活，并且 activeYellowCheck 不是这个对象，直接返回
            if (activeYellowCheck != null && activeYellowCheck != yellowCheckTransform.gameObject)
            {
                Debug.Log("已有其他小黄框激活，不能切换");
                StartCoroutine(ResetToggle()); // 依然需要延迟重置 isToggling
                return;
            }

            if (sr.sortingOrder == -2)
            {
                sr.sortingOrder = 2;  // 显示小黄框
                activeYellowCheck = yellowCheckTransform.gameObject; // 记录当前激活的小黄框
                //Debug.Log($"activeYellowCheck的动物：{activeYellowCheck}");
                ChangeAnimals.SetHighlightedAnimal(animal.transform);
                //Debug.Log($"已设置小黄框覆盖的动物：{animal.name}");
            }
            else
            {
                sr.sortingOrder = -2; // 隐藏小黄框
                activeYellowCheck = null; // 取消激活状态
            }
        }

        StartCoroutine(ResetToggle());
    }

    private IEnumerator ResetToggle()
    {
        yield return new WaitForSeconds(0.5f); // 0.5 秒防止重复点击
        isToggling = false;
    }

}
