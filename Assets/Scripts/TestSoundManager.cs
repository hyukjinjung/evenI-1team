using UnityEditor;
using UnityEngine;

public class TestSoundManager : MonoBehaviour
{
    public static TestSoundManager testSoundManager;    
    public static TestSoundManager Instance;

    public AudioSource soundEffectAudioSource; 
    public AudioSource BGMAudioSource; 

    public static BGMType plyingBGM = BGMType.None;


    public AudioClip LobbyBGM;  // Lobby background music
    public AudioClip GameProgress; // In-game progress 게임 진행
                                   //public AudioClip PreGameCutscene; // Pre-game cutscene
                                   //public AudioClip FeverMap;

    public AudioClip GameStartButton;

    // Chasing Monster
    public AudioClip ChasingMonster;                // Chasing monster appears 추적몬스터 등장 
    public AudioClip ChaseDistanceWarning;          // Chase distance warning 추적거리 가까움 경고 

    // Platform 발판 
    public AudioClip ItemAcquisition;               // Item acquisition 
    public AudioClip CurrencyItemAcquisition;       // Currency item acquisition 재화 아이템 획득
    public AudioClip MonsterHit;                    // Monster hit 몬스터 타격 
    public AudioClip MonsterDefeat;                 // Monster defeat 몬스터 퇴치 
    public AudioClip DisruptiveItemAcquisition;     // Disruptive item acquisition 방해 아이템

    // Pattern 방해 패턴
    public AudioClip PatternAppearanceWarning;      // Pattern appearance warning 패턴 등장 경고 
    public AudioClip PatternAttack;                 // Pattern attack 패턴 공격 

    // Default Character 기본 캐릭터
    public AudioClip CharacterJump;                 // Character jump 점프 
    public AudioClip CharacterAttack;               // Character attack 공격 
    public AudioClip GameOverDefenseRelease;        // Game over defense transformation release 변신 해제 
    public AudioClip GameOver;                      // Game over event 게임 오버 
    public AudioClip Resurrection;                  // Resurrection event 부활

    // Transformation Effects
    public AudioClip Transformation;                // Transformation event 변신 
    public AudioClip TransformationReleaseWarning;  // Transformation release warning 변신 해재 시간 경고 
    public AudioClip TransformationTimeOut;         // Transformation release due to time/usage limits
    public AudioClip NinjaJump;                     // Ninja jump 닌자 점프 
    public AudioClip BullJump;                      // Bull jump 황소 개구리 점프 
    public AudioClip BullMonsterSwallowAttack;      // Bull monster swallowing attack 황소 개구리 몬스터 삼키는 공격 
    public AudioClip BullMonsterSpitAttack;         // Bull monster spit attack 황소 개구리 몬스터 뱉는 공격 
    public AudioClip LotusMovement;                 // Lotus movement 연곷이동 
    public AudioClip LotusMagic;                    // Lotus magic 연꽃 마법 
    public AudioClip GoldenResurrection;            // Golden resurrection 황금 부활 

    // Fever
    public AudioClip FeverMapMovement;              // Fever map movement 피버맵 이동 

    // Result Screen
    public AudioClip CoutnueGame;                   // Resume game 이어하기 
    public AudioClip ScoreResult;                   // Score result 점수 결과 
    public AudioClip HighScoreAchievement;          // High score / Achievement / Ranking increase 최고기록 업적 랭킹 상승 

   
    private void Awake()
    {
        //BGM 재생
        if (testSoundManager == null)
        {
            testSoundManager = this;  //static 변수에 자기 자신을 저장
                                  //씬이 바뀌어도 게임 오브젝트를 파기하지 않음
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//게임 오브젝트르 파기
        }
    }

    public void PlayBgm(BGMType type)
    {
        if (type != plyingBGM)
        {
            plyingBGM = type;
            AudioSource audio = GetComponent<AudioSource>();
            if (type == BGMType.LobbyBGM)
            {
                audio.clip = LobbyBGM;   
            }
            else if (type == BGMType.GameProgress)
            {
                audio.clip = GameProgress;    
            }
           
            audio.Play();
        }
    }

    public void StopBgm()
    {
        GetComponent<AudioSource>().Stop();
        plyingBGM = BGMType.None;
    }

    public void SEPlay(SoundEffectType type)
    {
        if (type == SoundEffectType.ChasingMonster)
        {
            GetComponent<AudioSource>().PlayOneShot(ChasingMonster);   //게임 클리어
        }
        else if (type == SoundEffectType.ChaseDistanceWarning)
        {
            GetComponent<AudioSource>().PlayOneShot(ChaseDistanceWarning);   //게임 오버
        }
        else if (type == SoundEffectType.ItemAcquisition)
        {
            GetComponent<AudioSource>().PlayOneShot(ItemAcquisition);       //활 쏘기
        }
    }

    public void PlayButtonClickGameStart()
    {
        soundEffectAudioSource.PlayOneShot(GameStartButton);
    }









