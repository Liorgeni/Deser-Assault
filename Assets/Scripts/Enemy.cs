using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scoreHit = 15;
    [SerializeField] int enemyHitPoints = 10;

    ScoreBoard scoreBoard;
     GameObject parentGameobject;
     //Material enemyHit_Material;

    
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        //    enemyHit_Material = GetComponent<Renderer>().material;
        parentGameobject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbidy();
    }

     void AddRigidbidy()
    {
        Rigidbody rgbody = gameObject.AddComponent<Rigidbody>();
        rgbody.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
      if (enemyHitPoints < 1)
        {
        KillEnemy();
        }
    }

    private void ProcessHit()
    {

       // enemyHit_Material.color = Color.red;        
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameobject.transform;
        enemyHitPoints--;

    }

    private void KillEnemy()
    {
        scoreBoard.IncreaseScore(scoreHit);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameobject.transform;
        Destroy(gameObject);
    }

}
