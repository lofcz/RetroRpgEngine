using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NVorbis.NAudioSupport;
using System.Threading;
using System.Windows.Forms;

namespace RetroRPG
{
    // Třída pro přehrávání audia, založená na NetVorbis
    class Sound
    {
        private static Sound sound;
        CancellationTokenSource ts = new CancellationTokenSource();
        CancellationToken ct = new CancellationToken();

        List<CancellationTokenSource> tsList = new List<CancellationTokenSource>();
        List<CancellationToken> ctList = new List<CancellationToken>();

        CancellationTokenSource forRemove = null;

        public static Sound getInstance
        {
            get
            {
                if (sound == null)
                {
                    sound = new Sound();
                }

                return sound;
            }
        }

        /// <summary>
        /// Přehraje zadanou skladbu (mp3,wav,ogg)
        /// </summary>
        /// <param name="file">Název skladby</param>
        /// <param name="loop">Přehrávání donekonečna</param>
        public int playMusic(string file, bool loop)
        {
            ts = new CancellationTokenSource();
            ct = ts.Token;

            tsList.Add(ts);
            ctList.Add(ct);

            
           // MessageBox.Show(ts.GetHashCode().ToString());
           // MessageBox.Show(ct.GetHashCode().ToString());

            Task.Factory.StartNew(() => iPlayMusic(file, loop, ct), ct);
            return ts.GetHashCode();    
        }

        public void stopMusic(int id)
        {

            CancellationTokenSource tsRemove = null;

            foreach (CancellationTokenSource tsIterated in tsList)
            {
                if (id == tsIterated.GetHashCode())
                {
                    tsRemove = tsIterated;
                    break;
                }
            }

            if (tsRemove != null)
            {
                //MessageBox.Show("Stopping sound");
                tsRemove.Cancel();
                // ctList.Remove(tsRemove.Token);
                forRemove = tsRemove;

                tsList.Remove(tsRemove);
            }
        }

        void iPlayMusic(string file, bool repeat, CancellationToken myCt)
        {
            using (var vorbis = new VorbisWaveReader(file))
            using (var waveOut = new NAudio.Wave.WaveOut())
            {
                waveOut.Init(vorbis);
                waveOut.Play();


                while (waveOut.PlaybackState != NAudio.Wave.PlaybackState.Stopped)
                {
                    Thread.Sleep(10);

                    foreach (CancellationToken iteratedCt in ctList)
                    {
                        if (iteratedCt.IsCancellationRequested && iteratedCt == myCt)
                        {
                            ctList.Remove(forRemove.Token);
                            return;
                        }
                    }

                    /*
                    if (ct.IsCancellationRequested)
                    {
                        return;
                    }*/
                }

                if (repeat)
                {
                    iPlayMusic(file, repeat, myCt);
                }

            }
        }
    }
}
