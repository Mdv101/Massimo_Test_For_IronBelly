using UnityEngine;

[CreateAssetMenu(menuName = "IronBellyTest/IronBellyTestSetting", fileName = "newIronBellyTestSetting")]
public class IronBellyTestSetting : ScriptableObject
{
    public float maxSearchDistance = 99;
    public AreaOfMove areaOfMove;
    public bool useLineRender;
    public int startPoolSize;
    public Neighbor prefabNeighbor;
}
