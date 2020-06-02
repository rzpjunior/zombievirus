using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPointer : MonoBehaviour
{
    [SerializeField]
    private RectTransform m_CrossHair;
    
    void Start()
    {
        // Vector3 pos = m_mainCamera.ViewportToWorldPoint(m_CrossHair.position);
        // transform.position = m_mainCamera.WorldToViewportPoint(pos);
        // transform.LookAt(transform.position - (m_mainCamera.transform.position-transform.position));

        // transform.rotation = Quaternion.LookRotation(transform.position, mainCamera.transform.position);
    }

}
