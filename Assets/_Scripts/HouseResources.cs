using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseResources : SingletonMonoBehaviour<HouseResources>
{
    public int maxFood;
    public int maxWater;
    public int foodCount { get; private set; }
    public int waterCount { get; private set; }
    public List<GameObject> foodIcons = new List<GameObject>();
    public List<GameObject> waterIcons = new List<GameObject>();

    private void OnLevelWasLoaded(int level)
    {
        UpdateFoodUI();
        UpdateWaterUI();
    }

    protected override void Initialise()
    {
        foodCount = maxFood;
        waterCount = maxWater;
        UpdateFoodUI();
        UpdateWaterUI();
    }

    private void Start()
    {
        DayAndTimeSystem.instance.DayPassed += DayPassed;
    }

    public void DayPassed()
    {
        ConsumeWater();
        ConsumeFood();
    }

    public void ConsumeFood()
    {
        if (foodCount > 0)
        {
            foodCount = Mathf.Clamp(foodCount - 1, 0, maxFood);

            PlayerStateManager.instance.AddHunger(-0.25f);
            UpdateFoodUI();
        }
    }

    public void ConsumeWater()
    {
        if (waterCount > 0)
        {
            waterCount = Mathf.Clamp(waterCount - 1, 0, maxWater);
            PlayerStateManager.instance.AddThirst(-0.25f);
        }
    }

    public void UpdateFoodUI()
    {
        for (int i = 0; i < foodIcons.Count; i++)
        {
            if (i < foodCount)
                foodIcons[i].SetActive(true);
            else
                foodIcons[i].SetActive(false);
        }
    }

    public void UpdateWaterUI()
    {
        for (int i = 0; i < waterIcons.Count; i++)
        {
            if (i < foodCount)
                waterIcons[i].SetActive(true);
            else
                waterIcons[i].SetActive(false);
        }
    }

    public void AddFood(int amount)
    {
        foodCount = Mathf.Clamp(foodCount + amount, 0, maxFood);
        UpdateFoodUI();
    }

    public void AddWater(int amount)
    {
        waterCount = Mathf.Clamp(waterCount + amount, 0, maxWater);
    }
}