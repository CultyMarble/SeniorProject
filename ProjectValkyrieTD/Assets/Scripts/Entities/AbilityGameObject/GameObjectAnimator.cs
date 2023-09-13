using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GameObjectAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites = default;
    [SerializeField] private float animationSpeed = default;

    private SpriteRenderer spriteRenderer;

    private float animationTimer = default;
    private int currentSpriteIndex = default;

    //===========================================================================
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        RunAnimation();
    }

    //===========================================================================
    private void RunAnimation()
    {
        animationTimer += Time.deltaTime;
        if (animationTimer >= animationSpeed)
        {
            animationTimer -= animationSpeed;

            if (currentSpriteIndex == sprites.Length)
                currentSpriteIndex = 0;

            spriteRenderer.sprite = sprites[currentSpriteIndex];
            currentSpriteIndex++;
        }
    }
}