    /*public static TestSoundManager Instance;

    //public AudioSource bgmAudioSource;
    //public AudioSource soundEffectAudioSource;
    

    [Header("========BGMAudioSource========")]

    public AudioSource bgmAudioSource;
   
    public AudioSource startPanelBGM;
    public AudioSource playingPanelBGM;
    //public AudioSource tutorialPanelBGM;
    //public AudioSource feverPanelBGM;

    [Header("========SoundEffectAudioSource========")]
    
    public AudioSource soundEffectAudioSource;

    public AudioClip itemCollectSFX;      // 아이템 먹는 효과음
    public AudioClip jumpSFX;             // 점프하는 효과음
    public AudioClip buttonClickSFX;


    //// button touch effect
    public AudioClip GameStartButton;                     // Game start event
    //public AudioClip SettingButtonTouch;
    //public AudioClip PauseMainHomeButton;
    //public AudioClip PauseContinueButton;
    //public AudioClip PauseBGMButton;
    //public AudioClip PauseSoundEffectButton;
    //public AudioClip CautionMainHomeButton;
    //public AudioClip CautionContinueButton;
    //public AudioClip AdsComeBackButton;
    //public AudioClip SkipButton;
    //public AudioClip ResultScoreTwiceButton;
    //public AudioClip ResultReStartButton;
    //public AudioClip ResultMainHomeButton;
    //public AudioClip ResultCoinTwiceButton;
    //public AudioClip StoryReTurnButton;
    //public AudioClip StoryButton;
    //public AudioClip SettingInstagrambutton;
    //public AudioClip SettingsoundEffecftButton;
    //public AudioClip SettingSoundEffectButton;
    //public AudioClip SettingBGMButton;
    //public AudioClip SettingReturnButton;
    //public AudioClip CreditsReTurnButton;


    // Chasing Monster
    public AudioClip ChasingMonster;                // Chasing monster appears 추적몬스터 등장 
    public AudioClip ChaseDistanceWarning;          // Chase distance warning 추적거리 가까움 경고 
    
    // Platform 발판 
    public AudioClip ItemAcquisition;               // Item acquisition 
    public AudioClip CurrencyItemAcquisition;       // Currency item acquisition 재화 아이템 획득
    public AudioClip MonsterHit;                    // Monster hit 몬스터 타격 
    public AudioClip MonsterDefeat;                 // Monster defeat 몬스터 퇴치 
    public AudioClip DisruptiveItemAcquisition;     // Disruptive item acquisition 방해 아이템
    
    // Pattern 방해 패턴
    public AudioClip PatternAppearanceWarning;      // Pattern appearance warning 패턴 등장 경고 
    public AudioClip PatternAttack;                 // Pattern attack 패턴 공격 
    
    // Default Character 기본 캐릭터
    public AudioClip CharacterJump;                 // Character jump 점프 
    public AudioClip CharacterAttack;               // Character attack 공격 
    public AudioClip GameOverDefenseRelease;        // Game over defense transformation release 변신 해제 
    public AudioClip GameOver;                      // Game over event 게임 오버 
    public AudioClip Resurrection;                  // Resurrection event 부활
    
    // Transformation Effects
    public AudioClip Transformation;                // Transformation event 변신 
    public AudioClip TransformationReleaseWarning;  // Transformation release warning 변신 해재 시간 경고 
    public AudioClip TransformationTimeOut;         // Transformation release due to time/usage limits
    public AudioClip NinjaJump;                     // Ninja jump 닌자 점프 
    public AudioClip BullJump;                      // Bull jump 황소 개구리 점프 
    public AudioClip BullMonsterSwallowAttack;      // Bull monster swallowing attack 황소 개구리 몬스터 삼키는 공격 
    public AudioClip BullMonsterSpitAttack;         // Bull monster spit attack 황소 개구리 몬스터 뱉는 공격 
    public AudioClip LotusMovement;                 // Lotus movement 연곷이동 
    public AudioClip LotusMagic;                    // Lotus magic 연꽃 마법 
    public AudioClip GoldenResurrection;            // Golden resurrection 황금 부활 

    // Fever
    public AudioClip FeverMapMovement;              // Fever map movement 피버맵 이동 

    // Result Screen
    public AudioClip CoutnueGame;                   // Resume game 이어하기 
    public AudioClip ScoreResult;                   // Score result 점수 결과 
    public AudioClip HighScoreAchievement;          // High score / Achievement / Ranking increase 최고기록 업적 랭킹 상승 
  

    private bool isGameStarted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //startPanelBGM = gameObject.AddComponent<AudioSource>();
        //playingPanelBGM = gameObject.AddComponent<AudioSource>();
        //tutorialPanelBGM = gameObject.AddComponent<AudioSource>();
        //feverPanelBGM = gameObject.AddComponent<AudioSource>();
        GetComponent<AudioSource>().volume = 0.5f;
    }

    public void PlayStartPanelBGM()
    {
        startPanelBGM.Play();  
    }

    public void PlayPlayingPanelBGM()
    {
        playingPanelBGM.Play();  
    }

    public void PlayButtonClickGameStart()
    {
        soundEffectAudioSource.PlayOneShot(GameStartButton);  
    }


    public void PlayItemCollectSFX()
    {
        soundEffectAudioSource.PlayOneShot(itemCollectSFX);  // 아이템 먹는 효과음 재생
    }

    public void PlayJumpSFX()
    {
        soundEffectAudioSource.PlayOneShot(jumpSFX);  // 점프 효과음 재생
    }*/

}



