using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


// Unfinished !!!
namespace Needler.BattleSystem
{
    public enum MetronomeState
    {
        Uninitialized = -1,
        Ready = 0,
        RunningPrelude = 1,
        PausedPrelude = 2,
        RunningMain = 3,
        PausedMain = 4
    }
    public class Metronome
    {

        double[] beats = new double[0];
        double[] prelude = new double[0];
        double loopDuration = 0f;
        double preludeDuration = 0f;
        int cindQActive = -1;


        double countdown = 0;

        MetronomeState cstate = MetronomeState.Uninitialized;

        public static Metronome loadFromBPM(int bpm)
        {
            Metronome ret = new Metronome();
            ret.beats = new double[1] { 0f };
            ret.loopDuration = 60f / bpm;
            ret.cstate = MetronomeState.Ready;
            return ret;
        }

        public void Update(GameTime gametime)
        {
            if (cstate == MetronomeState.RunningMain)
            {

            }
            else if (cstate == MetronomeState.RunningPrelude)
            {

            }
            
        }


    }
}
