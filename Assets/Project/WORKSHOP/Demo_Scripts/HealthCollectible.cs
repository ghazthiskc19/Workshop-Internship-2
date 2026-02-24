using UnityEngine;

    public class HealthCollectible : MonoBehaviour
    {
        public AudioClip collectedClip;
        public ParticleSystem pickUpParticleEffect;
        void OnCollisionEnter2D(Collision2D other)
        {
            PlayerController controller = other.gameObject.GetComponent<PlayerController>();

            if (controller != null && controller.health < controller.maxHealth)
            {
                Instantiate(pickUpParticleEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);
                controller.PlaySound(collectedClip);
                controller.ChangeHealth(0);
                Destroy(gameObject);
            }
        }
    }
