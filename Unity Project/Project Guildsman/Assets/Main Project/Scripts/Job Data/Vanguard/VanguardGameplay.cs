using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class VanguardGameplay : MonoBehaviour
{
    public VanguardCombo vanguardCombo;
    public PlayerControls controls;

    // SpecialAction Check
    private bool delayedSpecialAction;
    private string phaseCheck;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Dodge.performed += ctx => Dodge();
        controls.Gameplay.UseItem.performed += ctx => UseItem();

        controls.Gameplay.LightAttack.performed += ctx =>
        {
            if (ctx.interaction is TapInteraction)
            {
                LightAttack();
            }
        };

        controls.Gameplay.HeavyAttack.performed += ctx =>
        {
            if (ctx.interaction is TapInteraction)
            {
                HeavyAttack();
            }
        };
        
        controls.Gameplay.SpecialAction.started += ctx =>
        {
            if (ctx.interaction is TapInteraction)
            {
                SpecialAttack();
            }
        };
        controls.Gameplay.SpecialAction.performed += ctx =>
        {
            if (ctx.interaction is HoldInteraction)
            {
                string phase = "Hold";
                SpecialAction(phase);
            }
        };
        controls.Gameplay.SpecialAction.canceled += ctx =>
        {
            if (ctx.interaction is HoldInteraction)
            {
                string phase = "Released";
                SpecialAction(phase);
            }
        };


        controls.Gameplay.Brace.performed += ctx =>
        {
            if (ctx.interaction is TapInteraction)
            {
                Parry();
            }
        };
        controls.Gameplay.Brace.performed += ctx =>
        {
            if (ctx.interaction is HoldInteraction)
            {
                string phase = "Hold";
                Brace(phase);
            }
        };
        controls.Gameplay.Brace.canceled += ctx =>
        {
            if (ctx.interaction is HoldInteraction)
            {
                string phase = "Released";
                Brace(phase);
            }
        };


        //controls.Gameplay.Execute.performed += ctx =>
        //{
        //    if (ctx.interaction is TapInteraction)
        //    {
        //        Execute();
        //    }
        //};
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    private void Start()
    {
        vanguardCombo = GetComponent<VanguardCombo>();        
    }

    private void Update()
    {
        if (delayedSpecialAction && !InputHandler.interact)
        {
            delayedSpecialAction = false;
            SpecialAction(phaseCheck);
        }
    }

    #region Combat

    void UseItem()
    {
        Debug.Log("UseItem");
    }

    void Dodge()
    {
        Debug.Log("Dodge");
    }

    void LightAttack()
    {
        vanguardCombo.LightAttackCombo();
    }
  
    void HeavyAttack()
    {
        vanguardCombo.HeavyAttackCombo();
    }

    void SpecialAttack()
    {
        vanguardCombo.SpecialAttackCombo();
    }

    void SpecialAction(string phase)
    {
        bool action = false;

        if (InputHandler.interact)
        {
            phaseCheck = phase;
            delayedSpecialAction = true;
        }
        else
        {
            if (phase == "Hold")
            {
                action = true;
            }
            else if (phase == "Released")
            {
                action = false;
            }
            vanguardCombo.SpecialAction(action);
        }
    }

    void Parry()
    {
        vanguardCombo.Parry();
    }

    void Brace(string phase)
    {
        bool action = false;

        if (phase == "Hold")
        {
            action = true;
        }
        else if (phase == "Released")
        {
            action = false;
        }
        vanguardCombo.Brace(action);
    }


    void Execute()
    {
        vanguardCombo.Execute();
    }

    #endregion
}
