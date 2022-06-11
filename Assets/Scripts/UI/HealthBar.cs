using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private Gradient gradient;

    [SerializeField]private Image fill;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int amount)
    {
        Debug.Log(transform.parent.root.name);
        slider.maxValue = amount;
        slider.value = amount;
        fill.color = gradient.Evaluate(1f);
    }


    public void SetHealth(int amount)
    {
        slider.value = amount;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
