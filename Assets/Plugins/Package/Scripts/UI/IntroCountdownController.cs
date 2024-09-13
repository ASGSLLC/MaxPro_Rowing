//using MaxProFitness.SharedSound;
using System.Collections;
using UnityEngine;
//using _Project.RowingCanoe.Scripts.CanoeRefactor;
//using _Project.RowingCanoe.Scripts;
//using _Project.RowingCanoe.Scripts.Player;
#if MAXPRO_LOGIN
using maxprofitness.login;
#endif

namespace maxprofitness.rowing
{
    public sealed class IntroCountdownController : MonoBehaviour
    {
        #region VARIABLES


        [Header("Animation Component")]
        [SerializeField] private Animator _animator;

        [SerializeField] private RowingCanoeGameManager canoeGameManager;

        private OpponentController opponent;
        private RowingCanoeRacePlayerController rowingPlayerController;
        private RowingTrackManager rowingTrackManager;
#if MAXPRO_LOGIN
        private IntroAnimHelper introAnimHelper;
#endif
        private static readonly int PlayAnimation = Animator.StringToHash("playAnimation");
        private bool _canPlayIntroCountdown = true;
        public bool isRowing = false;
        public bool isFighter = false;

#if MAXPRO_LOGIN
        [Header("Game Mode")]
        [SerializeField] private MinigameType _minigameType;
#endif

#endregion


        #region MONOBEHAVIOURS


        //-------------------//
        private void Start()
        //-------------------//
        {
            opponent = FindObjectOfType<OpponentController>();
            canoeGameManager = FindObjectOfType<RowingCanoeGameManager>();
            rowingPlayerController = FindObjectOfType<RowingCanoeRacePlayerController>();
            rowingTrackManager = FindObjectOfType<RowingTrackManager>();
#if MAXPRO_LOGIN
            introAnimHelper = FindObjectOfType<IntroAnimHelper>();
#endif

        } // END Start


        #endregion


        #region INIT ROWING GAME


        //---------------------------------//
        public void InitRowingGame()
        //---------------------------------//
        {
            EnableGameManager();
            CanoeEvents.RowingRaceStartedEvent?.Invoke();

            canoeGameManager.hasStarted = true;
            opponent.ToggleMovement(true);
            rowingPlayerController.ToggleMovement(true);

            rowingTrackManager.hasStarted = true;
            rowingTrackManager.StartCoroutine(rowingTrackManager.GameplayLoopCoroutine());
            
        } // END InitRowingGame


        #endregion


        #region ENABLE GAME MANAGER
        /// <summary>
        /// Sets GameManager.isGamePlaying = true && GameManager.isRecieveingInput = true
        /// </summary>
        //------------------------------//
        private void EnableGameManager()
        //------------------------------//
        {
#if MAXPRO_LOGIN
            GameManager.isGamePlaying = true;
            GameManager.isRecieveingInput = true;
#endif
        } // END EnableGameManager


#endregion


        #region TURN OFF THIS ANIMATION OBJECT
        //--------------------------------------//
        public void HideIntroCountdownAnim()
        //--------------------------------------//
        {
            this.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
            this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            this.gameObject.GetComponent<CanvasGroup>().interactable = false;

        } // END TurnOffThisAnimationObject
        #endregion


        #region INIT INTRO COUNTDOWN
        //---------------------------------------//
        public void InitializeIntroCountdown(int time)
        //---------------------------------------//
        {
            //Debug.Log($"IntroCountdownController.cs // InitializeIntroCountdown() // [{typeof(IntroCountdownController)}] - Initializing countdown");
            gameObject.SetActive(true);

            if (_canPlayIntroCountdown)
            {
                _canPlayIntroCountdown = false;

                StartCoroutine(IPlayIntroCountdownAnimation(time));
            }

        } // END InitIntroCountdown


        #endregion


        #region HANDLE COUNTDOWN SOUND


        //------------------------------------------//
        private void HandleCountdownSound(int time)
        //------------------------------------------//
        {
#if MAXPRO_LOGIN
            Debug.Log("IntroCountdownController.cs // HandleCountdownSound() // This should trigger the voice announcer for each ");
            
            switch (time)
            {
                case 3:
                    {
                        SoundManager.PlaySound(SharedGameSound.VOICE_ANNOUNCER_COUNTDOWN_3);
                        break;
                    }
                case 2:
                    {
                        SoundManager.PlaySound(SharedGameSound.VOICE_ANNOUNCER_COUNTDOWN_2);
                        break;
                    }
                case 1:
                    {
                        SoundManager.PlaySound(SharedGameSound.VOICE_ANNOUNCER_COUNTDOWN_1);
                        break;
                    }
            }
#endif
        } // END HandleCountdown


        #endregion


        #region I PLAY INTRO COUNDOWN ANIMATION


        //-------------------------------------------------------//
        public IEnumerator IPlayIntroCountdownAnimation(int time)
        //-------------------------------------------------------//
        {

            //Debug.Log("Playing intro");
            yield return new WaitForSeconds(1);
#if MAXPRO_LOGIN
            time--;

            if (time > 0)
            {
                HandleCountdownSound(time);

                StartCoroutine(IPlayIntroCountdownAnimation(time));
            }
            else if (time == 0)
            {
                //Debug.Log("Choosing GameType");
                rowingTrackManager = FindObjectOfType<RowingTrackManager>();

                if(rowingTrackManager != null)
                {
                    isFighter = false;
                    isRowing = true;
                }
                else
                {
                    isFighter = true;
                    isRowing = false;
                }

                if (isFighter == true)
                {
                    SoundManager.PlaySound(SharedGameSound.FF_VOICE_ANNOUNCER_FIGHT);
                    //Debug.Log("IntroCountdownController.cs // IPlayIntroCountdownAnimation() // Playing FitFighter Sound");
                }
                else if (isRowing == true)
                {
                    SoundManager.PlaySound(SharedGameSound.RC_VOICE_ANNOUNCER_GO);
                    //Debug.Log("IntroCountdownController.cs // IPlayIntroCountdownAnimation() // Playing Canoe Sound");
                }

                if(isRowing == true)
                {
                    InitRowingGame();
                    Debug.Log("IntroCountdownController.cs // IPlayIntroCountdownAnimation() // Init Rowing game");
                }
            }
#endif
        } // END IPlayIntroCountdownAnimation


        #endregion


    } // END IntroCountdownController.cs

} // END Namespace
