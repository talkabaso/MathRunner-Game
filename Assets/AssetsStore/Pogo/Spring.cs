using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spring : MonoBehaviour {
    [SerializeField] float _powerMultiplier = 5f;
    [SerializeField] float _springStiffness = 5f;
    [SerializeField] Rigidbody _pogoStickBody;

    void Start()
    {}

    private void OnCollisionStay(Collision collision)
    {
        UpdateExpand(collision);
    }

    private void UpdateExpand(Collision collision)
    {
        var gravitationalForce = _pogoStickBody.mass * Physics.gravity;
        var forceUp = -gravitationalForce * _springStiffness * _powerMultiplier;
        var contact = collision.contacts[0];
        var normal = contact.normal;
        var force = Vector3.Dot(forceUp, (_pogoStickBody.transform.up + normal).normalized) * (_pogoStickBody.transform.up + normal).normalized;
        _pogoStickBody.AddForce(force);
    }
}