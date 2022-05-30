using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileIconEffect : MonoBehaviour
{
    CharacterMovement characterMovement;

    SpriteRenderer spriteRenderer;
    bool loopPlay;

    public float time = 0.1f;
    public Sprite sprite1;
    public Sprite sprite2;

    private void Start()
    {
        characterMovement = GameObject.Find("Player").GetComponent<CharacterMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1;
        loopPlay = false;
    }

    public void TriggerProfile()
    {
        if (!loopPlay)
        {
            spriteRenderer.sprite = sprite2;

            loopPlay = true;
            if (IconEffect != null)
            {
                StopCoroutine(IconEffect);
            }
            IconEffect = StartCoroutine(PlayEffect());
        }
    }

    Coroutine IconEffect;
    IEnumerator PlayEffect()
    {
        yield return new WaitForSeconds(time);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);

        yield return new WaitForSeconds(time);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);

        loopPlay = false;
        spriteRenderer.sprite = sprite1;
    }
}
