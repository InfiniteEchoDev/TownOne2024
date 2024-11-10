using System.Collections;
using System.Collections.Generic;

using UnityEngine;



public class PlayerShip : MonoBehaviour
{
    [Range( 0, 2 )]
    public float Velocity = .25f;
    private float _lastMoveTime = 0;


    [Header( "Obj Refs" )]
    public Sprite ShipSprite;
    public GameObject AttachPoint;

    // Events
    public event System.Action<Vector2> OnMove; // Takes move delta Vec2 as argument

    // Private Components
    private Rigidbody2D _rigidbody;

    // Private vars

    private CardinalDirection _currentCardinalHeading;
    //private CardinalDirection? _nextHeading = null;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.linearVelocity = transform.up / Velocity;
    }

    private void FixedUpdate() {
        //if( _lastMoveTime + MoveCooldownPeriod <= Time.time ) {


        processVelocity();

        //_rigidbody.MovePosition( transform.position + transform.up );

        //    OnMove?.Invoke( transform.up );

        //    _lastMoveTime = Time.time;
        //}
    }

    public void processVelocity() {
        Vector2 newVel = transform.up / Velocity;
        if( _rigidbody.linearVelocity == newVel ) return;

        float distFromOriginInVelDir = Vector2.Dot( transform.position, _rigidbody.linearVelocity );
        float modDist = ( Vector2.Dot( transform.position, _rigidbody.linearVelocity ) + _rigidbody.linearVelocity.magnitude * Time.fixedDeltaTime ) % StarFieldMgr.Instance.GridSize;
        float nextFrameDistTrav = _rigidbody.linearVelocity.magnitude * Time.fixedDeltaTime;
        Debug.Log( $"dist: {distFromOriginInVelDir}, moddist: {modDist}, next:{nextFrameDistTrav}" );

        if( ( Vector2.Dot( transform.position, _rigidbody.linearVelocity ) + _rigidbody.linearVelocity.magnitude * Time.fixedDeltaTime ) % StarFieldMgr.Instance.GridSize < _rigidbody.linearVelocity.magnitude * Time.fixedDeltaTime ) {
            _rigidbody.linearVelocity = transform.up / Velocity;
        }
    }

    public void Move( Vector2 moveDir ) {
        if( moveDir == Vector2.zero )
            return;

        CardinalDirection newDirection = CardinalDirectionUtils.VectorToClosestCardinal( moveDir );
        _currentCardinalHeading = newDirection;

        _rigidbody.SetRotation( CardinalDirectionUtils.CardinalToSignedAngle( _currentCardinalHeading ) );
    }

}