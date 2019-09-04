using UnityEngine;
using UnityEngine.UI;
public class AimView : MonoBehaviour
{

    StopSystem st;
    Slider slider;
    public Transform parentObject;
    float z;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        st = GameManager.Instance.GetComponent<StopSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (st.SecondPhase)
        {
            Vector3 cursorPos = st.getCursorScreenPosition();
            Vector3 parentPos = Camera.main.WorldToScreenPoint(parentObject.position);
            if (parentPos.x > cursorPos.x)
            {
                z = -Vector3.Angle(Vector3.up, parentPos - cursorPos);
            }
            else
            {
                z = Vector3.Angle(Vector3.up, parentPos - cursorPos);
            }
            transform.localRotation = Quaternion.Euler(180, 180, z);
            float Value = Mathf.Clamp(Mathf.Log((cursorPos - parentPos).magnitude + 1), 0, 10);
            slider.value = Value;
            
        }
        else gameObject.SetActive(false);
    }
}
