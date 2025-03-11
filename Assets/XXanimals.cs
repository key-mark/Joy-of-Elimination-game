using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
public class XXanimals : MonoBehaviour
{
    // 水平动物数组
    public List<RaycastHit2D> horizontal;
    // 垂直动物数组
    public List<RaycastHit2D> vertical;

    // 用于判断射线是否开启的标志，可在Inspector面板中设置
    public bool IsRaycast = true;

    // 动物状态枚举
    public enum State { Move, Idle }
    public State sta = State.Idle;

    private bool hasLoggedHorizontal = false;
    private bool hasLoggedVertical = false;
    public int speed;
    public GameObject stars;
    public GameObject passpanel;  // 仅声明，不在这里初始化

    void Awake()
    {
        passpanel = GameObject.Find("PassPanel");  // 在 Awake 中获取对象
        stars = GameObject.Find("Stars");
    }


    void OnEnable()
    {
        speed = 1500;
        horizontal = new List<RaycastHit2D>();
        vertical = new List<RaycastHit2D>();
        sta = State.Idle; // 在物体启用时初始化状态
        Debug.Log(gameObject.name + " 状态初始化为: " + sta);
    }

    void OnDisable()
    {
        if (this.gameObject.tag == "6")
        {
            PassPanel panel = passpanel.GetComponent<PassPanel>(); // 获取 PassPanel 组件
            if (panel == null)
            {
                Debug.Log("panel不存在");
                return; // 避免空引用错误
            }
            Debug.Log("panel存在");
            panel.number2++; // 计数器 +1
            panel.PassText(); // 调用 PassText()

            if (panel.number2 >= 6)
            {
                panel.photo2.transform.gameObject.SetActive(true); // 显示 photo2

                // 获取 TextMeshPro 组件并修改颜色
                TextMeshProUGUI tmp1 = panel.test2.GetComponent<TextMeshProUGUI>();
                if (tmp1 != null) tmp1.color = Color.green;

                TextMeshProUGUI tmp2 = panel.test22.GetComponent<TextMeshProUGUI>();
                if (tmp2 != null) tmp2.color = Color.green;

                //panel.Isshow2 = true;
            }
        }
        if (this.gameObject.tag == "3")
        {
            PassPanel panel = passpanel.GetComponent<PassPanel>(); // 获取 PassPanel 组件
            if (panel == null)
            {
                Debug.Log("panel不存在");
                return; // 避免空引用错误
            }
            Debug.Log("panel存在");
            panel.number4++; // 计数器 +1
            panel.PassText(); // 调用 PassText()

            if (panel.number4 >= 10)
            {
                panel.photo4.transform.gameObject.SetActive(true); // 显示 photo2

                // 获取 TextMeshPro 组件并修改颜色
                TextMeshProUGUI tmp1 = panel.test4.GetComponent<TextMeshProUGUI>();
                if (tmp1 != null) tmp1.color = Color.green;

                TextMeshProUGUI tmp2 = panel.test44.GetComponent<TextMeshProUGUI>();
                if (tmp2 != null) tmp2.color = Color.green;

                //panel.Isshow2 = true;
            }
        }
        if (this.gameObject.tag == "4")
        {
            PassPanel panel = passpanel.GetComponent<PassPanel>(); // 获取 PassPanel 组件
            if (panel == null)
            {
                Debug.Log("panel不存在");
                return; // 避免空引用错误
            }
            Debug.Log("panel存在");
            panel.number5++; // 计数器 +1
            panel.PassText(); // 调用 PassText()

            if (panel.number5 >= 9)
            {
                panel.photo5.transform.gameObject.SetActive(true); // 显示 photo2

                // 获取 TextMeshPro 组件并修改颜色
                TextMeshProUGUI tmp1 = panel.test5.GetComponent<TextMeshProUGUI>();
                if (tmp1 != null) tmp1.color = Color.green;

                TextMeshProUGUI tmp2 = panel.test55.GetComponent<TextMeshProUGUI>();
                if (tmp2 != null) tmp2.color = Color.green;

                //panel.Isshow2 = true;
            }
        }


    }
    //物体移动到stars处消除
    public void AnimalMovement()
    {
        IsRaycast = false;
        transform.position = Vector3.MoveTowards(transform.position, stars.transform.position, speed * Time.deltaTime);
        if ((transform.position - stars.transform.position).sqrMagnitude < 0.1f)
        {
            Destroy(gameObject);
        }
    }
    // 清空并更新列表
    public void AddList(List<RaycastHit2D> list, RaycastHit2D[] array1, RaycastHit2D[] array2)
    {
        list.Clear(); // 清空列表，防止数据堆积
        foreach (var a in array1)
        {
            if (check(list, a))
            {
                list.Add(a);
            }
        }
        foreach (var b in array2)
        {
            if (check(list, b))
            {
                list.Add(b);
            }
        }
    }

