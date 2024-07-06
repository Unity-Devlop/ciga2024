using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

namespace Game
{
    [CreateAssetMenu(fileName = "FoodItem", menuName = "Data/FoodItem")]
    public class FoodItem: ScriptableObject
    {
        public Image foodSprite;
        public int myNum = 10000;
        public FoodType foodType;
    }    
}

