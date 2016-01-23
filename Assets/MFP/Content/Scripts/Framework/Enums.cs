namespace DestroyIt
{
    public enum Tag : int
    {
        ClingingDebris = 0,
        ClingPoint = 7,
        Concrete = 1,
        Glass = 2,
        MaterialTransferred = 3,
        Metal = 4,
        Paper = 5,
        Wood = 6,
        Powered = 8,
        Pooled = 9,
        Untagged = 10,
        DestructibleGroup = 11,
        Rubber = 12,
        Stuffing = 13
    }

    public enum FacingDirection
    {
        None,
        FollowedObject,
        FixedPosition
    }

    public enum WeaponType : int
    {
        Cannonball = 0,
        Rocket = 1,
        Gun,
        Nuke,
        Melee
    }

    public enum MoveDirection
    {
        Up,
        Down
    }

    public enum DestructionType : int
    {
        //TODO: Possible future limited destruction types for things beyond specified camera distance:
        //None,
        //FiftyPercentDebris,
        ParticleEffect = 0
    }
}