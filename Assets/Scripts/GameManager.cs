using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<Food> FoodChangedEvent;

    [SerializeField] CanvasController canvasController;
    [SerializeField] Spawner spawner;
    [SerializeField] private GameObject table;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject GetPointText;
    [SerializeField] private AudioManager audioManager;

    private Dictionary<string, Food> foodDict;
    private int lifes;

    private  int goalsCount = 3;
    private const int MaxFoodCount = 5;
    private const string CAMERA_WINPOS_ANIM = "CAMERA_WIN_POS";
    private const string CAMERA_STANDARTPOS_ANIM = "CAME_STANDART_POS";


    private void Start()
    {
        foodDict = new Dictionary<string, Food>();
        spawner.gameObject.SetActive(false);
    }



    public void FoodWasTaken(Food food)
    {       

        if (foodDict.ContainsKey(food.FoodDate.foodName) && foodDict[food.FoodDate.foodName].FoodDate.Count > 0)
        {
            foodDict[food.FoodDate.foodName].FoodDate.Count--;
            FoodChangedEvent?.Invoke(food);            
            CheckCount();
            GetPointText.SetActive(true);
            audioManager.ChangeSFX(Sounds.GetPointSFX);
            return;
        }

        if (lifes > 0)
        {
            lifes--;
            canvasController.LifeTextChange(lifes);
            audioManager.ChangeSFX(Sounds.LostLifeSFX);
            if (lifes == 0)
            {
                LoseGame();
            }
        }
    }

    public void LoseGame()
    {
      EndGame();
        playerController.PlayerAnim(PlayerAnimation.LOSE_GAME_ANIM);        
        canvasController.LoseMenuPanel();
        audioManager.ChangeMusic(Sounds.LoseMusic);
    }

  
    public void WinGame()
    {
        EndGame();
        playerController.PlayerAnim(PlayerAnimation.WIN_GAME_ANIM);
        canvasController.WinMenuPanel();
        audioManager.ChangeMusic(Sounds.WinMusic);
    }
    private void EndGame()
    {
        spawner.gameObject.SetActive(false);
        table.SetActive(false);
        HideFoodsAfterGame();
        mainCamera.GetComponent<Animator>().SetTrigger(CAMERA_WINPOS_ANIM);
    }



    private void CheckCount()
    {
        foreach (var item in foodDict.Values)
        {
            if (item.FoodDate.Count > 0) return;          
        }
        WinGame();
    }

  

    public void StartGame()
    {
        GameProp();
        canvasController.MenuPanel();  
    }

    public void RepeatGame()
    {
        GameProp();
        canvasController.HideMenu();
        playerController.PlayerAnim(PlayerAnimation.IDLE_ANIM);
        mainCamera.GetComponent<Animator>().SetTrigger(CAMERA_STANDARTPOS_ANIM);
        audioManager.ChangeMusic(Sounds.MainMusic);
    }


    private void GameGoalsGenerator()
    {
        int i = 0;
        while (i != goalsCount)
        {
            int randomFoodIndex = UnityEngine.Random.Range(0, spawner.Foods.Length);
            int randomCount = UnityEngine.Random.Range(1, MaxFoodCount);
            Food randomFood = spawner.Foods[randomFoodIndex];

            if (foodDict.ContainsKey(randomFood.FoodDate.foodName))
                continue;

            randomFood.FoodDate.Count = randomCount;
            foodDict.Add(randomFood.FoodDate.foodName, randomFood);
            canvasController.SetGUI(i, randomFood);
            i++;
        }

        canvasController.LifeTextChange(lifes);
    }

    private void HideFoodsAfterGame()
    {
        Food[] foodsInGame = FindObjectsOfType<Food>();
        foreach (Food fo in foodsInGame)
        {
            fo.ReleaseObject();
        }
    }

    private void GameProp()
    {
        lifes = 3;
        canvasController.LifeTextChange(lifes);
        spawner.gameObject.SetActive(true);
        GameGoalsGenerator();
        table.SetActive(true);
    }
}
