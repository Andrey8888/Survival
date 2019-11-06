using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingEvent : MonoBehaviour
{
    [SerializeField] private Lighting lighting;
    IEnumerator LightingCoroutine()
    {
        while (true)
        {
            lighting.LightingStart();
            yield return new WaitForSeconds(5);
        }
    }
}
