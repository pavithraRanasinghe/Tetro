using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;
    
    public virtual void Active(GameObject parent){}
    public virtual void Cooldown(GameObject parent){}

}
