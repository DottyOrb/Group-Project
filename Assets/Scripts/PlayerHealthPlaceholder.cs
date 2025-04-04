using UnityEngine;

public class PlayerHealthPlaceholder : MonoBehaviour
{
    public static PlayerHealthPlaceholder instance;
    public int health = 100;
    int speed = 5;

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAttackHitbox"))
    //    {
    //        health--;
    //    }
    //}

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))

        {
            this.transform.position += Vector3.up * this.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += Vector3.down * this.speed * Time.deltaTime;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    public void DamagePlayer(int damage)
    {
        health -= damage;
    }
}
