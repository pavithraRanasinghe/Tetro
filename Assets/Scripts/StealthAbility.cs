using UnityEngine;

[CreateAssetMenu]
public class StealthAbility : Ability
{
    [SerializeField] private Sprite player;
    [SerializeField] private Sprite stealthPlayer;
    
    public override void Active(GameObject parent)
    {
        Player playerCom = parent.GetComponent<Player>();
        SpriteRenderer spriteRenderer = parent.GetComponent<SpriteRenderer>();
        ParticleSystem particleSystem = parent.GetComponent<ParticleSystem>();
        
        spriteRenderer.sprite = stealthPlayer;
        particleSystem.textureSheetAnimation.SetSprite(0, stealthPlayer);

        playerCom.isStealth = true;

    }

    public override void Cooldown(GameObject parent)
    {
        Player playerCom = parent.GetComponent<Player>();
        SpriteRenderer spriteRenderer = parent.GetComponent<SpriteRenderer>();
        ParticleSystem particleSystem = parent.GetComponent<ParticleSystem>();

        spriteRenderer.sprite = player;
        particleSystem.textureSheetAnimation.SetSprite(0, player);
        playerCom.isStealth = false;
    }
}
