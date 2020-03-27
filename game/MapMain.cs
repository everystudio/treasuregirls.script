using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMain : MonoBehaviour
{
    public GameObject m_goMapIcon;
    public GameObject m_goMapStart;
    public GameObject m_goMapGoal;

    public GameObject m_goLeftWall;
    public GameObject m_goRightWall;
    public GameObject m_goChara;


    // Update is called once per frame
    void Update()
    {
        float rate = (m_goChara.transform.position.x - m_goLeftWall.transform.position.x) / (m_goRightWall.transform.position.x - m_goLeftWall.transform.position.x);

        float posx = rate * (m_goMapGoal.transform.position.x - m_goMapStart.transform.position.x) + m_goMapStart.transform.position.x;

        m_goMapIcon.transform.position = new Vector3(
            posx,
            m_goMapIcon.transform.position.y,
            m_goMapIcon.transform.position.z
            );
    }
}
