using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Image foodSprite;
    public TextMeshProUGUI textNum;

    public void Init(Sprite spr, int num)
    {
        foodSprite.sprite = spr;
        textNum.text = num.ToString();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
