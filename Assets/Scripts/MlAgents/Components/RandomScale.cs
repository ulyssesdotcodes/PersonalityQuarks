using UnityEngine;
public class RandomScale : MonoBehaviour
{
    public void Start()
    {
        transform.localScale.Scale(new Vector3(
            Random.value,
            Random.value,
            Random.value
        ));
    }
}