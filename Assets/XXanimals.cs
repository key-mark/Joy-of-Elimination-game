using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
public class XXanimals : MonoBehaviour
{
    // ˮƽ��������
    public List<RaycastHit2D> horizontal;
    // ��ֱ��������
    public List<RaycastHit2D> vertical;

    // �����ж������Ƿ����ı�־������Inspector���������
    public bool IsRaycast = true;

    // ����״̬ö��
    public enum State { Move, Idle }
    public State sta = State.Idle;

    private bool hasLoggedHorizontal = false;
    private bool hasLoggedVertical = false;
    public int speed;
    public GameObject stars;
    public GameObject passpanel;  // �����������������ʼ��

    void Awake()
    {
        passpanel = GameObject.Find("PassPanel");  // �� Awake �л�ȡ����
        stars = GameObject.Find("Stars");
    }


    void OnEnable()
    {
        speed = 1500;
        horizontal = new List<RaycastHit2D>();
        vertical = new List<RaycastHit2D>();
        sta = State.Idle; // ����������ʱ��ʼ��״̬
        Debug.Log(gameObject.name + " ״̬��ʼ��Ϊ: " + sta);
    }

    void OnDisable()
    {
        if (this.gameObject.tag == "6")
        {
            PassPanel panel = passpanel.GetComponent<PassPanel>(); // ��ȡ PassPanel ���
            if (panel == null)
            {
                Debug.Log("panel������");
                return; // ��������ô���
            }
            Debug.Log("panel����");
            panel.number2++; // ������ +1
            panel.PassText(); // ���� PassText()

            if (panel.number2 >= 6)
            {
                panel.photo2.transform.gameObject.SetActive(true); // ��ʾ photo2

                // ��ȡ TextMeshPro ������޸���ɫ
                TextMeshProUGUI tmp1 = panel.test2.GetComponent<TextMeshProUGUI>();
                if (tmp1 != null) tmp1.color = Color.green;

                TextMeshProUGUI tmp2 = panel.test22.GetComponent<TextMeshProUGUI>();
                if (tmp2 != null) tmp2.color = Color.green;

                //panel.Isshow2 = true;
            }
        }
        if (this.gameObject.tag == "3")
        {
            PassPanel panel = passpanel.GetComponent<PassPanel>(); // ��ȡ PassPanel ���
            if (panel == null)
            {
                Debug.Log("panel������");
                return; // ��������ô���
            }
            Debug.Log("panel����");
            panel.number4++; // ������ +1
            panel.PassText(); // ���� PassText()

            if (panel.number4 >= 10)
            {
                panel.photo4.transform.gameObject.SetActive(true); // ��ʾ photo2

                // ��ȡ TextMeshPro ������޸���ɫ
                TextMeshProUGUI tmp1 = panel.test4.GetComponent<TextMeshProUGUI>();
                if (tmp1 != null) tmp1.color = Color.green;

                TextMeshProUGUI tmp2 = panel.test44.GetComponent<TextMeshProUGUI>();
                if (tmp2 != null) tmp2.color = Color.green;

                //panel.Isshow2 = true;
            }
        }
        if (this.gameObject.tag == "4")
        {
            PassPanel panel = passpanel.GetComponent<PassPanel>(); // ��ȡ PassPanel ���
            if (panel == null)
            {
                Debug.Log("panel������");
                return; // ��������ô���
            }
            Debug.Log("panel����");
            panel.number5++; // ������ +1
            panel.PassText(); // ���� PassText()

            if (panel.number5 >= 9)
            {
                panel.photo5.transform.gameObject.SetActive(true); // ��ʾ photo2

                // ��ȡ TextMeshPro ������޸���ɫ
                TextMeshProUGUI tmp1 = panel.test5.GetComponent<TextMeshProUGUI>();
                if (tmp1 != null) tmp1.color = Color.green;

                TextMeshProUGUI tmp2 = panel.test55.GetComponent<TextMeshProUGUI>();
                if (tmp2 != null) tmp2.color = Color.green;

                //panel.Isshow2 = true;
            }
        }


    }
    //�����ƶ���stars������
    public void AnimalMovement()
    {
        IsRaycast = false;
        transform.position = Vector3.MoveTowards(transform.position, stars.transform.position, speed * Time.deltaTime);
        if ((transform.position - stars.transform.position).sqrMagnitude < 0.1f)
        {
            Destroy(gameObject);
        }
    }
    // ��ղ������б�
    public void AddList(List<RaycastHit2D> list, RaycastHit2D[] array1, RaycastHit2D[] array2)
    {
        list.Clear(); // ����б���ֹ���ݶѻ�
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

    // ����Ƿ��Ѵ������б���
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

    // ���˵����߼�⵽��������
    RaycastHit2D[] FilterHits(RaycastHit2D[] hits)
    {
        List<RaycastHit2D> validHits = new List<RaycastHit2D>();
        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject) // ������������
            {
                validHits.Add(hit);
            }
        }
        return validHits.ToArray();
    }

    // ���߼��
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
                Debug.Log(gameObject + " ��⵽��������ʼ����");

                List<GameObject> objectsToDestroy = new List<GameObject>();

                // ���ˮƽ�ʹ�ֱ���������
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

                // ����ǰ������ӵ������б�
                objectsToDestroy.Add(gameObject);


                // �����������ٷ���
                ChangeStateToMove(objectsToDestroy);

            }
        }
    }


    // �ж��Ƿ���������� tag �뵱ǰ������ͬ
    public bool FF(List<RaycastHit2D> all)
    {
        // ����б��ȣ�ȷ�������� 3 ������
        if (all.Count < 2)
        {
            return false;
        }

        // ��������ǩ�Ƿ��뵱ǰ����ƥ��
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
        InvokeRepeating(nameof(XXRaycast), 1f, 0.2f); // ÿ 0.2s ���һ��
    }

    void Update()
    {
        if(sta == State.Move)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(GetComponent<BoxCollider2D>());
            Invoke("AnimalMovement", 0.2f);
        }
        // ����������Ϊ "Check" ������
        GameObject[] checkObjects = GameObject.FindGameObjectsWithTag("Check");

        foreach (var checkObject in checkObjects)
        {
            // ��ȡ Check �ű�
            Check checkComponent = checkObject.GetComponent<Check>();
            if (checkComponent != null)
            {
                // ���û�������壬�������ɺ��������ӳ�����
                if (checkObject.transform.childCount == 0)
                {
                    StartCoroutine(DelayedCreateAnimal(checkComponent)); // �ӳ������¶���
                }
            }
        }
    }

    // �ӳ����ɶ����Э�̺���
    private IEnumerator DelayedCreateAnimal(Check checkComponent)
    {
        // �ӳ�һ����ʱ�䣨���� 2 �룩
        yield return new WaitForSeconds(1f); // �ӳ� 2 ��

        // �������ɶ���ĺ���
        checkComponent.CreateAnimalIfNeeded();
    }
}
