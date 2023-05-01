using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    public Animator crossfadeAnimator;
    public float crossfadeDuration = 1f;

    private void Start()
    {
        // Inicia a animação de Crossfade_End
        crossfadeAnimator.Play("Crossfade_End");
    }
}
