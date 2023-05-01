using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Animator crossfadeAnimator;
    public float crossfadeDuration = 1f;
    public float animationDuration = 4f;
    private float elapsedTime;

    private void Start()
    {
        // Inicia a animação de Transition_Start
        crossfadeAnimator.Play("Transition_Start");
    }

    public void LoadScene(int sceneID)
    {
        crossfadeAnimator.Play("Transition_End");
        PlayerCharacter.ResetCharacter();
        SceneManager.LoadScene(sceneID);
    }
}
