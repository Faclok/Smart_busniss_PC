using Assets.View.Body.FullScreen.AnalyzeWindow;
using Assets.View.Body.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Body.Analyst
{

    public class AnalyzeAnalyst : PanelContent
    {
        [Header("Link")]
        [SerializeField]
        private Analyze _analyze;

        private void Start()
        {
            _analyze.Open(AnalystControll.AnalyzeProperty);

            _analyze.MoveDate(DateTime.Today - new TimeSpan(1, 0, 0, 0), DateTime.Now);

            OnPanelOpen += UpdateOpen;
        }

        private void UpdateOpen()
        {
            _analyze.MoveDate(_analyze.Data.StartTimeCurrent, _analyze.Data.EndTimeCurrent);
        }

        private void OnDestroy()
        {
            OnPanelOpen -= UpdateOpen;
        }
    }
}
