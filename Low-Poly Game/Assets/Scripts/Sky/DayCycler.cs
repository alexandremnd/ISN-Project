using UnityEngine;

public class DayCycler : MonoBehaviour
{
    [Header("Objects References")]
    [SerializeField] private Transform m_sunLight;

    [Header("Day Cycler Settings")]
    [SerializeField, ReadOnly] private string m_timeFormatted;
    [SerializeField, ReadOnly] private float m_time;
    [SerializeField, Tooltip("Set the hour and minute when the day starts.")] private Vector2 m_startTime = new Vector2(12, 0);
    [SerializeField, Tooltip("Include the length of day and night.")] private float m_dayLength;

    #region Methods|Time
    public Vector2 TimeToVector()
    {
        Vector2 time = new Vector2();
        time.x = Mathf.Floor(m_time);
        time.y = Mathf.Floor((m_time - time.x) * 30 / 0.5f);
        return time;
    }

    public string TimeToString()
    {
        Vector2 time = TimeToVector();
        return time.x + "h" + time.y + "m";
    }

    public float VectorToTime(Vector2 timeVector)
    {
        float time = timeVector.x;
        time += timeVector.y * 0.5f / 30;
        return time;
    }
    #endregion

    #region Methods|Rotation
    public float Rotation(float time)
    {
        // Cette équation a été déterminée manuellement afin de représenter l'angle en fonction du temps 
        // Dans une intervalle de 0 à 24, afin de pouvoir déterminer l'angle de manière immédiate
        float yAngle = -15 * time + 450;
        return yAngle;
    }
    #endregion

    private void Start()
    {
        m_time = VectorToTime(m_startTime);
        m_timeFormatted = TimeToString();
        m_sunLight.localRotation = Quaternion.Euler(0, Rotation(m_time), 0);
    }

    private void FixedUpdate ()
    {
        UpdateTime();
        UpdateRotation();
    }

    private void UpdateTime()
    {
        m_time += (Time.deltaTime * 24) / (m_dayLength * 60);
        m_time = (m_time > 24) ? 0 : m_time;
        m_timeFormatted = TimeToString();
    }

    private void UpdateRotation()
    {
        // On inverse la rotation en y (rotation anti-horaire).
        m_sunLight.Rotate(new Vector3(0, -(Time.deltaTime * 6) / m_dayLength, 0));
    }

    public void SetTime(float time)
    {
        m_time = time;
        m_sunLight.localRotation = Quaternion.Euler(0, Rotation(time), 0);
    }
}
