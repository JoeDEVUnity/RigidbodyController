//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Player/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Controls"",
            ""id"": ""1ba23292-ad10-4561-b056-51f551565bb5"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2e6700b6-d6f6-490c-a723-ec15918a0381"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""PassThrough"",
                    ""id"": ""19b4dbaa-a3e2-4f21-a761-71413f4a6f34"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jumping"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4c734876-8ded-49c5-94af-fa07a7f4852d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprinting"",
                    ""type"": ""PassThrough"",
                    ""id"": ""86b8a054-3125-44e8-89ca-854d68e30905"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Switching"",
                    ""type"": ""PassThrough"",
                    ""id"": ""74857f8b-0ec3-4505-9f77-dd7355d108af"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FireValue"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a5c81812-509b-4846-91f1-ef5b107689f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0b20a9f8-234e-49c0-ba4e-99966246a8d6"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""8e31a5c5-943e-48dd-b1f0-ce17d57b1d7d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""aad766d9-1e7c-4d09-a7d4-e019f865668c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""dfbd1c35-d4d5-4334-9f83-13409269b45a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""95290f46-0ace-4fc8-88a7-2f841a0f5d1d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3cb94f03-218d-4707-8c63-c670fdcd03d3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""852e6dbb-b0b6-48e3-a131-1fbb1927c078"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jumping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02d3f813-73f5-40c5-85d8-a229a4d45008"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprinting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3ea6d74-880e-4e52-95b6-cb2b4303b820"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switching"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f39ca211-051b-4e01-a187-9cbb99dd4cd4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireValue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Controls
        m_Controls = asset.FindActionMap("Controls", throwIfNotFound: true);
        m_Controls_Movement = m_Controls.FindAction("Movement", throwIfNotFound: true);
        m_Controls_Mouse = m_Controls.FindAction("Mouse", throwIfNotFound: true);
        m_Controls_Jumping = m_Controls.FindAction("Jumping", throwIfNotFound: true);
        m_Controls_Sprinting = m_Controls.FindAction("Sprinting", throwIfNotFound: true);
        m_Controls_Switching = m_Controls.FindAction("Switching", throwIfNotFound: true);
        m_Controls_FireValue = m_Controls.FindAction("FireValue", throwIfNotFound: true);
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

    // Controls
    private readonly InputActionMap m_Controls;
    private IControlsActions m_ControlsActionsCallbackInterface;
    private readonly InputAction m_Controls_Movement;
    private readonly InputAction m_Controls_Mouse;
    private readonly InputAction m_Controls_Jumping;
    private readonly InputAction m_Controls_Sprinting;
    private readonly InputAction m_Controls_Switching;
    private readonly InputAction m_Controls_FireValue;
    public struct ControlsActions
    {
        private @PlayerControls m_Wrapper;
        public ControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Controls_Movement;
        public InputAction @Mouse => m_Wrapper.m_Controls_Mouse;
        public InputAction @Jumping => m_Wrapper.m_Controls_Jumping;
        public InputAction @Sprinting => m_Wrapper.m_Controls_Sprinting;
        public InputAction @Switching => m_Wrapper.m_Controls_Switching;
        public InputAction @FireValue => m_Wrapper.m_Controls_FireValue;
        public InputActionMap Get() { return m_Wrapper.m_Controls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlsActions set) { return set.Get(); }
        public void SetCallbacks(IControlsActions instance)
        {
            if (m_Wrapper.m_ControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMovement;
                @Mouse.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnMouse;
                @Jumping.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnJumping;
                @Jumping.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnJumping;
                @Jumping.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnJumping;
                @Sprinting.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnSprinting;
                @Sprinting.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnSprinting;
                @Sprinting.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnSprinting;
                @Switching.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnSwitching;
                @Switching.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnSwitching;
                @Switching.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnSwitching;
                @FireValue.started -= m_Wrapper.m_ControlsActionsCallbackInterface.OnFireValue;
                @FireValue.performed -= m_Wrapper.m_ControlsActionsCallbackInterface.OnFireValue;
                @FireValue.canceled -= m_Wrapper.m_ControlsActionsCallbackInterface.OnFireValue;
            }
            m_Wrapper.m_ControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
                @Jumping.started += instance.OnJumping;
                @Jumping.performed += instance.OnJumping;
                @Jumping.canceled += instance.OnJumping;
                @Sprinting.started += instance.OnSprinting;
                @Sprinting.performed += instance.OnSprinting;
                @Sprinting.canceled += instance.OnSprinting;
                @Switching.started += instance.OnSwitching;
                @Switching.performed += instance.OnSwitching;
                @Switching.canceled += instance.OnSwitching;
                @FireValue.started += instance.OnFireValue;
                @FireValue.performed += instance.OnFireValue;
                @FireValue.canceled += instance.OnFireValue;
            }
        }
    }
    public ControlsActions @Controls => new ControlsActions(this);
    public interface IControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
        void OnJumping(InputAction.CallbackContext context);
        void OnSprinting(InputAction.CallbackContext context);
        void OnSwitching(InputAction.CallbackContext context);
        void OnFireValue(InputAction.CallbackContext context);
    }
}
