using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    public Animator crossfadeAnimator;
    public float crossfadeDuration = 1f;
    private GameObject item;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            // Inicia a animação de Crossfade_Start
            crossfadeAnimator.Play("Crossfade_Start");

            // Aguarda um pequeno atraso antes de iniciar a transição de cena
            StartCoroutine(LoadNextSceneWithDelay(crossfadeDuration));

            item = GameObject.Find("ItemAnimation");
            if (item != null)
            {
                Destroy(item);
            }
        }
    }

    IEnumerator LoadNextSceneWithDelay(float delay)
    {
        // Aguarda 0.1 segundos antes de iniciar a transição de cena
        yield return new WaitForSeconds(delay);

        // Carrega a próxima cena após o tempo da animação de Crossfade_End
        SceneManager.LoadScene(2);
    }
}
