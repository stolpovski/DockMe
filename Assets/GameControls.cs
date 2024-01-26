//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/GameControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GameControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""Common"",
            ""id"": ""2877b87d-baeb-480d-98d2-8139eda8bc40"",
            ""actions"": [
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""c63e95de-23ee-45df-9253-776c9265e8b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e9b34839-61e2-4064-b61d-f15dec09d6be"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Car"",
            ""id"": ""13307d44-77be-4041-bd23-5595b0459a93"",
            ""actions"": [
                {
                    ""name"": ""Forward"",
                    ""type"": ""Button"",
                    ""id"": ""38c95d8b-d912-440c-a2bb-b576cc86c077"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4b844ee7-e5da-4a81-8355-ae2495c0a44c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Common
        m_Common = asset.FindActionMap("Common", throwIfNotFound: true);
        m_Common_Escape = m_Common.FindAction("Escape", throwIfNotFound: true);
        // Car
        m_Car = asset.FindActionMap("Car", throwIfNotFound: true);
        m_Car_Forward = m_Car.FindAction("Forward", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Common
    private readonly InputActionMap m_Common;
    private List<ICommonActions> m_CommonActionsCallbackInterfaces = new List<ICommonActions>();
    private readonly InputAction m_Common_Escape;
    public struct CommonActions
    {
        private @GameControls m_Wrapper;
        public CommonActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Escape => m_Wrapper.m_Common_Escape;
        public InputActionMap Get() { return m_Wrapper.m_Common; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CommonActions set) { return set.Get(); }
        public void AddCallbacks(ICommonActions instance)
        {
            if (instance == null || m_Wrapper.m_CommonActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CommonActionsCallbackInterfaces.Add(instance);
            @Escape.started += instance.OnEscape;
            @Escape.performed += instance.OnEscape;
            @Escape.canceled += instance.OnEscape;
        }

        private void UnregisterCallbacks(ICommonActions instance)
        {
            @Escape.started -= instance.OnEscape;
            @Escape.performed -= instance.OnEscape;
            @Escape.canceled -= instance.OnEscape;
        }

        public void RemoveCallbacks(ICommonActions instance)
        {
            if (m_Wrapper.m_CommonActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICommonActions instance)
        {
            foreach (var item in m_Wrapper.m_CommonActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CommonActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CommonActions @Common => new CommonActions(this);

    // Car
    private readonly InputActionMap m_Car;
    private List<ICarActions> m_CarActionsCallbackInterfaces = new List<ICarActions>();
    private readonly InputAction m_Car_Forward;
    public struct CarActions
    {
        private @GameControls m_Wrapper;
        public CarActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Forward => m_Wrapper.m_Car_Forward;
        public InputActionMap Get() { return m_Wrapper.m_Car; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CarActions set) { return set.Get(); }
        public void AddCallbacks(ICarActions instance)
        {
            if (instance == null || m_Wrapper.m_CarActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CarActionsCallbackInterfaces.Add(instance);
            @Forward.started += instance.OnForward;
            @Forward.performed += instance.OnForward;
            @Forward.canceled += instance.OnForward;
        }

        private void UnregisterCallbacks(ICarActions instance)
        {
            @Forward.started -= instance.OnForward;
            @Forward.performed -= instance.OnForward;
            @Forward.canceled -= instance.OnForward;
        }

        public void RemoveCallbacks(ICarActions instance)
        {
            if (m_Wrapper.m_CarActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICarActions instance)
        {
            foreach (var item in m_Wrapper.m_CarActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CarActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CarActions @Car => new CarActions(this);
    public interface ICommonActions
    {
        void OnEscape(InputAction.CallbackContext context);
    }
    public interface ICarActions
    {
        void OnForward(InputAction.CallbackContext context);
    }
}
