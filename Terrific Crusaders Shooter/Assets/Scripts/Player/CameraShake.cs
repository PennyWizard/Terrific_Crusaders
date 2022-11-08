using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraShake : MonoBehaviour
{
    public bool start;
    public float shakeDuration = 0.2f;
    public AnimationCurve curve;
    float strength;
    Quaternion startPosition;


    private void Start()
    {
        
        startPosition = transform.rotation;
    }
    void Update()
    {
        
        if (Input.GetButton("Shoot"))
        {
            
            StartCoroutine(Shaking());
        } 
    }
    public IEnumerator Shaking()
    {
        
        
        float elapsedTime = 0f;

        while(elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            strength = curve.Evaluate(elapsedTime/shakeDuration);
            transform.rotation = startPosition * Quaternion.Euler(Random.Range(0,strength), Random.Range(0, strength), Random.Range(0, strength));
            yield return null;
        }
        transform.rotation = startPosition;
    }
}
