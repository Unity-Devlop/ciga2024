using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class FoodItem : MonoBehaviour
    {
        // Start is called before the first frame update
        public Image foodSprite;
        public TextMeshProUGUI textNum;
        public int myNum = 11;
        public FoodType foodType;
        public Dictionary<FoodType, Sprite> foodObj = new Dictionary<FoodType, Sprite>();

        public void SetFoodType(FoodType a)
        {
            foodType = a;
        }

        public void Init(int num)
        {
            foodObj[foodType] = foodSprite.sprite;
            myNum = num;
            textNum.text = myNum.ToString();
            
            
            switch (foodType)
            {
                case FoodType.Hamburger :
                    foodSprite.sprite = foodObj[FoodType.Hamburger];
                    myNum = num;
                    textNum.text = myNum.ToString();
                    break;
                default:
                    break;
            }
        }
    
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }    
}

