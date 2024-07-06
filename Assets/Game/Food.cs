using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

public enum FoodType
{
    Hamburger,
}

public class Food : MonoBehaviour
{
    public int stomachIncrease;
    public int appetiteDecrease;
    public FoodType type;

    float time = 0;

    private void OnEnable()
    {
        time = Time.time;
    }

    private void Update()
    {
        if(Time.time-time>=2)
        {
            Disappear();
        }
    }

    public void BeClicked()
    {
        Disappear();
    }

    private void Disappear()
    {
        GameObjectPoolManager.Release(type, gameObject);
    }
}
