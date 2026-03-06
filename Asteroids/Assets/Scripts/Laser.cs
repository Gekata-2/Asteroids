using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Transform laserObject;
    
    public bool IsEnabled { get; private set; }
    
    public void SetLength(float value)
    {
        Vector3 localScale = laserObject.localScale;
        localScale = new Vector3(localScale.x, value, localScale.z);
        laserObject.localScale = localScale;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
        gameObject.SetActive(false);
    }
}