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
    Normal,         // 일반 상태
    Transformation, // 변신 상태
    Fever,          // 피버 모드 상태
}

public enum TransformationType
{
    NormalFrog,     // 기본 개구리 
    NinjaFrog,      // 닌자 개구리 
    BullFrog,       // 황소 개구리 
    LotusFrog,      // 연꽃 개구리 
    FlyingFrog,     // 비행 개구리 
    MusicianFrog,   // 악사 개구리 
    MagicianFrog,   // 마술 개구리 
    GoldenFrog,     // 황금 개구리 
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
    None = 0,               // 없음
    Item = 1,               // 아이템
    Monster = 2,            // 몬스터
    ObstacleItem = 3,       // 방해 아이템
    Coin = 1,               // 코인
    ObstaclePattern = 5,    // 방해 패턴
    Movement = 6,           // 이동
    SpecialKeyUse = 7,      // 특수키 사용
    Fall = 8,               // 추락
    TrackingMonster = 9     // 추적 몬스터
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
    GameProgress,           // In-game progress 게임 진행 
    FeverMap,               // Fever map event
}
public enum SoundEffectType
{
    LobbyButtonTouch,       // Lobby button touch effect

    // Game Start
    GameStart,              // Game start event
    
    // Chasing Monster
    ChasingMonster,         // Chasing monster appears 추적몬스터 등장 
    ChaseDistanceWarning,   // Chase distance warning 추적거리 가까움 경고 

    // Platform 발판 
    ItemAcquisition,        // Item acquisition 
    CurrencyItemAcquisition,// Currency item acquisition 재화 아이템 획득
    MonsterHit,             // Monster hit 몬스터 타격 
    MonsterDefeat,          // Monster defeat 몬스터 퇴치 
    DisruptiveItemAcquisition, // Disruptive item acquisition 방해 아이템 

    // Pattern 방해 패턴 
    PatternAppearanceWarning, // Pattern appearance warning 패턴 등장 경고 
    PatternAttack,            // Pattern attack 패턴 공격 

    // Default Character 기본 캐릭터
    CharacterJump,          // Character jump 점프 
    CharacterAttack,        // Character attack 공격 
    GameOverDefenseRelease, // Game over defense transformation release 변신 해제 
    GameOver,               // Game over event 게임 오버 
    Resurrection,           // Resurrection event 부활

    // Transformation Effects
    Transformation,          // Transformation event 변신 
    TransformationReleaseWarning, // Transformation release warning 변신 해재 시간 경고 
    TransformationTimeOut,  // Transformation release due to time/usage limits
    NinjaJump,              // Ninja jump 닌자 점프 
    NinjaAttack,            // Ninja attack 닌자 공격 
    BullJump,               // Bull jump 황소 개구리 점프 
    BullMonsterSwallowAttack,  // Bull monster swallowing attack 황소 개구리 몬스터 삼키는 공격 
    BullMonsterSpitAttack,     // Bull monster spit attack 황소 개구리 몬스터 뱉는 공격 
    LotusMovement,          // Lotus movement 연곷이동 
    LotusMagic,             // Lotus magic 연꽃 마법 
    GoldenResurrection,     // Golden resurrection 황금 부활 

    // Fever
    FeverMapMovement,       // Fever map movement 피버맵 이동 

    // Result Screen
    CoutnueGame,             // Resume game 이어하기 
    ScoreResult,            // Score result 점수 결과 
    HighScoreAchievement,   // High score / Achievement / Ranking increase 최고기록 업적 랭킹 상승 
}


public enum BGMType
{
    None,
    LobbyBGM,               // Lobby background music
    PreGameCutscene,        // Pre-game cutscene
    GameProgress,           // In-game progress 게임 진행 
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

