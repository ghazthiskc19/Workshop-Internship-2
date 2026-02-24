using System;
using UnityEngine;
	public class Enemy : MonoBehaviour
	{
		// ====== ENEMY MOVEMENT ========
		Rigidbody2D rigidbody2d;
		// buat variable speed dengan tipe data float
	
		// buat variable vertical dengan tipe data bool (boolean)

		// buat variable changeTime dengan tipe data float

		// buat variable timer dengan tipe data float

		// buat variable direction dengan tipe data int (integer)
		private bool broken = true;
		public bool isBroken { get { return broken; }}

		// ===== ANIMATION ========
		Animator animator;

		// ====== AUDIO ========
		AudioSource audioSource;
		public AudioClip fixedSound;
	
		// ====== PARTICLE EFFECTS ========
		public ParticleSystem smokeParticleEffect;
		public ParticleSystem fixedParticleEffect;

		// ====== BREAKING AND FIXING =======
		public event Action OnFixed;

		private void Awake()
		{
			Helpers.RecursiveLayerSet(transform, Helpers.EnemyLayer);
		}

		void Start ()
		{
			rigidbody2d = GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();
			audioSource = GetComponent<AudioSource>();
			// timer = changeTime;
		}
	
		void Update()
		{

		}

		void FixedUpdate()
		{
			if(!broken)
			{
				return;
			}
           
			Vector2 position = rigidbody2d.position;
			
			// if (vertical)
			// {
			// 	position.y = position.y + speed * direction * Time.deltaTime;
			// 	animator.SetFloat("Move X", 0);
			// 	animator.SetFloat("Move Y", direction);
			// }
			// else
			// {
			// 	position.x = position.x + speed * direction * Time.deltaTime;
			// 	animator.SetFloat("Move X", direction);
			// 	animator.SetFloat("Move Y", 0);
			// }
			
			rigidbody2d.MovePosition(position);
		}
		
		void OnCollisionEnter2D(Collision2D other)
		{
			PlayerController player = other.gameObject.GetComponent<PlayerController>();

			if (player != null)
			{
				player.ChangeHealth(-1);
			}
		}

		public void Fix()
		{
			broken = false;
			rigidbody2d.simulated = false;
			animator.SetTrigger("Fixed");

			// Audio
			audioSource.Stop();
			audioSource.PlayOneShot(fixedSound);

			//Particles
			smokeParticleEffect.Stop();
			Instantiate(fixedParticleEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);

			OnFixed?.Invoke();
		}
	}