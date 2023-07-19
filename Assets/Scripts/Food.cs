using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class Food : MonoBehaviour
{
    [SerializeField] private FoodDate foodDate;
    [SerializeField] private float speed;

    private PlayerController playerController;
    public FoodDate FoodDate => foodDate;
    private bool isGrab = false;


    private Rigidbody rb;
    public IObjectPool<Food> Pool { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (!isGrab) rb.velocity = Vector3.left * speed * Time.deltaTime;
    }

    public  void ReleaseObject()
    {
        Pool.Release(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        ReleaseObject();
    }

    private void OnMouseDown()
    {
        isGrab = true;
        rb.velocity = Vector3.zero;
        playerController.TakeFood(this);
    }
}
