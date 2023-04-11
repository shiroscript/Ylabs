using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Demo;
using Lightbug.CharacterControllerPro.Core;

public class PlayerController : MonoBehaviour
{
    [Header("Character")]

    [SerializeField]
    CharacterActor characterActor = null;

    [Header("Scene references")]

    [SerializeField]
    CharacterReferenceObject[] references = null;

    [Header("UI")]

    [SerializeField]
    Canvas infoCanvas = null;

    [SerializeField]
    bool hideAndConfineCursor = true;

    [Header("Camera")]

    [SerializeField]
    new Camera3D camera = null;

    [UnityEngine.Serialization.FormerlySerializedAs("frameRateText")]
    [SerializeField]
    UnityEngine.UI.Text targetFrameRateText = null;

    NormalMovement normalMovement = null;

    private float m_LastUpdateShowTime = 0f;  
    private float m_UpdateShowDeltaTime = 0.2f; 
    private int m_FrameUpdate = 0; 

    void Awake()
    {
        if (characterActor != null)
        {
            normalMovement = characterActor.GetComponentInChildren<NormalMovement>();
        }

        if (normalMovement != null && camera != null)
        {
            if (camera.cameraMode == Camera3D.CameraMode.FirstPerson)
                normalMovement.lookingDirectionParameters.lookingDirectionMode = LookingDirectionParameters.LookingDirectionMode.ExternalReference;
            else
                normalMovement.lookingDirectionParameters.lookingDirectionMode = LookingDirectionParameters.LookingDirectionMode.Movement;
        }

        Cursor.visible = !hideAndConfineCursor;
        Cursor.lockState = hideAndConfineCursor ? CursorLockMode.Locked : CursorLockMode.None;


        EventManager.Register("onEnterSquareSuccess", this, onEnterSquareSuccess);
    }

    void onEnterSquareSuccess()
    {
        camera.enabled = true;
    }

    private void OnDestroy()
    {
        EventManager.DeregisterAll(this);
    }

    private void Start()
    {
        m_LastUpdateShowTime = Time.realtimeSinceStartup;
        characterActor.gameObject.SetActive(true);
    }

    void Update()
    {
        for (int index = 0; index < references.Length; index++)
        {
            if (references[index] == null)
                break;

            if (Input.GetKeyDown(KeyCode.Alpha1 + index) || Input.GetKeyDown(KeyCode.Keypad1 + index))
            {
                GoTo(references[index]);
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (infoCanvas != null)
                infoCanvas.enabled = !infoCanvas.enabled;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            // If the Camera3D is present, change between First person and Third person mode.
            if (camera != null)
            {
                camera.ToggleCameraMode();

                if (normalMovement != null)
                {
                    if (camera.cameraMode == Camera3D.CameraMode.FirstPerson)
                        normalMovement.lookingDirectionParameters.lookingDirectionMode = LookingDirectionParameters.LookingDirectionMode.ExternalReference;
                    else
                        normalMovement.lookingDirectionParameters.lookingDirectionMode = LookingDirectionParameters.LookingDirectionMode.Movement;

                }

            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(Input.GetMouseButtonDown(0))
        {
            Cursor.visible = !hideAndConfineCursor;
            Cursor.lockState = hideAndConfineCursor ? CursorLockMode.Locked : CursorLockMode.None;
        }

        m_FrameUpdate++;
        if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime)
        {
            m_FrameUpdate = 0;
            m_LastUpdateShowTime = Time.realtimeSinceStartup;
            targetFrameRateText.text = ((int)(1 / Time.deltaTime)).ToString();
        }
    }

    void GoTo(CharacterReferenceObject reference)
    {
        if (reference == null)
            return;

        if (characterActor == null)
            return;

        characterActor.constraintUpDirection = reference.referenceTransform.up;
        characterActor.Teleport(reference.referenceTransform);

        characterActor.upDirectionReference = reference.verticalAlignmentReference;
        characterActor.upDirectionReferenceMode = VerticalAlignmentSettings.VerticalReferenceMode.Away;

    }
}
