using Assets.View.Body.FullScreen.AnalyzeWindow;
using Assets.View.Body.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.View.Body.Machine
{

    public class AnalyzeMachine : PanelContent
    {

        [Header("Link")]
        [SerializeField]
        private Analyze _analyze;

        private  void Start()
        {
            _analyze.Open(MachineControll.AnalyzeProperty);

            _analyze.MoveDate(DateTime.Today - new TimeSpan(1,0,0,0),DateTime.Now);
        }
    }
}