    // 检查是否已存在于列表中
    public bool check(List<RaycastHit2D> list, RaycastHit2D obj)
    {
        foreach (var item in list)
        {
            if (item.transform.gameObject == obj.transform.gameObject)
            {
                return false;
            }
        }
        return true;
    }

    // 过滤掉射线检测到自身物体
    RaycastHit2D[] FilterHits(RaycastHit2D[] hits)
    {
        List<RaycastHit2D> validHits = new List<RaycastHit2D>();
        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject) // 过滤自身物体
            {
                validHits.Add(hit);
            }
        }
        return validHits.ToArray();
    }

    // 射线检测
    public void XXRaycast()
    {
        if (IsRaycast)
        {
            var left = FilterHits(Physics2D.RaycastAll(transform.position, Vector3.left, 200));
            var right = FilterHits(Physics2D.RaycastAll(transform.position, Vector3.right, 200));
            var up = FilterHits(Physics2D.RaycastAll(transform.position, Vector3.up, 200));
            var down = FilterHits(Physics2D.RaycastAll(transform.position, Vector3.down, 200));

            AddList(horizontal, left, right);
            AddList(vertical, up, down);

            if (FF(horizontal) || FF(vertical))
            {
                Debug.Log(gameObject + " 检测到三消，开始销毁");

                List<GameObject> objectsToDestroy = new List<GameObject>();

                // 添加水平和垂直方向的物体
                if (FF(horizontal))
                {
                    foreach (var item in horizontal)
                    {
                        objectsToDestroy.Add(item.transform.gameObject);
                    }
                }

                if (FF(vertical))
                {
                    foreach (var item in vertical)
                    {
                        objectsToDestroy.Add(item.transform.gameObject);
                    }
                }

                // 将当前物体添加到销毁列表
                objectsToDestroy.Add(gameObject);


                // 调用批量销毁方法
                ChangeStateToMove(objectsToDestroy);

            }
        }
    }


    // 判断是否所有物体的 tag 与当前物体相同
    public bool FF(List<RaycastHit2D> all)
    {
        // 检查列表长度，确保至少有 3 个物体
        if (all.Count < 2)
        {
            return false;
        }

        // 检查物体标签是否与当前物体匹配
        for (int i = 0; i < all.Count; i++)
        {
            if (all[i].transform.tag != gameObject.tag)
            {
                return false;
            }
        }

        return true;
    }

    public void ChangeStateToMove(List<GameObject> objectsToDestroy)
    {
        foreach (var obj in objectsToDestroy)
        {
            XXanimals animal = obj.GetComponent<XXanimals>();
            if (animal != null)
            {
                animal.sta = State.Move;
            }
        }
    }




    void Start()
    {
        InvokeRepeating(nameof(XXRaycast), 1f, 0.2f); // 每 0.2s 检测一次
    }

    void Update()
    {
        if(sta == State.Move)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(GetComponent<BoxCollider2D>());
            Invoke("AnimalMovement", 0.2f);
        }
        // 遍历所有名为 "Check" 的物体
        GameObject[] checkObjects = GameObject.FindGameObjectsWithTag("Check");

        foreach (var checkObject in checkObjects)
        {
            // 获取 Check 脚本
            Check checkComponent = checkObject.GetComponent<Check>();
            if (checkComponent != null)
            {
                // 如果没有子物体，调用生成函数，并延迟生成
                if (checkObject.transform.childCount == 0)
                {
                    StartCoroutine(DelayedCreateAnimal(checkComponent)); // 延迟生成新动物
                }
            }
        }
    }

    // 延迟生成动物的协程函数
    private IEnumerator DelayedCreateAnimal(Check checkComponent)
    {
        // 延迟一定的时间（例如 2 秒）
        yield return new WaitForSeconds(1f); // 延迟 2 秒

        // 调用生成动物的函数
        checkComponent.CreateAnimalIfNeeded();
    }
}
