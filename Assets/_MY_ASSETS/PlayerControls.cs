// GENERATED AUTOMATICALLY FROM 'Assets/_MY_ASSETS/PlayerControls.inputactions'

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
            ""name"": ""NoteSelector"",
            ""id"": ""9407347b-6826-4bd1-a7b4-78828bb77205"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""23a6235f-58a8-4acc-857a-d1751687b1f2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sing"",
                    ""type"": ""Button"",
                    ""id"": ""51e40b6b-659c-4458-8139-35a60333c079"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LockNote"",
                    ""type"": ""Button"",
                    ""id"": ""eff156e8-0cac-4e4c-8cc4-6b4b8228f265"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a882b611-a808-4386-959e-6eda97e8875a"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2626121-d1ca-4a9c-990b-ad80c8c01b65"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3c72de4-5746-474a-ae7c-21c466ed4244"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockNote"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // NoteSelector
        m_NoteSelector = asset.FindActionMap("NoteSelector", throwIfNotFound: true);
        m_NoteSelector_Move = m_NoteSelector.FindAction("Move", throwIfNotFound: true);
        m_NoteSelector_Sing = m_NoteSelector.FindAction("Sing", throwIfNotFound: true);
        m_NoteSelector_LockNote = m_NoteSelector.FindAction("LockNote", throwIfNotFound: true);
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

    // NoteSelector
    private readonly InputActionMap m_NoteSelector;
    private INoteSelectorActions m_NoteSelectorActionsCallbackInterface;
    private readonly InputAction m_NoteSelector_Move;
    private readonly InputAction m_NoteSelector_Sing;
    private readonly InputAction m_NoteSelector_LockNote;
    public struct NoteSelectorActions
    {
        private @PlayerControls m_Wrapper;
        public NoteSelectorActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_NoteSelector_Move;
        public InputAction @Sing => m_Wrapper.m_NoteSelector_Sing;
        public InputAction @LockNote => m_Wrapper.m_NoteSelector_LockNote;
        public InputActionMap Get() { return m_Wrapper.m_NoteSelector; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NoteSelectorActions set) { return set.Get(); }
        public void SetCallbacks(INoteSelectorActions instance)
        {
            if (m_Wrapper.m_NoteSelectorActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_NoteSelectorActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_NoteSelectorActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_NoteSelectorActionsCallbackInterface.OnMove;
                @Sing.started -= m_Wrapper.m_NoteSelectorActionsCallbackInterface.OnSing;
                @Sing.performed -= m_Wrapper.m_NoteSelectorActionsCallbackInterface.OnSing;
                @Sing.canceled -= m_Wrapper.m_NoteSelectorActionsCallbackInterface.OnSing;
                @LockNote.started -= m_Wrapper.m_NoteSelectorActionsCallbackInterface.OnLockNote;
                @LockNote.performed -= m_Wrapper.m_NoteSelectorActionsCallbackInterface.OnLockNote;
                @LockNote.canceled -= m_Wrapper.m_NoteSelectorActionsCallbackInterface.OnLockNote;
            }
            m_Wrapper.m_NoteSelectorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Sing.started += instance.OnSing;
                @Sing.performed += instance.OnSing;
                @Sing.canceled += instance.OnSing;
                @LockNote.started += instance.OnLockNote;
                @LockNote.performed += instance.OnLockNote;
                @LockNote.canceled += instance.OnLockNote;
            }
        }
    }
    public NoteSelectorActions @NoteSelector => new NoteSelectorActions(this);
    public interface INoteSelectorActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSing(InputAction.CallbackContext context);
        void OnLockNote(InputAction.CallbackContext context);
    }
}
