using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private float m_MoveSpeed;
    private Transform m_TargetTransform;
    private ZombieState m_ZombieState = ZombieState.Inactive;
    public ZombieState State {get {return m_ZombieState;}}

    public void Deactivate()
    {
        m_ZombieState = ZombieState.Inactive;
        gameObject.SetActive(false);
    }

    public void Activate(Transform targetTransform, Vector3 initialPos, float moveSpeed=40.0f)
    {
        transform.position = initialPos;
        m_TargetTransform = targetTransform;
        m_MoveSpeed = moveSpeed;
        gameObject.SetActive(true);
        m_ZombieState = ZombieState.ActiveWalking;
        transform.LookAt(m_TargetTransform);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_ZombieState==ZombieState.ActiveWalking)
        {
            float step = m_MoveSpeed * Time.deltaTime;
            
            transform.position = Vector3.MoveTowards(transform.position, m_TargetTransform.position, step);
            if(Vector3.Distance(transform.position, m_TargetTransform.position)<0.001f)
            {
                Deactivate();
            }
        }
    }
}
