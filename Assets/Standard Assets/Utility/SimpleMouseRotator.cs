using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Utility
{
    public class SimpleMouseRotator : MonoBehaviour
    {
        [SerializeField]
        private float speed = 20;

        public Vector2 rotationRange = new Vector3(70, 70);
        public float rotationSpeed = 10;
        public float dampingTime = 0.2f;
        public bool autoZeroVerticalOnMobile = true;
        public bool autoZeroHorizontalOnMobile = false;
        public bool relative = true;


        private Vector3 m_TargetAngles;
        private Vector3 m_FollowAngles;
        private Vector3 m_FollowVelocity;
        private Quaternion m_OriginalRotation;

        private bool gyroEnabled;
        private Gyroscope gyro;
        private Quaternion rot;

        private GameObject cameraContainer;
#if MOBILE_INPUT    

        private bool EnableGyro()
        {
            if (SystemInfo.supportsGyroscope)
            {
                gyro = Input.gyro;
                gyro.enabled = enabled;

                transform.localRotation = Quaternion.Euler(90f, 90f, 0f);
                rot = new Quaternion(0, 0, 1, 0);
                return true;
            }

            return false;
        }

        private GameObject camParent;

        private void Start()
        {
            camParent = new GameObject("CamParent");
            camParent.transform.position = this.transform.position;
            this.transform.parent = camParent.transform;
            Input.gyro.enabled = true;
        }

        private void Update()
        {
            camParent.transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);
            transform.Rotate(-Input.gyro.rotationRateUnbiased.x, 0, 0);
        }
    }
#else
        private void Start()
        {
            m_OriginalRotation = transform.localRotation;
        }

        private void Update()
        {
            transform.localRotation = m_OriginalRotation;

            float inputH;
            float inputV;
            if (relative)
            {
                inputH = CrossPlatformInputManager.GetAxis("Mouse X");
                inputV = CrossPlatformInputManager.GetAxis("Mouse Y");

                // wrap values to avoid springing quickly the wrong way from positive to negative
                if (m_TargetAngles.y > 180)
                {
                    m_TargetAngles.y -= 360;
                    m_FollowAngles.y -= 360;
                }
                if (m_TargetAngles.x > 180)
                {
                    m_TargetAngles.x -= 360;
                    m_FollowAngles.x -= 360;
                }
                if (m_TargetAngles.y < -180)
                {
                    m_TargetAngles.y += 360;
                    m_FollowAngles.y += 360;
                }
                if (m_TargetAngles.x < -180)
                {
                    m_TargetAngles.x += 360;
                    m_FollowAngles.x += 360;
                }

                // with mouse input, we have direct control with no springback required.
                m_TargetAngles.y += inputH * rotationSpeed;
                m_TargetAngles.x += inputV * rotationSpeed;

                // clamp values to allowed range
                m_TargetAngles.y = Mathf.Clamp(m_TargetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f);
                m_TargetAngles.x = Mathf.Clamp(m_TargetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f);
            }
            else
            {
                inputH = Input.mousePosition.x;
                inputV = Input.mousePosition.y;

                // set values to allowed range
                m_TargetAngles.y = Mathf.Lerp(-rotationRange.y * 0.5f, rotationRange.y * 0.5f, inputH / Screen.width);
                m_TargetAngles.x = Mathf.Lerp(-rotationRange.x * 0.5f, rotationRange.x * 0.5f, inputV / Screen.height);
            }

            // smoothly interpolate current values to target angles
            m_FollowAngles = Vector3.SmoothDamp(m_FollowAngles, m_TargetAngles, ref m_FollowVelocity, dampingTime);

            // update the actual gameobject's rotation
            transform.localRotation = m_OriginalRotation * Quaternion.Euler(-m_FollowAngles.x, m_FollowAngles.y, 0);
        }
    }
#endif
}