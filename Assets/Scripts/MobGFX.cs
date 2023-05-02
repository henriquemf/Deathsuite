using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class MobGFX : MonoBehaviour
{
    public struct Mob
    {
        public float hp;
        public int atkCnt;
        public int attack;
        public bool hasSpell;
        public bool hasAttack;
        public bool isBoss;
        public int waitTime;
        public float attackRange;
        public float spellRange;
    }

    public AIPath aiPath;
    public GameObject prefabToSpawn;
    public Transform attackPoint;
    public float attackRange = 1.5f;
    public LayerMask enemyLayers;
    public Animator animator;
    public Rigidbody2D rb;
    private Rigidbody2D playerRb;
    public GameObject mobObj;

    private float distToPlayer;
    private Vector2 spellPos;
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

        mob = new Mob();
        switch (mobObj.tag)
        {
            case "ethern":
                mob.hp = 1200;
                mob.atkCnt = 1;
                mob.hasAttack = true;
                mob.attack = 40;
                mob.hasSpell = true;
                mob.isBoss = true;
                mob.waitTime = 2;
                mob.attackRange = 3.99f;
                mob.spellRange = 20f;
                EnemySpawner.activeEnemyPrefabs.Add(mobObj);
                break;
            case "golgrathen":
                mob.hp = 1000;
                mob.atkCnt = 1;
                mob.hasAttack = true;
                mob.attack = 30;
                mob.hasSpell = false;
                mob.isBoss = true;
                mob.waitTime = 1;
                mob.attackRange = 3.99f;
                mob.spellRange = 20f;
                EnemySpawner.activeEnemyPrefabs.Add(mobObj);
                break;
            case "ortrax":
                mob.hp = 1500;
                mob.atkCnt = 2;
                mob.hasAttack = true;
                mob.attack = 50;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                EnemySpawner.activeEnemyPrefabs.Add(mobObj);
                break;
            case "fireworm":
                mob.hp = 150;
                mob.atkCnt = 1;
                mob.hasAttack = false;
                mob.attack = 20;
                mob.hasSpell = true;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 10f;
                break;
            case "mushroom":
                mob.hp = 100;
                mob.atkCnt = 2;
                mob.hasAttack = true;
                mob.attack = 10;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "skeleton":
                mob.hp = 150;
                mob.atkCnt = 3;
                mob.hasAttack = true;
                mob.attack = 20;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "goblin":
                mob.hp = 100;
                mob.atkCnt = 3;
                mob.hasAttack = true;
                mob.attack = 10;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "bigswordknight":
                mob.hp = 250;
                mob.atkCnt = 3;
                mob.hasAttack = true;
                mob.attack = 30;
                mob.hasSpell = false;
                mob.isBoss = true;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "swordknight":
                mob.hp = 250;
                mob.atkCnt = 3;
                mob.hasAttack = true;
                mob.attack = 30;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "fastknight":
                mob.hp = 250;
                mob.atkCnt = 2;
                mob.hasAttack = true;
                mob.attack = 30;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            case "bat":
                mob.hp = 150;
                mob.atkCnt = 3;
                mob.attack = 20;
                mob.hasAttack = true;
                mob.hasSpell = false;
                mob.isBoss = false;
                mob.waitTime = 2;
                mob.attackRange = 3.9f;
                mob.spellRange = 20f;
                break;
            default:
                mob.hp = 100;
                mob.atkCnt = 1;
                mob.attack = 10;
                mob.hasSpell = false;
                mob.hasAttack = true;
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
        Debug.Log("OE1");
        aiPath.enabled = true;
    }

    public void kill()
    {
        aiPath.enabled = false;
        animator.SetTrigger("died");
        StartCoroutine(Death());
    }

    private void animate()
    {
        animator.SetBool("isWalking", aiPath.desiredVelocity != Vector3.zero);
        distToPlayer = Vector2.Distance(rb.position, playerRb.position);
        Debug.Log("OE2");
        if (distToPlayer <= mob.attackRange && !isAttacking && !isCasting && mob.hasAttack)
        {
            Debug.Log("OE3");
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
        GameObject go = Instantiate(prefabToSpawn, spellPos, Quaternion.identity);
        go.GetComponent<Animator>().Play("Spell");
        // if is fireworm, make the Instance go to the player with a speed of 5 until it hits the player
        // if (mobObj.tag == "fireworm")
        // {
        //     GameObject spell = GameObject.FindWithTag("spell");
        //     spell.GetComponent<Rigidbody2D>().velocity = (playerPos - spellPos).normalized * 5;
        // }
        // // if hit the player, deal damage and start the explosion animation
        // if (mobObj.tag == "fireworm" && Vector2.Distance(spellPos, playerPos) <= 0.5f)
        // {
        //     PlayerCharacter.TakeHit(mob.attack);
        //     go.GetComponent<Animator>().Play("Explosion");
        // }
        StartCoroutine(Hit());
        Destroy(go, 1);
    }

    IEnumerator Attack()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;

        isAttacking = true;
        aiPath.enabled = false;
        int currAtk = 1;
        Debug.Log("mob.atkCnt: " + mob.atkCnt);
        if (mob.atkCnt == 1)
            currAtk = 1;
        else if (mob.atkCnt == 2)
            currAtk = Random.Range(1, 3);
        else
            currAtk = Random.Range(1, 4);

        Debug.Log("currAtk: " + currAtk);
        if (currAtk == 1)
            animator.SetTrigger("attack1");
        else if (currAtk == 2)
            animator.SetTrigger("attack2");
        else
            animator.SetTrigger("attack3");


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
        spellPos = playerPos + new Vector2(1, 2);
        yield return new WaitForSeconds(mob.waitTime);
        castSpell();
        rb.bodyType = RigidbodyType2D.Dynamic;
        aiPath.enabled = true;
        isCasting = false;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    IEnumerator Hit()
    {
        yield return null;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(spellPos, 2, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            StartCoroutine(cameraShake.Shake(.15f, .4f));
            hurt.Play();
            enemy.GetComponent<Animator>().SetTrigger("tookHit");
            Debug.Log("We hit " + enemy.name);
            PlayerCharacter.TakeHit(mob.attack);
        }
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
