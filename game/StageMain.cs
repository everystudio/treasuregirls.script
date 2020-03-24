using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMain : MonoBehaviour
{
    public GameObject m_goRoot;
    public GameObject m_prefStageBlock;

    private int pitch = 1;
    void Start(){
        /*
        for( int i = 0 ; i < 20 ; i++){
            GameObject block = PrefabManager.Instance.MakeObject(m_prefStageBlock , m_goRoot);
            block.transform.localPosition = new Vector3(i*pitch,-2,0);
        }
        */

    }
}
