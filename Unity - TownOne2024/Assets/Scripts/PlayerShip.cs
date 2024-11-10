using System.Collections;
using System.Collections.Generic;

using UnityEngine;



public class PlayerShip : MonoBehaviour
{
    [Range( 0, 20 )]
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

        _rigidbody.linearVelocity = transform.up * Velocity;
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
        Vector2 newVel = transform.up * Velocity;


        //float divDist = ( Mathf.Abs( Vector2.Dot( transform.position, _rigidbody.linearVelocity ) ) ) / StarFieldMgr.Instance.GridSize;
        //float divProjDist = ( Mathf.Abs( Vector2.Dot( transform.position, _rigidbody.linearVelocity ) ) + ( _rigidbody.linearVelocity.magnitude * Time.fixedDeltaTime ) ) / StarFieldMgr.Instance.GridSize;

        ////Debug.Log( $"dist: {distFromOriginInVelDir}, projdist: {projDistFromOriginInVelDir}, divdist: {divDist}, divProj:{divProjDist}, next:{nextFrameDistTrav}" );
        //Debug.Log( $"divdist: {divDist}, divProj:{divProjDist}" );


        if( _rigidbody.linearVelocity == newVel ) return;

 
        //if( Mathf.FloorToInt( divDist ) != Mathf.FloorToInt( divProjDist ) ) {
            _rigidbody.linearVelocity = newVel;
        //}
    }

    public void Move( Vector2 moveDir ) {
        if( moveDir == Vector2.zero )
            return;

        CardinalDirection newDirection = CardinalDirectionUtils.VectorToClosestCardinal( moveDir );
        _currentCardinalHeading = newDirection;

        _rigidbody.SetRotation( CardinalDirectionUtils.CardinalToSignedAngle( _currentCardinalHeading ) );
    }

}