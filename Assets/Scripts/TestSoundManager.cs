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
    public AudioClip GameProgress; // In-game progress ���� ����
                                   //public AudioClip PreGameCutscene; // Pre-game cutscene
                                   //public AudioClip FeverMap;

    public AudioClip GameStartButton;

    // Chasing Monster
    public AudioClip ChasingMonster;                // Chasing monster appears �������� ���� 
    public AudioClip ChaseDistanceWarning;          // Chase distance warning �����Ÿ� ����� ��� 

    // Platform ���� 
    public AudioClip ItemAcquisition;               // Item acquisition 
    public AudioClip CurrencyItemAcquisition;       // Currency item acquisition ��ȭ ������ ȹ��
    public AudioClip MonsterHit;                    // Monster hit ���� Ÿ�� 
    public AudioClip MonsterDefeat;                 // Monster defeat ���� ��ġ 
    public AudioClip DisruptiveItemAcquisition;     // Disruptive item acquisition ���� ������

    // Pattern ���� ����
    public AudioClip PatternAppearanceWarning;      // Pattern appearance warning ���� ���� ��� 
    public AudioClip PatternAttack;                 // Pattern attack ���� ���� 

    // Default Character �⺻ ĳ����
    public AudioClip CharacterJump;                 // Character jump ���� 
    public AudioClip CharacterAttack;               // Character attack ���� 
    public AudioClip GameOverDefenseRelease;        // Game over defense transformation release ���� ���� 
    public AudioClip GameOver;                      // Game over event ���� ���� 
    public AudioClip Resurrection;                  // Resurrection event ��Ȱ

    // Transformation Effects
    public AudioClip Transformation;                // Transformation event ���� 
    public AudioClip TransformationReleaseWarning;  // Transformation release warning ���� ���� �ð� ��� 
    public AudioClip TransformationTimeOut;         // Transformation release due to time/usage limits
    public AudioClip NinjaJump;                     // Ninja jump ���� ���� 
    public AudioClip BullJump;                      // Bull jump Ȳ�� ������ ���� 
    public AudioClip BullMonsterSwallowAttack;      // Bull monster swallowing attack Ȳ�� ������ ���� ��Ű�� ���� 
    public AudioClip BullMonsterSpitAttack;         // Bull monster spit attack Ȳ�� ������ ���� ��� ���� 
    public AudioClip LotusMovement;                 // Lotus movement �����̵� 
    public AudioClip LotusMagic;                    // Lotus magic ���� ���� 
    public AudioClip GoldenResurrection;            // Golden resurrection Ȳ�� ��Ȱ 

    // Fever
    public AudioClip FeverMapMovement;              // Fever map movement �ǹ��� �̵� 

    // Result Screen
    public AudioClip CoutnueGame;                   // Resume game �̾��ϱ� 
    public AudioClip ScoreResult;                   // Score result ���� ��� 
    public AudioClip HighScoreAchievement;          // High score / Achievement / Ranking increase �ְ��� ���� ��ŷ ��� 

   
    private void Awake()
    {
        //BGM ���
        if (testSoundManager == null)
        {
            testSoundManager = this;  //static ������ �ڱ� �ڽ��� ����
                                  //���� �ٲ� ���� ������Ʈ�� �ı����� ����
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//���� ������Ʈ�� �ı�
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
            GetComponent<AudioSource>().PlayOneShot(ChasingMonster);   //���� Ŭ����
        }
        else if (type == SoundEffectType.ChaseDistanceWarning)
        {
            GetComponent<AudioSource>().PlayOneShot(ChaseDistanceWarning);   //���� ����
        }
        else if (type == SoundEffectType.ItemAcquisition)
        {
            GetComponent<AudioSource>().PlayOneShot(ItemAcquisition);       //Ȱ ���
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

    public AudioClip itemCollectSFX;      // ������ �Դ� ȿ����
    public AudioClip jumpSFX;             // �����ϴ� ȿ����
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
    public AudioClip ChasingMonster;                // Chasing monster appears �������� ���� 
    public AudioClip ChaseDistanceWarning;          // Chase distance warning �����Ÿ� ����� ��� 
    
    // Platform ���� 
    public AudioClip ItemAcquisition;               // Item acquisition 
    public AudioClip CurrencyItemAcquisition;       // Currency item acquisition ��ȭ ������ ȹ��
    public AudioClip MonsterHit;                    // Monster hit ���� Ÿ�� 
    public AudioClip MonsterDefeat;                 // Monster defeat ���� ��ġ 
    public AudioClip DisruptiveItemAcquisition;     // Disruptive item acquisition ���� ������
    
    // Pattern ���� ����
    public AudioClip PatternAppearanceWarning;      // Pattern appearance warning ���� ���� ��� 
    public AudioClip PatternAttack;                 // Pattern attack ���� ���� 
    
    // Default Character �⺻ ĳ����
    public AudioClip CharacterJump;                 // Character jump ���� 
    public AudioClip CharacterAttack;               // Character attack ���� 
    public AudioClip GameOverDefenseRelease;        // Game over defense transformation release ���� ���� 
    public AudioClip GameOver;                      // Game over event ���� ���� 
    public AudioClip Resurrection;                  // Resurrection event ��Ȱ
    
    // Transformation Effects
    public AudioClip Transformation;                // Transformation event ���� 
    public AudioClip TransformationReleaseWarning;  // Transformation release warning ���� ���� �ð� ��� 
    public AudioClip TransformationTimeOut;         // Transformation release due to time/usage limits
    public AudioClip NinjaJump;                     // Ninja jump ���� ���� 
    public AudioClip BullJump;                      // Bull jump Ȳ�� ������ ���� 
    public AudioClip BullMonsterSwallowAttack;      // Bull monster swallowing attack Ȳ�� ������ ���� ��Ű�� ���� 
    public AudioClip BullMonsterSpitAttack;         // Bull monster spit attack Ȳ�� ������ ���� ��� ���� 
    public AudioClip LotusMovement;                 // Lotus movement �����̵� 
    public AudioClip LotusMagic;                    // Lotus magic ���� ���� 
    public AudioClip GoldenResurrection;            // Golden resurrection Ȳ�� ��Ȱ 

    // Fever
    public AudioClip FeverMapMovement;              // Fever map movement �ǹ��� �̵� 

    // Result Screen
    public AudioClip CoutnueGame;                   // Resume game �̾��ϱ� 
    public AudioClip ScoreResult;                   // Score result ���� ��� 
    public AudioClip HighScoreAchievement;          // High score / Achievement / Ranking increase �ְ��� ���� ��ŷ ��� 
  

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
        soundEffectAudioSource.PlayOneShot(itemCollectSFX);  // ������ �Դ� ȿ���� ���
    }

    public void PlayJumpSFX()
    {
        soundEffectAudioSource.PlayOneShot(jumpSFX);  // ���� ȿ���� ���
    }*/

}



