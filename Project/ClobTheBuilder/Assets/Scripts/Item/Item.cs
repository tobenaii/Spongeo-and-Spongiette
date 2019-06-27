﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private Grid m_grid;
    private List<Renderer> m_renderers;
    private bool m_isGhost;
    private bool m_inRedZone;
    private Rigidbody m_rb;
    public ItemSlot m_slot;
    private void OnEnable()
    {
        Ghostify();
    }

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_renderers = new List<Renderer>(GetComponentsInChildren<Renderer>());
    }

    private void Ghostify()
    {
        //Color defaultCol = m_renderer.material.color;
        //defaultCol.a = m_ghostAlpha;
        //m_renderer.material.color = defaultCol;
        m_isGhost = true;
    }

    private void Unghostify()
    {
        //Color defaultCol = m_renderer.material.color;
        //defaultCol.a = 1.0f;
        //m_renderer.material.color = defaultCol;
        m_isGhost = false;
        if (m_inRedZone)
        {
            m_slot.DestroyItem(gameObject);
        }
    }

    public virtual void OnEnter(GameObject gameObject)
    {

    }

    public virtual void OnStay(GameObject gameObject)
    {

    }

    public virtual void OnExit(GameObject gameObject)
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        m_inRedZone = true;
        foreach (Renderer rend in m_renderers)
        {
            rend.material.color = new Color(1, 0, 0);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        m_inRedZone = false;
        foreach (Renderer rend in m_renderers)
        {
            rend.material.color = new Color(1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isGhost)
            return;
        m_rb.MovePosition(m_grid.SnapToGrid(transform.position));
        if (Input.GetMouseButtonUp(0))
        {
            Unghostify();
        }
    }
}
