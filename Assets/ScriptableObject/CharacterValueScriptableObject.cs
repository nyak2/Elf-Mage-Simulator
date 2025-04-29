using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactersValues", menuName = "ScriptableObjects/CharacterValueScriptableObject", order = 1)]
public class CharacterValueScriptableObject : ScriptableObject
{
    [Header("Frieren")]

    public float minwanderTime;
    public float maxwanderTime;

    public float minidleTime;
    public float maxidleTime;

    public float attacktime;
    public float findtime;
    public float cryingtime;

    [Header("Mimic")]
    public float stucktime;
    public int MimicGrimoireLevelAmount;

    [Header("Grimoire")]
    public int NormalGrimoireLevelAmount;

    [Header ("Fern")]

    public float minwanderTimeFern;
    public float maxwanderTimeFern;

    public float minidleTimeFern;
    public float maxidleTimeFern;

    [Header ("Demon")]

    public float minwanderTimeDemon;
    public float maxwanderTimeDemon;

    public float minidleTimeDemon;
    public float maxidleTimeDemon;
}
