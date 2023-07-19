using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FoodOnTheScreen : MonoBehaviour
{
    public Food food;
    public Image image;
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        GameManager.FoodChangedEvent += FoodCountChange;
    }

    private void FoodCountChange(Food food)
    {
        if (this.food.FoodDate.name == food.FoodDate.name) text.text = food.FoodDate.Count.ToString();
    }

    public void SetFood(Food food)
    {
        this.food = food;
        SetProp();
    }

    private void SetProp()
    {
        image.sprite = food.FoodDate.foodSprite;
        text.text = food.FoodDate.Count.ToString();
    }

}
