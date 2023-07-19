using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Food[] foods;
    [SerializeField] private float spawnTimeRate;
    [SerializeField] private Vector3 spawmPos;
    private Dictionary<string, IObjectPool<Food>> foodPoolDict;
    private Dictionary<string, GameObject> containersDict;

    public Food[] Foods => foods;

    private float currentTime;
    private int foodsIndex = 0;

    private void Awake()
    {
        foodPoolDict = new Dictionary<string, IObjectPool<Food>>();
        containersDict = new Dictionary<string, GameObject>();
        currentTime = spawnTimeRate;


        while (foodsIndex != foods.Length) 
        {
            IObjectPool<Food> objectPool = new ObjectPool<Food>(CreatePoolObject, OnGetObjectFromPool, OnReturnedToPool, OnDestroyPoolObject);
            foodPoolDict.Add(foods[foodsIndex].name, objectPool);

            GameObject container = new($"{foods[foodsIndex].name}_Pool");
            containersDict.Add(foods[foodsIndex].name, container);

            foodsIndex++;
        }
    }

    private void Update()
    {
        if (currentTime<= 0)
        {
            SpawnFood(RandomFood());
            currentTime = spawnTimeRate;
        }

        currentTime -= Time.deltaTime;
    }

    private Food RandomFood()
    {
        int randomIndex = Random.Range(0, foods.Length);
        foodsIndex = randomIndex;
        return foods[randomIndex];
    }

    private void SpawnFood(Food tmpFood)
    {
        Food food = foodPoolDict[tmpFood.name].Get();
        food.transform.position = spawmPos;
        food.transform.SetParent(containersDict[tmpFood.name].transform);

    }

    private void OnDestroyPoolObject(Food food) => Destroy(food);
    private void OnReturnedToPool(Food food) => food.gameObject.SetActive(false);
    private void OnGetObjectFromPool(Food food) => food.gameObject.SetActive(true);
    private Food CreatePoolObject()
    {
        Food food = Instantiate(foods[foodsIndex]);
        food.Pool = foodPoolDict[foods[foodsIndex].name];
        return food;
    }
}
