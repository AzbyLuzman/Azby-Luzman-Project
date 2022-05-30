using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightCollider : MonoBehaviour
{
    CharacterMovement characterMovement;

    Light2D light2D;
    LayerMask playerMask;
    Transform player;

    public ProfileIconEffect profileIconEffect;
    public bool alert;

    private void Start()
    {
        characterMovement = GameObject.Find("Player").GetComponent<CharacterMovement>();

        light2D = GetComponent<Light2D>();
        playerMask = LayerMask.GetMask("Player");
        player = GameObject.Find("Player").transform.Find("PlayerCollider");
        profileIconEffect = GameObject.Find("Character Profile").GetComponent<ProfileIconEffect>();
        alert = false;
    }

    private void Update()
    {
        Vector2 point = transform.position;
        float radius = light2D.pointLightOuterRadius;
        Collider2D collider2D;
        collider2D = Physics2D.OverlapCircle(point, radius, playerMask);
        if (collider2D != null)
        {
            Vector2 direction = player.position - transform.position;
            float deltaAngle = Vector2.Angle(direction, transform.up);

            if (deltaAngle < light2D.pointLightInnerAngle / 2)
            {
                if (!characterMovement.crouch && !alert)
                {
                    alert = true;
                }
                profileIconEffect.TriggerProfile();
            }
            else if (alert)
            {
                alert = false;
            }
        }
        else if (alert)
        {
            alert = false;
        }
    }
}
