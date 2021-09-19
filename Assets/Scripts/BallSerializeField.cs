using UnityEngine;

public class BallSerializeField : MonoBehaviour
{
    public Rigidbody2D Rb => rb;

    [SerializeField]
    private Rigidbody2D rb = null;
}
