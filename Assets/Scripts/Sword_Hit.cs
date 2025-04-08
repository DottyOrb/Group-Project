using UnityEngine;

public class Sword_Hit : MonoBehaviour
{
    #region Public Variables
    public AudioSource HitSource;
    public AudioClip HitClip;
    #endregion

    private void Start()
    {
        HitSource.clip = HitClip;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyMelee") || other.gameObject.layer == LayerMask.NameToLayer("EnemyRanged") || other.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile") || other.gameObject.layer == LayerMask.NameToLayer("Spawner"))
        {
            HitSource.Play();
        }
    }
}
