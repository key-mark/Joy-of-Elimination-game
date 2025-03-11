using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // 确保引入 TextMeshPro 命名空间

public class PassPanel : MonoBehaviour
{
    // Start is called before the first frame update

    public static PassPanel instance;
    

    public TextMeshProUGUI test2;
    public TextMeshProUGUI test4;
    public TextMeshProUGUI test5;
    public TextMeshProUGUI test22;
    public TextMeshProUGUI test44;
    public TextMeshProUGUI test55;

    public Image photo2;
    public Image photo4;
    public Image photo5;
    public int number2;
    public int number4;
    public int number5;

    public void PassText()
    {
        test22.text = number2.ToString();
        test44.text = number4.ToString();
        test55.text = number5.ToString();
    }

    
}
