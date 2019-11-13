using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesEvent : MonoBehaviour
{
    [SerializeField] private Transform wavePref;
    [SerializeField] private Transform wavePos;
    IEnumerator WaveCoroutine()
    {
        while (true)
        {
            Instantiate(wavePref, new Vector2(wavePos.position.x, wavePos.position.y), Quaternion.identity);
            yield return new WaitForSeconds(10);
        }
    }

}
