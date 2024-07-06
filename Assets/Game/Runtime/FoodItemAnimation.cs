using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class FoodItemAnimation : MonoBehaviour
    {
        public int i = 0;
        // Start is called before the first frame update
        void Start()
        {
        }

        void Awake()
        {
            i = 0;
        }

        // Update is called once per frame
        void Update()
        {
            i++;
            if (i == 2)
            {
                gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f),0.3f);
                gameObject.transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.1f);
            }
        }
    }

}
