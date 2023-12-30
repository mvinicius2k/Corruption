namespace Game;

public enum AttackKind
{
    /// <summary>
    /// Toda a força do ataque é desprezada. A defesa inimiga não se moverá
    /// </summary>
    None,
    /// <summary>
    /// A defesa inimiga sentirá o golpe, mas não será jogada para longe. Caso a força seja muito baixa, nem sequer se moverá
    /// </summary>
    Normal,
    /// <summary>
    /// O inimigo ficará desnorteado. Caso a força seja muito baixa, poderá não funcionar.
    /// </summary>
    Stun,
    /// <summary>
    /// O ataque é feito para jogar o inimigo longe. A força será levada em consideração
    /// </summary>
    Knockout,
}

