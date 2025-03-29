using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]

public class ProjectileHero : MonoBehaviour
{
    private BoundsCheck bndCheck;
    private Renderer rend;
    [Header("Dynamic")]
    public Rigidbody rigid;
    [SerializeField] 
    private eWeaponType _type;
    float birthTime;
    float x0;
    public eWeaponType type
    {
        get { return _type; }
        set{
            SetType(value);
        }
    }

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }
    void Start(){
        x0 = transform.position.x;
        birthTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
       
        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offUp))
        {
            Destroy(gameObject);
            return;
        }
       if (type == eWeaponType.phaser) {
            SinusoidMotion();
        }

    }
    private void SinusoidMotion()
    {
        WeaponDefinition def = Main.GET_WEAPON_DEFINTION(type);
        float amplitude = def.waveAmplitude; // Use amplitude from WeaponDefinition
        float frequency = def.waveFrequency; // Use frequency from WeaponDefinition

        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * frequency * age; // Full sine wave cycle
        float sinOffset = amplitude * Mathf.Sin(theta);

        // Apply sine wave to the x-axis while maintaining upward velocity
        Vector3 newPos = transform.position;
        newPos.x = x0 + sinOffset;
        transform.position = newPos;
    }
    public void SetType(eWeaponType eType)
    {
        _type = eType;
        WeaponDefinition def = Main.GET_WEAPON_DEFINTION(_type);
        rend.material.color = def.projectileColor;
    }
    public Vector3 vel{
        get{ return rigid.linearVelocity; }
        set{ rigid.linearVelocity = value; }
    }
}
