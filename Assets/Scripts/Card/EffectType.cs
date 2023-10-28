namespace ArcomageClone.Cards
{
    public enum EffectType
    {
        Damage,
        AddCastle, //negative to damage
        AddWall, //negative to damage
        BuildGain,
        Build,
        MightGain,
        Might,
        MagicGain,
        Magic,
        BuildEncampment, //+1 to attacks
        BuildForceField, //negate next attack
        GrowForest, //-1 to attacks, need +3 damage to remove
        DigMoat, //-2 to taken damage
        MagicWall //extra wall
    }
}
