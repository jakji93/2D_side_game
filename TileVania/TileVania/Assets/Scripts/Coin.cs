using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip pickUpSFX;
    [SerializeField] int scorePerCoin = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        FindObjectOfType<GameSession>().AddToScore(scorePerCoin);
        AudioSource.PlayClipAtPoint(pickUpSFX, Camera.main.transform.position);
    }
}
