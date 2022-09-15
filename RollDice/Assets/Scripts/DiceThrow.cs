using UnityEngine;

public class DiceThrow : MonoBehaviour
{

    private Rigidbody _rigidbody;
    public Vector3 DiceVelocity
    {
        get;
        private set; 
    }

    public delegate void OnClickHandler();
    public event OnClickHandler OnClick;

    private GameObject _gameController;
    private ShowAbility _showAbility;

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        
        _gameController = GameObject.FindWithTag("GameController");
        _showAbility = _gameController.GetComponent<ShowAbility>();
    }
    
    void Update()
    {
        DiceVelocity = _rigidbody.velocity;

        if (Input.GetMouseButtonDown(0))
        {
            OnClick += ThrowDice;
            OnClick += _showAbility.Show;
            
            OnClick?.Invoke();
            
            OnClick -= ThrowDice;
            OnClick -= _showAbility.Show;
        }
    }

    void ThrowDice()
    {
        float directionX = Random.Range(0f, 500f);
        float directionY = Random.Range(0f, 500f);
        float directionZ = Random.Range(0f, 500f);
        Vector3 torque = new Vector3(directionX, directionY, directionZ);

        transform.rotation = Quaternion.identity;
            
        _rigidbody.AddForce(transform.up * 500);
        _rigidbody.AddTorque(torque);
    }
    
}
