// GENERATED AUTOMATICALLY FROM 'Assets/Main Project/Scripts/Player Input Action/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""fcd6e2f3-bb06-4b51-8fb8-01ea3d647254"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4d1c9d98-6d61-49dd-9333-b916fe5bd0be"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a8a3a5a9-507e-4737-90e2-02a49fe29c18"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraLock"",
                    ""type"": ""Button"",
                    ""id"": ""afa0abe8-e7f6-44db-883c-d25bf72fd733"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,Hold""
                },
                {
                    ""name"": ""LightAttack"",
                    ""type"": ""Button"",
                    ""id"": ""10a4df9e-246b-49c7-97cb-9c5189a1e1b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,Hold""
                },
                {
                    ""name"": ""HeavyAttack"",
                    ""type"": ""Button"",
                    ""id"": ""9749de0f-3340-467f-bb70-df780a33b055"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,Hold""
                },
                {
                    ""name"": ""UseItem"",
                    ""type"": ""Button"",
                    ""id"": ""fb4a5c59-ac53-4840-9823-c98efb8e2752"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,Hold""
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""9d4e101e-5f6f-4db5-ab72-276d2b6a8529"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,Hold""
                },
                {
                    ""name"": ""Skills"",
                    ""type"": ""Button"",
                    ""id"": ""b405900e-c3ee-49dc-be65-da45cd1c1d79"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,Hold""
                },
                {
                    ""name"": ""SpecialAction"",
                    ""type"": ""Button"",
                    ""id"": ""aaefb191-fb13-409d-9c61-75ed28f2f457"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,Hold(duration=0.2)""
                },
                {
                    ""name"": ""Brace"",
                    ""type"": ""Button"",
                    ""id"": ""600e50f1-d802-44a6-b2d4-23858a72c8ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,Hold(duration=0.2)""
                },
                {
                    ""name"": ""Execute"",
                    ""type"": ""Button"",
                    ""id"": ""dc7045eb-a6a3-4cb8-b3a8-c5f98e287f3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,Hold(duration=0.2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c78d4a05-b23d-4215-a88f-b2566a4860dc"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d669f49-bac2-4f5d-bbbb-4ffaf61b3e0d"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a3f4bfa-5f0b-47d9-971d-17b35ed6c24f"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cac1dd87-6802-4920-97e0-e008babd9b2a"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4f0eecb-c6cd-4312-904a-5af658bd78f2"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31fa4783-b284-4aae-83a1-ddb6a6c12ccf"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b35782e5-f095-4f32-b086-03966274e115"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1762a2d-76a6-405a-a528-0a15289e8188"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skills"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7996a17a-8cd6-4bf3-9b61-9e99cdbe7b00"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d44f49a-4254-46e6-b97a-c50c4363d70f"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Brace"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1f7ed57-28f9-451d-95c6-d18c06fa7c88"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Execute"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Camera = m_Gameplay.FindAction("Camera", throwIfNotFound: true);
        m_Gameplay_CameraLock = m_Gameplay.FindAction("CameraLock", throwIfNotFound: true);
        m_Gameplay_LightAttack = m_Gameplay.FindAction("LightAttack", throwIfNotFound: true);
        m_Gameplay_HeavyAttack = m_Gameplay.FindAction("HeavyAttack", throwIfNotFound: true);
        m_Gameplay_UseItem = m_Gameplay.FindAction("UseItem", throwIfNotFound: true);
        m_Gameplay_Dodge = m_Gameplay.FindAction("Dodge", throwIfNotFound: true);
        m_Gameplay_Skills = m_Gameplay.FindAction("Skills", throwIfNotFound: true);
        m_Gameplay_SpecialAction = m_Gameplay.FindAction("SpecialAction", throwIfNotFound: true);
        m_Gameplay_Brace = m_Gameplay.FindAction("Brace", throwIfNotFound: true);
        m_Gameplay_Execute = m_Gameplay.FindAction("Execute", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Camera;
    private readonly InputAction m_Gameplay_CameraLock;
    private readonly InputAction m_Gameplay_LightAttack;
    private readonly InputAction m_Gameplay_HeavyAttack;
    private readonly InputAction m_Gameplay_UseItem;
    private readonly InputAction m_Gameplay_Dodge;
    private readonly InputAction m_Gameplay_Skills;
    private readonly InputAction m_Gameplay_SpecialAction;
    private readonly InputAction m_Gameplay_Brace;
    private readonly InputAction m_Gameplay_Execute;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Camera => m_Wrapper.m_Gameplay_Camera;
        public InputAction @CameraLock => m_Wrapper.m_Gameplay_CameraLock;
        public InputAction @LightAttack => m_Wrapper.m_Gameplay_LightAttack;
        public InputAction @HeavyAttack => m_Wrapper.m_Gameplay_HeavyAttack;
        public InputAction @UseItem => m_Wrapper.m_Gameplay_UseItem;
        public InputAction @Dodge => m_Wrapper.m_Gameplay_Dodge;
        public InputAction @Skills => m_Wrapper.m_Gameplay_Skills;
        public InputAction @SpecialAction => m_Wrapper.m_Gameplay_SpecialAction;
        public InputAction @Brace => m_Wrapper.m_Gameplay_Brace;
        public InputAction @Execute => m_Wrapper.m_Gameplay_Execute;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Camera.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCamera;
                @CameraLock.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraLock;
                @CameraLock.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraLock;
                @CameraLock.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraLock;
                @LightAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLightAttack;
                @LightAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLightAttack;
                @LightAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLightAttack;
                @HeavyAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHeavyAttack;
                @UseItem.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUseItem;
                @UseItem.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUseItem;
                @UseItem.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUseItem;
                @Dodge.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDodge;
                @Skills.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkills;
                @Skills.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkills;
                @Skills.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkills;
                @SpecialAction.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAction;
                @SpecialAction.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAction;
                @SpecialAction.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAction;
                @Brace.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBrace;
                @Brace.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBrace;
                @Brace.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBrace;
                @Execute.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnExecute;
                @Execute.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnExecute;
                @Execute.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnExecute;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
                @CameraLock.started += instance.OnCameraLock;
                @CameraLock.performed += instance.OnCameraLock;
                @CameraLock.canceled += instance.OnCameraLock;
                @LightAttack.started += instance.OnLightAttack;
                @LightAttack.performed += instance.OnLightAttack;
                @LightAttack.canceled += instance.OnLightAttack;
                @HeavyAttack.started += instance.OnHeavyAttack;
                @HeavyAttack.performed += instance.OnHeavyAttack;
                @HeavyAttack.canceled += instance.OnHeavyAttack;
                @UseItem.started += instance.OnUseItem;
                @UseItem.performed += instance.OnUseItem;
                @UseItem.canceled += instance.OnUseItem;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @Skills.started += instance.OnSkills;
                @Skills.performed += instance.OnSkills;
                @Skills.canceled += instance.OnSkills;
                @SpecialAction.started += instance.OnSpecialAction;
                @SpecialAction.performed += instance.OnSpecialAction;
                @SpecialAction.canceled += instance.OnSpecialAction;
                @Brace.started += instance.OnBrace;
                @Brace.performed += instance.OnBrace;
                @Brace.canceled += instance.OnBrace;
                @Execute.started += instance.OnExecute;
                @Execute.performed += instance.OnExecute;
                @Execute.canceled += instance.OnExecute;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
        void OnCameraLock(InputAction.CallbackContext context);
        void OnLightAttack(InputAction.CallbackContext context);
        void OnHeavyAttack(InputAction.CallbackContext context);
        void OnUseItem(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnSkills(InputAction.CallbackContext context);
        void OnSpecialAction(InputAction.CallbackContext context);
        void OnBrace(InputAction.CallbackContext context);
        void OnExecute(InputAction.CallbackContext context);
    }
}
