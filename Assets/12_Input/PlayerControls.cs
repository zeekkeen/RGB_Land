// GENERATED AUTOMATICALLY FROM 'Assets/12_Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""0fa20521-65d7-4ddb-8f00-5143a9d0b9d9"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""23707ce7-21fb-4302-9a1d-ddfff3050e5e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""move"",
                    ""type"": ""Value"",
                    ""id"": ""7f7ecc35-439d-4f0b-856a-ef9e5175e19f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""62529b62-d53b-47ef-a8c9-4e945d06ed61"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""4f71c52d-633f-4650-b66c-4c4970d48564"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MeleeAttack"",
                    ""type"": ""Button"",
                    ""id"": ""e93ae821-9fe9-4b70-9019-c5e50f7d7330"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""anykey"",
                    ""type"": ""Button"",
                    ""id"": ""9da5e00b-0865-403b-b50f-b82a040e3a39"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""cea6d156-9f2a-400e-b0bc-93c93983f829"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""d610bdf0-d9c3-488f-b0dd-4974ec338359"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RangedAttack"",
                    ""type"": ""Button"",
                    ""id"": ""04ffeaa3-0d6f-4bc3-b6e8-91955103dbef"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ColorControl"",
                    ""type"": ""Button"",
                    ""id"": ""260b9976-482a-4d65-b23f-a65ec47d339e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dialog"",
                    ""type"": ""Button"",
                    ""id"": ""d19caf7d-2aa2-4575-85b0-39872a805fae"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Button"",
                    ""id"": ""b6125b4a-b48b-465a-97db-a52eef499dbf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NextRangedPower"",
                    ""type"": ""Button"",
                    ""id"": ""8046b765-66e5-4885-8f84-183ea5cd1d8c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PreviousRangedPower"",
                    ""type"": ""Button"",
                    ""id"": ""29954626-e51d-4515-b8fc-01557ae5ec8b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""eac4701a-04b9-4d64-8fb3-d00416fe4b6e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba70b592-fb13-4116-a6f4-3787939bfb88"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""25a6e99f-e2fe-4829-8288-721a74a7f110"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3252ad2e-9bba-4d2e-ae5d-4faa4bb95354"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9c3a85ee-7034-437a-b1a3-f73b65b97ffb"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fcd573e2-4df3-4bd4-a440-36f82da69942"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6d105a33-65ef-4a1d-aa88-902d34253449"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0303b5ed-85a8-457a-84a3-0b68b242c5a0"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""GamePadArrows"",
                    ""id"": ""b623efb2-1476-4a53-b897-0dd6ce11c9a1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""880b0d16-5fec-4391-bc57-3821b3482066"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6c14fe77-45c0-4d3f-92e3-cdf72e1c6151"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cffd4097-0205-49e8-b4a7-611746a68b93"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0b93fd98-0370-476a-826d-fddc1d62b627"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""84dd54b9-524b-4535-9404-caaf52ba0b14"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""225e7721-b648-45d3-8813-6858dd995f36"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b64c2bc-4bef-4676-9c85-8049aabecabd"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f29b6582-40ee-45f9-ab37-806283a01efc"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d582b2f-a688-49d9-9be3-f599395f3a3e"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b1667b4-7fc8-4586-8346-73373556b009"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3a6e9fb-9447-4f8c-905f-6bdacfd6fab0"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc5bc211-4442-40ce-bc92-cae0b2076166"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""anykey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59b6a7e5-bbc5-426b-b8a2-b2ef837aaa96"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""anykey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b0f59d1-a184-46d1-8a71-f3cad37629e8"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34e72728-0ee0-4d34-95aa-d9a271d06c7b"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30a07107-bd47-48a8-970f-e0df90d35dc8"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5dcbf6f-e59e-4335-a7e5-464683c8d63c"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""058e7019-0f6c-48fc-8af0-67d87f9d35bf"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75e82c19-e996-4634-9460-fd49d491306c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""575e605d-9495-47a8-84a7-786b760148d5"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RangedAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce276f68-40ab-41ba-9e2f-21b89e33bbd5"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RangedAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88bc8aff-0434-43bc-a09d-8d354a958fb6"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ColorControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f54b2e3-fc20-4379-a6e6-6056e96f5e4e"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ColorControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9cbd9c3e-b153-49c6-8b73-149e58df4e05"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dialog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f7b31af-efa9-44a3-a0dd-bdebb4bcce6c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dialog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""247682fc-22be-4a3c-8e7a-f2a7d7ec46e5"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9cd0ccd1-48a5-4f6d-adb3-2d75495aac9f"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a48ce34f-a377-47c4-a6c7-95af636d5480"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""NextRangedPower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea48a095-9fee-483a-83e9-6a79fbf9a190"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""NextRangedPower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8dc2d19c-9c36-4fe0-86df-b28ca76026d5"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PreviousRangedPower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17b59f0b-bd7c-4f81-a674-4689159f789f"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PreviousRangedPower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_Jump = m_GamePlay.FindAction("Jump", throwIfNotFound: true);
        m_GamePlay_move = m_GamePlay.FindAction("move", throwIfNotFound: true);
        m_GamePlay_Pause = m_GamePlay.FindAction("Pause", throwIfNotFound: true);
        m_GamePlay_Dash = m_GamePlay.FindAction("Dash", throwIfNotFound: true);
        m_GamePlay_MeleeAttack = m_GamePlay.FindAction("MeleeAttack", throwIfNotFound: true);
        m_GamePlay_anykey = m_GamePlay.FindAction("anykey", throwIfNotFound: true);
        m_GamePlay_MoveLeft = m_GamePlay.FindAction("MoveLeft", throwIfNotFound: true);
        m_GamePlay_MoveRight = m_GamePlay.FindAction("MoveRight", throwIfNotFound: true);
        m_GamePlay_RangedAttack = m_GamePlay.FindAction("RangedAttack", throwIfNotFound: true);
        m_GamePlay_ColorControl = m_GamePlay.FindAction("ColorControl", throwIfNotFound: true);
        m_GamePlay_Dialog = m_GamePlay.FindAction("Dialog", throwIfNotFound: true);
        m_GamePlay_Map = m_GamePlay.FindAction("Map", throwIfNotFound: true);
        m_GamePlay_NextRangedPower = m_GamePlay.FindAction("NextRangedPower", throwIfNotFound: true);
        m_GamePlay_PreviousRangedPower = m_GamePlay.FindAction("PreviousRangedPower", throwIfNotFound: true);
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

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_Jump;
    private readonly InputAction m_GamePlay_move;
    private readonly InputAction m_GamePlay_Pause;
    private readonly InputAction m_GamePlay_Dash;
    private readonly InputAction m_GamePlay_MeleeAttack;
    private readonly InputAction m_GamePlay_anykey;
    private readonly InputAction m_GamePlay_MoveLeft;
    private readonly InputAction m_GamePlay_MoveRight;
    private readonly InputAction m_GamePlay_RangedAttack;
    private readonly InputAction m_GamePlay_ColorControl;
    private readonly InputAction m_GamePlay_Dialog;
    private readonly InputAction m_GamePlay_Map;
    private readonly InputAction m_GamePlay_NextRangedPower;
    private readonly InputAction m_GamePlay_PreviousRangedPower;
    public struct GamePlayActions
    {
        private @PlayerControls m_Wrapper;
        public GamePlayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_GamePlay_Jump;
        public InputAction @move => m_Wrapper.m_GamePlay_move;
        public InputAction @Pause => m_Wrapper.m_GamePlay_Pause;
        public InputAction @Dash => m_Wrapper.m_GamePlay_Dash;
        public InputAction @MeleeAttack => m_Wrapper.m_GamePlay_MeleeAttack;
        public InputAction @anykey => m_Wrapper.m_GamePlay_anykey;
        public InputAction @MoveLeft => m_Wrapper.m_GamePlay_MoveLeft;
        public InputAction @MoveRight => m_Wrapper.m_GamePlay_MoveRight;
        public InputAction @RangedAttack => m_Wrapper.m_GamePlay_RangedAttack;
        public InputAction @ColorControl => m_Wrapper.m_GamePlay_ColorControl;
        public InputAction @Dialog => m_Wrapper.m_GamePlay_Dialog;
        public InputAction @Map => m_Wrapper.m_GamePlay_Map;
        public InputAction @NextRangedPower => m_Wrapper.m_GamePlay_NextRangedPower;
        public InputAction @PreviousRangedPower => m_Wrapper.m_GamePlay_PreviousRangedPower;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @move.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @move.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @move.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Pause.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPause;
                @Dash.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnDash;
                @MeleeAttack.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMeleeAttack;
                @anykey.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAnykey;
                @anykey.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAnykey;
                @anykey.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAnykey;
                @MoveLeft.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMoveLeft;
                @MoveRight.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMoveRight;
                @RangedAttack.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnRangedAttack;
                @RangedAttack.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnRangedAttack;
                @RangedAttack.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnRangedAttack;
                @ColorControl.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnColorControl;
                @ColorControl.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnColorControl;
                @ColorControl.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnColorControl;
                @Dialog.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnDialog;
                @Dialog.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnDialog;
                @Dialog.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnDialog;
                @Map.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMap;
                @Map.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMap;
                @Map.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMap;
                @NextRangedPower.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnNextRangedPower;
                @NextRangedPower.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnNextRangedPower;
                @NextRangedPower.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnNextRangedPower;
                @PreviousRangedPower.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPreviousRangedPower;
                @PreviousRangedPower.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPreviousRangedPower;
                @PreviousRangedPower.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPreviousRangedPower;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @move.started += instance.OnMove;
                @move.performed += instance.OnMove;
                @move.canceled += instance.OnMove;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @MeleeAttack.started += instance.OnMeleeAttack;
                @MeleeAttack.performed += instance.OnMeleeAttack;
                @MeleeAttack.canceled += instance.OnMeleeAttack;
                @anykey.started += instance.OnAnykey;
                @anykey.performed += instance.OnAnykey;
                @anykey.canceled += instance.OnAnykey;
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @RangedAttack.started += instance.OnRangedAttack;
                @RangedAttack.performed += instance.OnRangedAttack;
                @RangedAttack.canceled += instance.OnRangedAttack;
                @ColorControl.started += instance.OnColorControl;
                @ColorControl.performed += instance.OnColorControl;
                @ColorControl.canceled += instance.OnColorControl;
                @Dialog.started += instance.OnDialog;
                @Dialog.performed += instance.OnDialog;
                @Dialog.canceled += instance.OnDialog;
                @Map.started += instance.OnMap;
                @Map.performed += instance.OnMap;
                @Map.canceled += instance.OnMap;
                @NextRangedPower.started += instance.OnNextRangedPower;
                @NextRangedPower.performed += instance.OnNextRangedPower;
                @NextRangedPower.canceled += instance.OnNextRangedPower;
                @PreviousRangedPower.started += instance.OnPreviousRangedPower;
                @PreviousRangedPower.performed += instance.OnPreviousRangedPower;
                @PreviousRangedPower.canceled += instance.OnPreviousRangedPower;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IGamePlayActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnMeleeAttack(InputAction.CallbackContext context);
        void OnAnykey(InputAction.CallbackContext context);
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnRangedAttack(InputAction.CallbackContext context);
        void OnColorControl(InputAction.CallbackContext context);
        void OnDialog(InputAction.CallbackContext context);
        void OnMap(InputAction.CallbackContext context);
        void OnNextRangedPower(InputAction.CallbackContext context);
        void OnPreviousRangedPower(InputAction.CallbackContext context);
    }
}
