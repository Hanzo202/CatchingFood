using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private List<FoodOnTheScreen> foodOnTheScreen;
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject LoseMenu;
    [SerializeField] private GameObject WinMenu;
    [SerializeField] private GameObject GUI;
    [SerializeField] private GameManager gameManager;

    public void SetGUI(int index, Food food)
    {
        foodOnTheScreen[index].SetFood(food);
    }

    public void LifeTextChange(int life)
    {
        lifesText.text = life.ToString();
    }

    public void MenuPanel()
    {
        menu.SetActive(false);
        GUI.SetActive(true);
    }

    public void LoseMenuPanel()
    {
        LoseMenu.SetActive(true);
        GUI.SetActive(false);
    }

    public void WinMenuPanel()
    {
        WinMenu.SetActive(true);
        GUI.SetActive(false);
    }

    public void HideMenu()
    {
        LoseMenu.SetActive(false);
        WinMenu.SetActive(false);
        GUI.SetActive(true);
    }
}
