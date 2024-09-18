//using _Project.App.Scripts.UI.Metrics;
//using _Project.RowingCanoe.Scripts.CanoeRefactor;
using UnityEngine;
using maxprofitness.rowing;
using UnityEngine.Serialization;
//using MaxProFitness.SharedSound;

#if MAXPRO_LOGIN
using maxprofitness.login;
#endif

namespace maxprofitness.rowing
{
    public class RowingCanoeGameManager : MonoBehaviour
    {
        #region VARIABLES


        public event System.Action OnGameStarted;
        public event System.Action OnGameFinish;
        public event System.Action OnDeviceConnected;
        public bool hasStarted = false;

        [SerializeField] private RowingCanoeInputController rowingCanoeInputController;


        #endregion


        #region MONOBEHAVIOURS


        //------------------//
        private void Awake()
        //------------------//
        {
            hasStarted = false;
        }


        //------------------//
        private void Start()
        //-----------------//
        {
            hasStarted = false;

        } // END Start


        #endregion


        #region START / FINISH GAME


        //---------------------//
        public void StartGame()
        //--------------------//
        {
            OnGameStarted?.Invoke();

        } // END StartGame



        //----------------------//
        public void FinishGame()
        //---------------------//
        {
            OnGameFinish?.Invoke();

        } // END FinishGame


        #endregion


    } // END RowingCaneGameManager


} // END Namespace
