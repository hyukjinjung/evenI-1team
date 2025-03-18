using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Idle,
    Cinematic,
    Playing,
    GameOver
}


public enum CharacterState
{
    Normal,         // �Ϲ� ����
    Transformation, // ���� ����
    Fever,          // �ǹ� ��� ����
}

public enum TransformationType
{
    NormalFrog,     // �⺻ ������ 
    NinjaFrog,      // ���� ������ 
    BullFrog,       // Ȳ�� ������ 
    LotusFrog,      // ���� ������ 
    FlyingFrog,     // ���� ������ 
    MusicianFrog,   // �ǻ� ������ 
    MagicianFrog,   // ���� ������ 
    GoldenFrog,     // Ȳ�� ������ 
}

public enum ItemType
{
    Coin,
    Transformation,
    Consumable
}

public enum TransformationItemType
{
    Ninja,
    Bull,
    Lotus,
    Flying,
    Musician,
    Masician
}

public enum AddScoreConditionType
{
    OnKill,
    OnTransformEnd,
    OnSpecialAbilityUse,
    GettingItems
}

public enum MonsterType
{
    Fly,
    Spider,
    Butterfly,
    Dragonfly,
    Mantis
}

public enum AddScoreTargetType
{
    None = 0,               // ����
    Item = 1,               // ������
    Monster = 2,            // ����
    ObstacleItem = 3,       // ���� ������
    Coin = 1,               // ����
    ObstaclePattern = 5,    // ���� ����
    Movement = 6,           // �̵�
    SpecialKeyUse = 7,      // Ư��Ű ���
    Fall = 8,               // �߶�
    TrackingMonster = 9     // ���� ����
}

public enum TileType
{
    NormalTile,
    MonsterTile,
    ObstacleTile,
    ItemTile,
}

public enum FeverScoreType
{
    Movement,
    FeverCoin,
    MonsterKill,
    ItemUse,
    FeverReductionCoin,
    TestCoin
}

public enum ChasingMonsterDistanceState
{
    Far,
    Medium,
    Close
}

public enum SpecialAbilityType
{
    None,
    NinjaAssassination,
    BullSwallowAndSpit
}

public enum BGM
{   
   
    LobbyBGM,               // Lobby background music

    // StageSetupBGM
    // Stage setup background music
    PreGameCutscene,        // Pre-game cutscene
    GameProgress,           // In-game progress ���� ���� 
    FeverMap,               // Fever map event
}
public enum SoundEffectType
{
    LobbyButtonTouch,       // Lobby button touch effect

    // Game Start
    GameStart,              // Game start event
    
    // Chasing Monster
    ChasingMonster,         // Chasing monster appears �������� ���� 
    ChaseDistanceWarning,   // Chase distance warning �����Ÿ� ����� ��� 

    // Platform ���� 
    ItemAcquisition,        // Item acquisition 
    CurrencyItemAcquisition,// Currency item acquisition ��ȭ ������ ȹ��
    MonsterHit,             // Monster hit ���� Ÿ�� 
    MonsterDefeat,          // Monster defeat ���� ��ġ 
    DisruptiveItemAcquisition, // Disruptive item acquisition ���� ������ 

    // Pattern ���� ���� 
    PatternAppearanceWarning, // Pattern appearance warning ���� ���� ��� 
    PatternAttack,            // Pattern attack ���� ���� 

    // Default Character �⺻ ĳ����
    CharacterJump,          // Character jump ���� 
    CharacterAttack,        // Character attack ���� 
    GameOverDefenseRelease, // Game over defense transformation release ���� ���� 
    GameOver,               // Game over event ���� ���� 
    Resurrection,           // Resurrection event ��Ȱ

    // Transformation Effects
    Transformation,          // Transformation event ���� 
    TransformationReleaseWarning, // Transformation release warning ���� ���� �ð� ��� 
    TransformationTimeOut,  // Transformation release due to time/usage limits
    NinjaJump,              // Ninja jump ���� ���� 
    NinjaAttack,            // Ninja attack ���� ���� 
    BullJump,               // Bull jump Ȳ�� ������ ���� 
    BullMonsterSwallowAttack,  // Bull monster swallowing attack Ȳ�� ������ ���� ��Ű�� ���� 
    BullMonsterSpitAttack,     // Bull monster spit attack Ȳ�� ������ ���� ��� ���� 
    LotusMovement,          // Lotus movement �����̵� 
    LotusMagic,             // Lotus magic ���� ���� 
    GoldenResurrection,     // Golden resurrection Ȳ�� ��Ȱ 

    // Fever
    FeverMapMovement,       // Fever map movement �ǹ��� �̵� 

    // Result Screen
    CoutnueGame,             // Resume game �̾��ϱ� 
    ScoreResult,            // Score result ���� ��� 
    HighScoreAchievement,   // High score / Achievement / Ranking increase �ְ��� ���� ��ŷ ��� 
}


public enum BGMType
{
    None,
    LobbyBGM,               // Lobby background music
    PreGameCutscene,        // Pre-game cutscene
    GameProgress,           // In-game progress ���� ���� 
    FeverMap,               // Fever map event
}


public enum BtnType
{
  SettingInstagrambutton,
  SettingSourceButton,
  SettingSoundEffectButton,
  SettingBGMButton,
  SettingReturnButton,
  PauseMainHomeButton,
  PauseContinueButton,
  PauseBGMButton,
  PauseSoundEffectButton,
  CautionMainHomeButton,
  CautionContinueButton,
  AdsComeBackButton,
  SkipButton,
  ResultScoreTwiceButton,
  ResultReStartButton,
  ResultMainHomeButton,
  ResultCoinTwiceButton,
  StoryReTurnButton,
  StoryButton,
  CreditsReTurnButton,

}





public class EnumData : MonoBehaviour
{

}

