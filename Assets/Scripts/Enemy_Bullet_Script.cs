using UnityEngine;

public class Enemy_Bullet_Script : MonoBehaviour
{
    #region Public Variables
    public float force;
    #endregion

    #region Private Variables
    private Rigidbody2D rb;
    [SerializeField] private float lifeTime = 3f;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.right * force;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
