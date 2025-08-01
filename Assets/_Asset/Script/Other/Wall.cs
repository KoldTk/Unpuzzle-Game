using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private int scoreGain;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        EventDispatcher<int>.Dispatch(Event.GainScore.ToString(), scoreGain);
    } 
}
