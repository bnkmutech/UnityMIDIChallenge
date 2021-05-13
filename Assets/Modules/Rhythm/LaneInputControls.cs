// GENERATED AUTOMATICALLY FROM 'Assets/Modules/LaneInputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @LaneInputControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @LaneInputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""LaneInputControls"",
    ""maps"": [
        {
            ""name"": ""LaneControls"",
            ""id"": ""19863cf0-9b4b-4b7b-aba8-60275d13ec56"",
            ""actions"": [
                {
                    ""name"": ""Lane1"",
                    ""type"": ""Value"",
                    ""id"": ""b207e28c-a396-4fde-aba9-a97424afc828"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lane2"",
                    ""type"": ""Value"",
                    ""id"": ""5f7af74f-4c71-474a-b41e-c3dcd1cb0a36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lane3"",
                    ""type"": ""Value"",
                    ""id"": ""edee338f-65f1-4dd6-b9c6-0000b2901acc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lane4"",
                    ""type"": ""Value"",
                    ""id"": ""2f912bf9-af1f-4447-894f-22889a3fc594"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lane5"",
                    ""type"": ""Value"",
                    ""id"": ""5ed297f3-110c-43b0-ae6a-f81e4cebda1e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lane6"",
                    ""type"": ""Value"",
                    ""id"": ""ec57acdd-cc5c-470c-a3ae-84355848bcf9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f71913f4-c9ea-4ae4-9f04-c805fc37e78a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lane1"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f4eee659-7237-4c1d-81fc-035e8b50350a"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Lane1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""d0a7c8e1-8c5c-4294-912d-6e43b00fcd3b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Lane2"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7b79fde8-57c4-456c-8540-9ca4403fbf35"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Lane2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""830e3832-72e2-4e3b-8039-b6f1c9d31e90"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Lane3"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""454a69dd-baa6-4f73-b94b-ea991fb5d406"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Lane3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""e97e7301-d28f-410e-b4f5-a225bfcdd411"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Lane4"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""eef2840e-01e1-41e3-8d0b-790157ca35b7"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Lane4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""2613a749-ce2e-41a0-af8d-adced7ecde56"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Lane5"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f844a005-25f9-4a79-970d-3c0e58397f50"",
                    ""path"": ""<Keyboard>/n"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Lane5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""42690ecf-e028-4d82-86ae-cc4e83d2c9fe"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Lane6"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""86c47720-8f90-4546-ac41-1d425d484589"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Lane6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // LaneControls
        m_LaneControls = asset.FindActionMap("LaneControls", throwIfNotFound: true);
        m_LaneControls_Lane1 = m_LaneControls.FindAction("Lane1", throwIfNotFound: true);
        m_LaneControls_Lane2 = m_LaneControls.FindAction("Lane2", throwIfNotFound: true);
        m_LaneControls_Lane3 = m_LaneControls.FindAction("Lane3", throwIfNotFound: true);
        m_LaneControls_Lane4 = m_LaneControls.FindAction("Lane4", throwIfNotFound: true);
        m_LaneControls_Lane5 = m_LaneControls.FindAction("Lane5", throwIfNotFound: true);
        m_LaneControls_Lane6 = m_LaneControls.FindAction("Lane6", throwIfNotFound: true);
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

    // LaneControls
    private readonly InputActionMap m_LaneControls;
    private ILaneControlsActions m_LaneControlsActionsCallbackInterface;
    private readonly InputAction m_LaneControls_Lane1;
    private readonly InputAction m_LaneControls_Lane2;
    private readonly InputAction m_LaneControls_Lane3;
    private readonly InputAction m_LaneControls_Lane4;
    private readonly InputAction m_LaneControls_Lane5;
    private readonly InputAction m_LaneControls_Lane6;
    public struct LaneControlsActions
    {
        private @LaneInputControls m_Wrapper;
        public LaneControlsActions(@LaneInputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Lane1 => m_Wrapper.m_LaneControls_Lane1;
        public InputAction @Lane2 => m_Wrapper.m_LaneControls_Lane2;
        public InputAction @Lane3 => m_Wrapper.m_LaneControls_Lane3;
        public InputAction @Lane4 => m_Wrapper.m_LaneControls_Lane4;
        public InputAction @Lane5 => m_Wrapper.m_LaneControls_Lane5;
        public InputAction @Lane6 => m_Wrapper.m_LaneControls_Lane6;
        public InputActionMap Get() { return m_Wrapper.m_LaneControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LaneControlsActions set) { return set.Get(); }
        public void SetCallbacks(ILaneControlsActions instance)
        {
            if (m_Wrapper.m_LaneControlsActionsCallbackInterface != null)
            {
                @Lane1.started -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane1;
                @Lane1.performed -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane1;
                @Lane1.canceled -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane1;
                @Lane2.started -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane2;
                @Lane2.performed -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane2;
                @Lane2.canceled -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane2;
                @Lane3.started -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane3;
                @Lane3.performed -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane3;
                @Lane3.canceled -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane3;
                @Lane4.started -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane4;
                @Lane4.performed -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane4;
                @Lane4.canceled -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane4;
                @Lane5.started -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane5;
                @Lane5.performed -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane5;
                @Lane5.canceled -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane5;
                @Lane6.started -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane6;
                @Lane6.performed -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane6;
                @Lane6.canceled -= m_Wrapper.m_LaneControlsActionsCallbackInterface.OnLane6;
            }
            m_Wrapper.m_LaneControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Lane1.started += instance.OnLane1;
                @Lane1.performed += instance.OnLane1;
                @Lane1.canceled += instance.OnLane1;
                @Lane2.started += instance.OnLane2;
                @Lane2.performed += instance.OnLane2;
                @Lane2.canceled += instance.OnLane2;
                @Lane3.started += instance.OnLane3;
                @Lane3.performed += instance.OnLane3;
                @Lane3.canceled += instance.OnLane3;
                @Lane4.started += instance.OnLane4;
                @Lane4.performed += instance.OnLane4;
                @Lane4.canceled += instance.OnLane4;
                @Lane5.started += instance.OnLane5;
                @Lane5.performed += instance.OnLane5;
                @Lane5.canceled += instance.OnLane5;
                @Lane6.started += instance.OnLane6;
                @Lane6.performed += instance.OnLane6;
                @Lane6.canceled += instance.OnLane6;
            }
        }
    }
    public LaneControlsActions @LaneControls => new LaneControlsActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface ILaneControlsActions
    {
        void OnLane1(InputAction.CallbackContext context);
        void OnLane2(InputAction.CallbackContext context);
        void OnLane3(InputAction.CallbackContext context);
        void OnLane4(InputAction.CallbackContext context);
        void OnLane5(InputAction.CallbackContext context);
        void OnLane6(InputAction.CallbackContext context);
    }
}
