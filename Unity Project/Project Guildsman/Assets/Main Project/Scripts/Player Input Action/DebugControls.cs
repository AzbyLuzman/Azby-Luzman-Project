// GENERATED AUTOMATICALLY FROM 'Assets/Main Project/Scripts/Player Input Action/DebugControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DebugControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DebugControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DebugControls"",
    ""maps"": [
        {
            ""name"": ""Test"",
            ""id"": ""23e69a88-20bb-4dbd-9bcd-10bb99bf433e"",
            ""actions"": [
                {
                    ""name"": ""TestInput"",
                    ""type"": ""Button"",
                    ""id"": ""aac48f19-bab6-47db-bcbf-fdd12a34c95d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d4c034d4-7f13-4e6f-a4a3-f48cea5f074a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Test
        m_Test = asset.FindActionMap("Test", throwIfNotFound: true);
        m_Test_TestInput = m_Test.FindAction("TestInput", throwIfNotFound: true);
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

    // Test
    private readonly InputActionMap m_Test;
    private ITestActions m_TestActionsCallbackInterface;
    private readonly InputAction m_Test_TestInput;
    public struct TestActions
    {
        private @DebugControls m_Wrapper;
        public TestActions(@DebugControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TestInput => m_Wrapper.m_Test_TestInput;
        public InputActionMap Get() { return m_Wrapper.m_Test; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestActions set) { return set.Get(); }
        public void SetCallbacks(ITestActions instance)
        {
            if (m_Wrapper.m_TestActionsCallbackInterface != null)
            {
                @TestInput.started -= m_Wrapper.m_TestActionsCallbackInterface.OnTestInput;
                @TestInput.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnTestInput;
                @TestInput.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnTestInput;
            }
            m_Wrapper.m_TestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TestInput.started += instance.OnTestInput;
                @TestInput.performed += instance.OnTestInput;
                @TestInput.canceled += instance.OnTestInput;
            }
        }
    }
    public TestActions @Test => new TestActions(this);
    public interface ITestActions
    {
        void OnTestInput(InputAction.CallbackContext context);
    }
}
