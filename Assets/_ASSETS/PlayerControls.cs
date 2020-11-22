// GENERATED AUTOMATICALLY FROM 'Assets/_ASSETS/PlayerControls.inputactions'

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
                    ""type"": ""Value"",
                    ""id"": ""51e40b6b-659c-4458-8139-35a60333c079"",
                    ""expectedControlType"": ""Analog"",
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
                },
                {
                    ""name"": ""SwitchWheelType"",
                    ""type"": ""Button"",
                    ""id"": ""bfef0087-2834-4a51-923f-7c192b36e847"",
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
                    ""id"": ""b3c72de4-5746-474a-ae7c-21c466ed4244"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockNote"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6366bcc-c533-4aca-9ff3-8626e78c4121"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockNote"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d74f31d-ae83-4d50-b6af-bd6550046dfa"",
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
                    ""id"": ""2479c25b-91b1-4652-b806-314f0806d4fd"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6ca222a-6098-4e99-9af7-0dfdda7a8b6b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWheelType"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player"",
            ""id"": ""141f1024-2864-4acb-9c4d-0ff8ace6cb07"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""2b8cea39-9e99-42b8-82e7-d466ad2f5b69"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""ecbea8fe-64c8-495a-b322-afdf10cbfd4b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c7ceebe8-5df4-4b2c-a5a8-0f304288d687"",
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
                    ""id"": ""fcce6fa0-70b8-4fd0-bdaf-fe5ed06f6df3"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""GameManager"",
            ""id"": ""3df114dd-e03b-4545-a7a9-35c00b7179bb"",
            ""actions"": [
                {
                    ""name"": ""ReloadScene"",
                    ""type"": ""Button"",
                    ""id"": ""a7b1c764-dead-43b4-a05f-23928419ac23"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""de3cb12a-8fe2-4b4a-9368-51c4a303f792"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""35cd41a8-f4d3-4f3e-83c6-58189b41263f"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReloadScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf3ed4fa-8445-4dc1-bf9a-fb7226d8ab56"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
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
        m_NoteSelector_SwitchWheelType = m_NoteSelector.FindAction("SwitchWheelType", throwIfNotFound: true);
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        // GameManager
        m_GameManager = asset.FindActionMap("GameManager", throwIfNotFound: true);
        m_GameManager_ReloadScene = m_GameManager.FindAction("ReloadScene", throwIfNotFound: true);
        m_GameManager_PauseGame = m_GameManager.FindAction("PauseGame", throwIfNotFound: true);
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
    private readonly InputAction m_NoteSelector_SwitchWheelType;
    public struct NoteSelectorActions
    {
        private @PlayerControls m_Wrapper;
        public NoteSelectorActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_NoteSelector_Move;
        public InputAction @Sing => m_Wrapper.m_NoteSelector_Sing;
        public InputAction @LockNote => m_Wrapper.m_NoteSelector_LockNote;
        public InputAction @SwitchWheelType => m_Wrapper.m_NoteSelector_SwitchWheelType;
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
                @SwitchWheelType.started -= m_Wrapper.m_NoteSelectorActionsCallbackInterface.OnSwitchWheelType;
                @SwitchWheelType.performed -= m_Wrapper.m_NoteSelectorActionsCallbackInterface.OnSwitchWheelType;
                @SwitchWheelType.canceled -= m_Wrapper.m_NoteSelectorActionsCallbackInterface.OnSwitchWheelType;
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
                @SwitchWheelType.started += instance.OnSwitchWheelType;
                @SwitchWheelType.performed += instance.OnSwitchWheelType;
                @SwitchWheelType.canceled += instance.OnSwitchWheelType;
            }
        }
    }
    public NoteSelectorActions @NoteSelector => new NoteSelectorActions(this);

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // GameManager
    private readonly InputActionMap m_GameManager;
    private IGameManagerActions m_GameManagerActionsCallbackInterface;
    private readonly InputAction m_GameManager_ReloadScene;
    private readonly InputAction m_GameManager_PauseGame;
    public struct GameManagerActions
    {
        private @PlayerControls m_Wrapper;
        public GameManagerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ReloadScene => m_Wrapper.m_GameManager_ReloadScene;
        public InputAction @PauseGame => m_Wrapper.m_GameManager_PauseGame;
        public InputActionMap Get() { return m_Wrapper.m_GameManager; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameManagerActions set) { return set.Get(); }
        public void SetCallbacks(IGameManagerActions instance)
        {
            if (m_Wrapper.m_GameManagerActionsCallbackInterface != null)
            {
                @ReloadScene.started -= m_Wrapper.m_GameManagerActionsCallbackInterface.OnReloadScene;
                @ReloadScene.performed -= m_Wrapper.m_GameManagerActionsCallbackInterface.OnReloadScene;
                @ReloadScene.canceled -= m_Wrapper.m_GameManagerActionsCallbackInterface.OnReloadScene;
                @PauseGame.started -= m_Wrapper.m_GameManagerActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_GameManagerActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_GameManagerActionsCallbackInterface.OnPauseGame;
            }
            m_Wrapper.m_GameManagerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ReloadScene.started += instance.OnReloadScene;
                @ReloadScene.performed += instance.OnReloadScene;
                @ReloadScene.canceled += instance.OnReloadScene;
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
            }
        }
    }
    public GameManagerActions @GameManager => new GameManagerActions(this);
    public interface INoteSelectorActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSing(InputAction.CallbackContext context);
        void OnLockNote(InputAction.CallbackContext context);
        void OnSwitchWheelType(InputAction.CallbackContext context);
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IGameManagerActions
    {
        void OnReloadScene(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
    }
}
