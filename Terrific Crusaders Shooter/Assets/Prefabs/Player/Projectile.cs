using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField] UnityEvent onCollideWith = new UnityEvent();
    [SerializeField] LayerMask collisionLayerMask = ~0;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionLayerMask == (collisionLayerMask | (collision.gameObject.layer)))
        {
            onCollideWith?.Invoke();

            
            Destroy(gameObject, 5f);
            
        }
    }

    public void MakeASound(float range)
    {
        var sound = new Sound(transform.position, range);
        sound.soundType = Sound.SoundType.Intersting;

        Sounds.MakeSound(sound);
    }

}
