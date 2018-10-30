using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public float health = 100;
    public GameObject projectile;
    public float projectileSpeed = 10f;
    public float shotsPerSeconds = 0.5f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationForExplosion = 1f;
    public int scoreValue = 150;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSoundVolume;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume;

    private ScoreKeeper scoreKeeper;


     void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    void Update()
    {
        float probability = Time.deltaTime * shotsPerSeconds ;
        if (Random.value < probability)
        {
            Fire();
        }
    }

    void Fire ()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -2, 0);
        GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            missile.Hit();
            health -= missile.GetDamage();
            if (health <= 0)
            {
                Die();
                scoreKeeper.Score(scoreValue);
            }
        }
    }

    private void Die ()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);          
        Destroy(explosion, durationForExplosion);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
    }
}
