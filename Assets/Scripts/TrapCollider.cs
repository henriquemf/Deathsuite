using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollider : MonoBehaviour
{
    public Animator animator;
    
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Traps")
		{
			PlayerCharacter.TakeHit(30);
			animator.SetTrigger("tookHit");
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Traps")
		{
			animator.SetBool("tookHit", false);
		}
	}
}
