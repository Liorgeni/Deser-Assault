using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 5f;
    [SerializeField] ParticleSystem crashVFX;


void OnTriggerEnter(Collider other)
{
        StartCrashSequence();
}




void StartCrashSequence ()
{
    crashVFX.Play();
    GetComponent<PlayerController>().enabled = false;
    GetComponent<MeshRenderer>().enabled = false;
    Invoke("RealoadLevel", levelLoadDelay);
    GetComponent<BoxCollider>().enabled = false;
    DisableMeshRenderer();


}

void RealoadLevel()
{
    int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIdx);
}

void DisableMeshRenderer()
{
    foreach (MeshRenderer meshInChild in GetComponentsInChildren<MeshRenderer>())
meshInChild.enabled = false;
 
foreach (Collider colliderInChild in GetComponentsInChildren<Collider>())
colliderInChild.enabled = false;
}

}
