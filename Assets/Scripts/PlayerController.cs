using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAnimation
{
    public static readonly string TAKE_FOOD_ANIM = "TAKE_FOOD";
    public static readonly string WIN_GAME_ANIM = "WIN_GAME";
    public static readonly string LOSE_GAME_ANIM = "LOSE_GAME";
    public static readonly string IDLE_ANIM = "IDLE";
}

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Transform handIKTarget;
    [SerializeField] private Transform handBone;
    [SerializeField] private float ZOffset;
    [SerializeField] private float YOffset;
    [SerializeField] private GameObject box;
    [SerializeField] private GameManager gameManager;

    private Animator animator;
    private Food foodInHand;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeFood(Food food)
    {
        foodInHand = food;
        handIKTarget.position = new Vector3(foodInHand.transform.position.x, foodInHand.transform.position.y + YOffset, foodInHand.transform.position.z + ZOffset);
        animator.SetTrigger(PlayerAnimation.TAKE_FOOD_ANIM);
    }

    private void OnAnimationGrabbedItem()
    {
        foodInHand.transform.position = handBone.transform.position;
        foodInHand.transform.SetParent(handBone, true);
    }

    private void OnAnimationStoreddItem()
    {
        foodInHand.transform.SetParent(null);
        foodInHand.transform.SetParent(box.transform, true);
        gameManager.FoodWasTaken(foodInHand);
    }

   public void PlayerAnim(string anim)
    {
        animator.SetTrigger(anim);
    }
}
