using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
	public float runSpeed = 100f;
	public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	private int attackIndex = 0;
	private List<string> attacks = new List<string>{"attack1", "attack2", "attack3"};
	private int limit = 2;
	private AudioSource hit1;
    private AudioSource hit2;
    private AudioSource hit3;
    private int hitCount;
	private CameraShake cameraShake;
	private AudioSource atk1;
	private AudioSource atk2;
	private AudioSource atk3;
	private float attackCooldown = 0.5f; // Tempo de espera entre os ataques
    private float lastAttackTime = 0f; // Tempo do último ataque

	void Start()
	{
		cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
		hitCount = 0;

		if (PlayerCharacter.GetCurrentCharacter() == "Hiromasa")
		{
			atk1 = GameObject.Find("HiromasaAttack1").GetComponent<AudioSource>();
			atk2 = GameObject.Find("HiromasaAttack2").GetComponent<AudioSource>();
		}
		else if (PlayerCharacter.GetCurrentCharacter() == "Aldrich")
		{
			atk1 = GameObject.Find("AldrichAttack1").GetComponent<AudioSource>();
			atk2 = GameObject.Find("AldrichAttack2").GetComponent<AudioSource>();
		}
		else if (PlayerCharacter.GetCurrentCharacter() == "Stigandr")
		{
			atk1 = GameObject.Find("StigandrAttack1").GetComponent<AudioSource>();
			atk2 = GameObject.Find("StigandrAttack2").GetComponent<AudioSource>();
			atk3 = GameObject.Find("StigandrAttack3").GetComponent<AudioSource>();
		}

        hit1 = GameObject.Find("Hit1").GetComponent<AudioSource>();
        hit2 = GameObject.Find("Hit2").GetComponent<AudioSource>();
        hit3 = GameObject.Find("Hit3").GetComponent<AudioSource>();

		if (PlayerCharacter.GetCurrentCharacter() == "Stigandr")
			limit = 3;
		else
			limit = 2;
	}
	
	// Update is called once per frame
	void Update () {
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
			jump = true;

		if (Input.GetButtonDown("Fire1"))
		{
			if (Time.time - lastAttackTime >= attackCooldown)
            {
				attackIndex++;
				animator.SetBool(attacks[attackIndex % limit], true);
                // Inicia o ataque
                lastAttackTime = Time.time; // Atualiza o tempo do último ataque
				if (attackIndex == 1)
				{
					atk1.Play();
				}
				else if (attackIndex == 2)  
				{
					atk2.Play();
				}
				else if (attackIndex == 3) 
				{
					atk3.Play();
				}
				Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
				foreach (Collider2D enemy in hitEnemies)
				{
					// get genemy GameObject
					hitCount++;
					if (hitCount == 1) 
					{
						//Debug.Log("Hit 1");
						StartCoroutine(cameraShake.Shake(.3f, .3f));
						hit1.Play();
					}
					else if (hitCount == 2)
					{
						//Debug.Log("Hit 2");
						StartCoroutine(cameraShake.Shake(.3f, .3f));
						hit2.Play();
					}
					else if (hitCount == 3)
					{
						//Debug.Log("Hit 3");
						StartCoroutine(cameraShake.Shake(.3f, .3f));
						hit3.Play();
						hitCount = 0;
					}
					enemy.GetComponent<Animator>().SetTrigger("tookHit");
					if (enemy.gameObject.name == "Enemy")
					{
						MobGFX mobGFX = enemy.GetComponent<MobGFX>();
						//Debug.Log("Enemy hp: " + mobGFX.mob.hp);
						if (mobGFX != null)
						{
							mobGFX.mob.hp -= PlayerCharacter.CalculateDamage();
							//Debug.Log("Enemy hp: " + mobGFX.mob.hp);
							if (mobGFX.mob.hp <= 0)
							{
								EnemySpawner.activeEnemyPrefabs.Remove(enemy.gameObject);
								mobGFX.kill();
							}
						}
					}
					else
					{
						List<GameObject> enemiesToRemove = new List<GameObject>();
						foreach (GameObject activeEnemy in EnemySpawner.activeEnemyPrefabs)
						{
							if (activeEnemy.name == enemy.name)
							{
								MobGFX mobGFX = activeEnemy.GetComponent<MobGFX>();
								if (mobGFX != null)
								{
									mobGFX.mob.hp -= PlayerCharacter.CalculateDamage();
									//Debug.Log("Enemy hp: " + mobGFX.mob.hp);
									if (mobGFX.mob.hp <= 0)
									{
										//Debug.Log("DIED");
										enemiesToRemove.Add(activeEnemy);
										EnemySpawner.mobCnt--;
										mobGFX.kill();
									}
								}
							}
						}
						foreach (GameObject enemyToRemove in enemiesToRemove)
						{
							EnemySpawner.activeEnemyPrefabs.Remove(enemyToRemove);
						}
					}
				}
			}
		}
		else
			animator.SetBool(attacks[attackIndex % limit], false);

	}

	void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
		attackIndex = attackIndex % limit;
	}
}
