using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharaMain : Singleton<GameCharaMain>
{
    public ArrowButton m_arrowLeft;
    public ArrowButton m_arrowRight;

    public SpriteRenderer m_sprChara;

    public Rigidbody2D rb2d;
    public BoxCollider2D collidor2d;

    public CharaBody m_charaBody;

    public float move_power = 1.0f;
    public float gravity = 20.0f;

    public bool is_move;

}
