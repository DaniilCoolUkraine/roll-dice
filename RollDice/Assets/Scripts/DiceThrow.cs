using UnityEngine;

public class DiceThrow : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public Vector3 DiceVelocity
    {
        get;
        private set; 
    }
    
    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        DiceVelocity = _rigidbody.velocity;
    }

    public void ThrowDice()
    {
        float directionX = Random.Range(0f, 500f);
        float directionY = Random.Range(0f, 500f);
        float directionZ = Random.Range(0f, 500f);
        Vector3 torque = new Vector3(directionX, directionY, directionZ);

        transform.rotation = Quaternion.identity;
            
        _rigidbody.AddForce(gameObject.transform.up * 500);
        _rigidbody.AddTorque(torque);
    }
    
}
