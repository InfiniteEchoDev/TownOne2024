using System.Collections;
using System.Collections.Generic;

using UnityEngine;



public enum CardinalDirection {
    North,
    East,
    South,
    West,
}
public class CardinalDirectionUtils
{
    public static CardinalDirection VectorToClosestCardinal( Vector2 vec ) {
        float dirAngle = Vector2.SignedAngle( Vector2.up, vec );

        dirAngle = Mathf.Floor( dirAngle / 90 ) * 90;

        if( dirAngle == 0 )
            return CardinalDirection.North;
        if( dirAngle == 90 ) 
            return CardinalDirection.East;
        if( dirAngle == -90 ) 
            return CardinalDirection.West;

        return CardinalDirection.South;
    }

    public static Vector2 CardinalToVector( CardinalDirection dir ) {
        switch( dir ) {
            case CardinalDirection.North:
                return Vector2.up;
            case CardinalDirection.East:
                return Vector2.right;
            case CardinalDirection.South:
                return Vector2.down;
            case CardinalDirection.West:
                return Vector2.left;
            default:
                return Vector2.up;
        }
    }
    public static float CardinalToSignedAngle( CardinalDirection dir ) {
        switch( dir ) {
            case CardinalDirection.North:
                return 0;
            case CardinalDirection.East:
                return 90;
            case CardinalDirection.South:
                return 180;
            case CardinalDirection.West:
                return -90;
            default:
                return 0;
        }
    }


}