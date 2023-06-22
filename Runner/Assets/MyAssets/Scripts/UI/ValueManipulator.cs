using HyperCasual.Runner;
using UnityEngine;

public class ValueManipulator : MonoBehaviour
{
    public static ValueManipulator Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void InitInputs()
    {
        PlayerController.Instance.m_HorizontalSpeedFactor = float.Parse(PauseMenu.Instance.input.text);
        PlayerController.Instance.m_TargetSpeed = float.Parse(PauseMenu.Instance.input2.text);
    }

    public void SaveInputs()
    {
        SaveManager.Instance.HorizontalSpeed = PlayerController.Instance.m_HorizontalSpeedFactor;
        SaveManager.Instance.NormalSpeed = PlayerController.Instance.m_TargetSpeed;
    }
}
