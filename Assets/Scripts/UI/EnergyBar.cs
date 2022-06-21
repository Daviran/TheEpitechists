using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxEnergy(int energy)
    {
        slider.maxValue = 100;
        slider.value = energy;
    }

    public void SetEnergy(float energy)
    {
        slider.value += energy;
    }
}
