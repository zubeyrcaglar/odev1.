using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public List<Sprite> nSprites, neSprites, eSprites, seSprites, sSprites;

    public float walkSpeed = 2f;
    public float frameRate = 6f;

    private Vector2 direction;
    private float idleTime;

    void Update()
    {
        // Input Y�n�
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Hareket
        Move();

        // Sprite Flip
        HandleSpriteFlip();

        // Animasyon
        AnimateSprite();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(direction.x, direction.y, 0) * walkSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }

    private void HandleSpriteFlip()
    {
        if (direction.x < 0)
            spriteRenderer.flipX = true;
        else if (direction.x > 0)
            spriteRenderer.flipX = false;
    }

    private void AnimateSprite()
    {
        List<Sprite> selectedSprites = GetSpriteDirection();

        if (selectedSprites != null && selectedSprites.Count > 0)
        {
            float playTime = Time.time - idleTime;
            int frame = (int)(playTime * frameRate) % selectedSprites.Count;
            spriteRenderer.sprite = selectedSprites[frame];
        }
        else
        {
            idleTime = Time.time;
        }
    }

    private List<Sprite> GetSpriteDirection()
    {
        if (direction.y > 0) return Mathf.Abs(direction.x) > 0 ? neSprites : nSprites; // Kuzey veya Kuzeydo�u
        if (direction.y < 0) return Mathf.Abs(direction.x) > 0 ? seSprites : sSprites; // G�ney veya G�neydo�u
        if (direction.x != 0) return eSprites; // Do�u veya Bat�
        return null;
    }
}
