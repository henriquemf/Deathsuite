using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class MobGFX : MonoBehaviour
{
    public struct Mob
    {
        public float hp;
        public int attack;
        public bool hasSpell;
        public bool isBoss;
        public int waitTime;
        public float attackRange;
        public float spellRange;
    }

    public AIPath aiPath;

    public Transform attackPoint;
    public float attackRange = 1.5f;
    public LayerMask enemyLayers;
    public Animator animator;
    // public Animator spellAnimator;
    public Rigidbody2D rb;
    private Rigidbody2D playerRb;
    public GameObject mobObj;

    private float distToPlayer;

    private string currentCharacter;
    private bool isAttacking = false;
    private bool isCasting = false;
    private Vector2 playerPos;
    public Mob mob;
    private CameraShake cameraShake;
    private AudioSource hurt;

    void Start()
    {
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        currentCharacter = PlayerCharacter.GetCurrentCharacter();

        if (currentCharacter == "Hiromasa")
        {
            playerRb = GameObject.Find("Hiromasa").GetComponent<Rigidbody2D>();
            hurt = GameObject.Find("HiromasaHurt").GetComponent<AudioSource>();

        }
        else if (currentCharacter == "Aldrich")
        {
            playerRb = GameObject.Find("Aldrich").GetComponent<Rigidbody2D>();
            hurt = GameObject.Find("AldrichHurt").GetComponent<AudioSource>();
        }
        else if (currentCharacter == "Stigandr")
        {
            playerRb = GameObject.Find("Stigandr").GetComponent<Rigidbody2D>();
            hurt = GameObject.Find("StigandrHurt").GetComponent<AudioSource>();
        }
        else
        {
            playerRb = GameObject.Find("Hiromasa").GetComponent<Rigidbody2D>();
        }

        mob = new Mob();
        switch (mobObj.tag)
        {
            case "ethern":
                mob.hp = 100;
                mob.attack = 10;
                mob.hasSpell = true;
                mob.isBoss = true;
                mob.waitTime = 2;
                mob.attackRange = 3.99f;
                mob.spellRange = 20f;
                break;
            case "golgrathen":
                mob.hp = 100;
                mob.attack = 30;
                mob.hasSpell = false;
                mob.isBoss = true;
                mob.waitTime = 1;
                mob.attackRange = 3.99f;
                mob.spellRange = 20f;
                break;
            case "ortrax":
                mob.hp = 100;
                mob.attack = 10;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "fireworm":
                mob.hp = 100;
                mob.attack = 10;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "mushroom":
                mob.hp = 100;
                mob.attack = 10;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "skeleton":
                mob.hp = 100;
                mob.attack = 10;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "goblin":
                mob.hp = 100;
                mob.attack = 10;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "bigswordknight":
                mob.hp = 100;
                mob.attack = 10;
                mob.hasSpell = false;
                mob.isBoss = true;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "swordknight":
                mob.hp = 100;
                mob.attack = 10;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "fastknight":
                mob.hp = 100;
                mob.attack = 10;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "bat":
                mob.hp = 100;
                mob.attack = 10;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            default:
                mob.hp = 100;
                mob.attack = 10;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;   
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        animate();
    }

    public void kill()
    {
        animator.SetTrigger("died");
        StartCoroutine(Death());
    }

    private void animate()
    {
        animator.SetBool("isWalking", aiPath.desiredVelocity != Vector3.zero);
        distToPlayer = Vector2.Distance(rb.position, playerRb.position);
        if (distToPlayer <= mob.attackRange && !isAttacking && !isCasting)
        {
            StartCoroutine(Attack());
        }

        if ((distToPlayer <= mob.spellRange && distToPlayer > mob.spellRange - 5) && !isCasting && !isAttacking && mob.hasSpell)
        {
            isCasting = true;
            aiPath.enabled = false;
            playerPos = playerRb.position;
            animator.SetTrigger("cast");
            StartCoroutine(Cast());
        }
    }

    private void castSpell()
    {
        // play the Spell animation on top of the playerPos
        // animator.SetTrigger("spell");
        Vector2 spellPos = new Vector2(playerPos.x, playerPos.y + 1f);
        Instantiate(Resources.Load("Spell"), spellPos, Quaternion.identity);

    }

    IEnumerator Attack()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;

        isAttacking = true;
        aiPath.enabled = false;
        animator.SetTrigger("attack1");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            StartCoroutine(cameraShake.Shake(.15f, .4f));
            hurt.Play();
            enemy.GetComponent<Animator>().SetTrigger("tookHit");
            Debug.Log("We hit " + enemy.name);
            PlayerCharacter.TakeHit(mob.attack);
        }
        yield return new WaitForSeconds(mob.waitTime);
        rb.bodyType = RigidbodyType2D.Dynamic;
        aiPath.enabled = true;
        isAttacking = false;
    }

    IEnumerator Cast()
    {
        yield return new WaitForSeconds(mob.waitTime);
        rb.bodyType = RigidbodyType2D.Dynamic;
        aiPath.enabled = true;
        isCasting = false;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
