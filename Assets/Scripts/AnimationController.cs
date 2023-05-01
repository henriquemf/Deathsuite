using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // define o parâmetro "isRunning" como verdadeiro ou falso, dependendo de alguma condição
        animator.SetBool("isRunning", Input.GetKey(KeyCode.Space));
    }
}
