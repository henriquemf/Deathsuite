using UnityEngine;
using UnityEngine.UI;

public class IdleHiro : MonoBehaviour
{
    public Sprite[] sprites; // Adicione aqui a sequência de sprites que você quer usar
    public float frameTime = 0.1f; // Tempo entre cada quadro da animação

    private Image image;
    private int currentSpriteIndex = 0;
    private float timer = 0;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        // Atualiza o quadro atual da animação
        timer += Time.deltaTime;
        if (timer >= frameTime)
        {
            timer = 0;
            currentSpriteIndex++;
            if (currentSpriteIndex >= sprites.Length)
            {
                currentSpriteIndex = 0;
            }
            image.sprite = sprites[currentSpriteIndex];
        }
    }
}
