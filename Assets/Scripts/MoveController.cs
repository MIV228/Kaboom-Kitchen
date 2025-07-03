using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    public Transform xrorigin;
    public Transform cam;
    public Transform orientation;
    public InputActionProperty moveAction;
    [SerializeField] private Rigidbody rb;
    public float speed;

    private Jump jumpScript;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpScript = GetComponent<Jump>();
    }

    public void Update()
    {
//        orientation.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
        //orientation.localRotation = new Quaternion(0, cam.localRotation.y, 0, 0);
        orientation.eulerAngles = new Vector3(0, cam.localEulerAngles.y + xrorigin.localEulerAngles.y, 0);
    }

    public void FixedUpdate()
    {
        ComputeDesiredMove();
    }

    [Space, Header("Movement Direction")]
    [SerializeField]
    [Tooltip("Directs the XR Origin's movement when using the head-relative mode. If not set, will automatically find and use the XR Origin Camera.")]
    Transform m_HeadTransform;

    public Transform headTransform
    {
        get => m_HeadTransform;
        set => m_HeadTransform = value;
    }

    [SerializeField]
    [Tooltip("Directs the XR Origin's movement when using the hand-relative mode with the left hand.")]
    Transform m_LeftControllerTransform;

    /// <summary>
    /// Directs the XR Origin's movement when using the hand-relative mode with the left hand.
    /// </summary>
    public Transform leftControllerTransform
    {
        get => m_LeftControllerTransform;
        set => m_LeftControllerTransform = value;
    }

    [SerializeField]
    [Tooltip("Directs the XR Origin's movement when using the hand-relative mode with the right hand.")]
    Transform m_RightControllerTransform;

    public Transform rightControllerTransform
    {
        get => m_RightControllerTransform;
        set => m_RightControllerTransform = value;
    }

    Transform m_CombinedTransform;
    Pose m_LeftMovementPose = Pose.identity;
    Pose m_RightMovementPose = Pose.identity;

    /// <inheritdoc />
    void Awake()
    {
        m_CombinedTransform = new GameObject("[Dynamic Move Provider] Combined Forward Source").transform;
        m_CombinedTransform.SetParent(transform, false);
        m_CombinedTransform.localPosition = Vector3.zero;
        m_CombinedTransform.localRotation = Quaternion.identity;
    }

    /// <inheritdoc />
    protected void ComputeDesiredMove()
    {
        if (m_HeadTransform == null)
        {
            var xrOrigin = transform;
            if (xrOrigin != null)
            {
                var xrCamera = cam;
                if (xrCamera != null)
                    m_HeadTransform = xrCamera.transform;
            }
        }

        // Get the forward source for the left hand input
        if (m_HeadTransform != null)
            m_LeftMovementPose = m_HeadTransform.GetWorldPose();

        // Get the forward source for the right hand input
        if (m_HeadTransform != null)
            m_RightMovementPose = m_HeadTransform.GetWorldPose();

        // Combine the two poses into the forward source based on the magnitude of input
        var leftHandValue = moveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;

        var leftHandBlend = 0.5f;

        var combinedPosition = Vector3.Lerp(m_RightMovementPose.position, m_LeftMovementPose.position, leftHandBlend);
        var combinedRotation = Quaternion.Slerp(m_RightMovementPose.rotation, m_LeftMovementPose.rotation, leftHandBlend);
        m_CombinedTransform.rotation = combinedRotation;

        if (jumpScript.isGrounded)
            rb.AddForce(orientation.right * leftHandValue.x * speed + orientation.forward * leftHandValue.y * speed, ForceMode.Force);
        else
            rb.AddForce(orientation.right * leftHandValue.x * speed * 0.1f + orientation.forward * leftHandValue.y * speed * 0.1f, ForceMode.Force);
    }
}
