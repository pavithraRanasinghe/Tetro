using UnityEngine;

[CreateAssetMenu]
public class StealthAbility : Ability
{
    public override void Active(GameObject parent)
    {
        Player player = parent.GetComponent<Player>();
        SpriteRenderer spriteRenderer = parent.GetComponent<SpriteRenderer>();
        
        spriteRenderer.color = Color.white;
        player.isStealth = true;

    }

    public override void Cooldown(GameObject parent)
    {
        Player player = parent.GetComponent<Player>();
        SpriteRenderer spriteRenderer = parent.GetComponent<SpriteRenderer>();

        player.isStealth = false;
        spriteRenderer.color = new Color(255,198,0);
    }
}
