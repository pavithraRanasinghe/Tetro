using UnityEngine;

[CreateAssetMenu]
public class ScoreAbility : Ability
{
    
    public override void Active(GameObject parent)
    {
        
        Player player = parent.GetComponent<Player>();
        SpriteRenderer spriteRenderer = parent.GetComponent<SpriteRenderer>();

        player.isActiveScoreAbility = true;
        spriteRenderer.color = Color.blue;

    }

    public override void Cooldown(GameObject parent)
    {
        Player player = parent.GetComponent<Player>();
        SpriteRenderer spriteRenderer = parent.GetComponent<SpriteRenderer>();
        
        player.isActiveScoreAbility = false;
        spriteRenderer.color = new Color(255,198,0);
    }
}
