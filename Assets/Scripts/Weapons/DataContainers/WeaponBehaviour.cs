using UnityEngine;

[CreateAssetMenu(menuName =("New weapon behaviour"))]
public class WeaponBehaviour : ScriptableObject {

    public string axis = "Fire1";

    public DirectionType direction = DirectionType.CAMERA;
    public DirectionOrigin directionOrigin = DirectionOrigin.CAMERA;
    public Vector3 originOffset = new Vector3(0, 0, 1);

    [Space(10)]

    public FiringMode firingMode = FiringMode.AUTOMATIC_BURST;

    public float recyclingTime = 0.15f;
    public float burstRecyclingTime = 0.2f;
    public int burstAmount = 3;


    public enum FiringMode
    {
        AUTOMATIC, SEMI_AUTOMATIC, BURST, AUTOMATIC_BURST
    }

    public enum DirectionType
    {
        FORWARD, CAMERA
    }

    public enum DirectionOrigin
    {
        TRANSFORM, CAMERA
    }

}
