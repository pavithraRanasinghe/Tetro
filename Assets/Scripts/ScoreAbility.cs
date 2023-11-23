using UnityEngine;

[CreateAssetMenu]
public class ScoreAbility : Ability
{
    [SerializeField] private Sprite player;
    [SerializeField] private Sprite scorePlayer;

    public override void Active(GameObject parent)
    {
        Player playerCom = parent.GetComponent<Player>();
        SpriteRenderer spriteRenderer = parent.GetComponent<SpriteRenderer>();
        ParticleSystem particleSystem = parent.GetComponent<ParticleSystem>();
        particleSystem.textureSheetAnimation.SetSprite(0, scorePlayer);

        playerCom.isActiveScoreAbility = true;
        spriteRenderer.sprite = scorePlayer;
    }

    public override void Cooldown(GameObject parent)
    {
        Player playerCom = parent.GetComponent<Player>();
        SpriteRenderer spriteRenderer = parent.GetComponent<SpriteRenderer>();
        ParticleSystem particleSystem = parent.GetComponent<ParticleSystem>();
        particleSystem.textureSheetAnimation.SetSprite(0, player);

        playerCom.isActiveScoreAbility = false;
        spriteRenderer.sprite = player;
    }
}