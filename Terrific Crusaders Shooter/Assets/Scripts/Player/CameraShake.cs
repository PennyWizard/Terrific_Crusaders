using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool start;
    public float shakeDuration = 0.2f;
    public AnimationCurve curve;
    float strength;
    private void Start()
    {
        start = false;
    }
    void Update()
    {
        
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        } 
    }
    public IEnumerator Shaking()
    {
        Vector3 startPosition = GameManager.instance.player.transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            strength = curve.Evaluate(elapsedTime/shakeDuration);
            transform.position = startPosition + Random.insideUnitSphere*strength;
            yield return null;
        }
        transform.position = startPosition;
    }
}
