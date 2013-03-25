using System;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;

namespace GameofLife
{
    public partial class MainForm : Form
    {
        private bool isRunning = false;

        private BackgroundWorker lifeWorker = new BackgroundWorker();
        private GameState state;

        private Stopwatch runTime = new Stopwatch();

        public MainForm()
        {
            InitializeComponent();

            PowerTwos.Initialise();
            state = new GameState();

            MinimumSize = Size;

            lifeWorker.WorkerSupportsCancellation = true;
            lifeWorker.WorkerReportsProgress = true;
            lifeWorker.DoWork += lifeWorker_DoWork;
            lifeWorker.ProgressChanged += lifeWorker_ProgressChanged;

            updateStatisticsLabels();
        }

        private void btnRun_Click(object sender, System.EventArgs e)
        {
            isRunning = !isRunning;
            btnRun.Text = isRunning ? "Stop" : "Run";

            if (isRunning)
            {
                runTime.Start();
                lifeWorker.RunWorkerAsync();
            }

            else
            {
                runTime.Stop();
                lifeWorker.CancelAsync();
            }
        }

        private void lifeWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; !lifeWorker.CancellationPending && i < 700; i++)
            {
                state.Iterate();
                PaintDisplay(plDisplay.CreateGraphics());

                lifeWorker.ReportProgress(0);
            }

            PaintDisplay(plDisplay.CreateGraphics());
        }

        #region Boring Event Handlers
        void lifeWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            updateStatisticsLabels();
        }

        private void btnRandomize_Click(object sender, System.EventArgs e)
        {
            state.Randomize(1);
            PaintDisplay(plDisplay.CreateGraphics());
        }

        private void btnIterate_Click(object sender, System.EventArgs e)
        {
            state.Iterate();
            PaintDisplay(plDisplay.CreateGraphics());

            updateStatisticsLabels();
        }

        private void getHashDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HashInfo hashInfo = new HashInfo();
            hashInfo.Show();
        }

        private void performanceDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformanceData performanceData = new PerformanceData();
            performanceData.Show();
        }

        private void scrollH_Scroll(object sender, ScrollEventArgs e)
        {
            PaintDisplay(plDisplay.CreateGraphics());
        }

        private void scrollV_Scroll(object sender, ScrollEventArgs e)
        {
            PaintDisplay(plDisplay.CreateGraphics());
        }

        private void plDisplay_Paint(object sender, PaintEventArgs e)
        {
            PaintDisplay(e.Graphics);
        }
        #endregion

        private void updateStatisticsLabels()
        {
            lblGeneration.Text = state.Generation
                + " Run time: " + runTime.ElapsedMilliseconds;

            lblPopulation.Text = state.Population;
        }

        private void PaintDisplay(Graphics plDisplayGraphics)
        {
            Brush greenBrush = new SolidBrush(Color.Green);
            Pen blackPen = new Pen(Color.Black);

            plDisplayGraphics.Clear(plDisplay.BackColor);

            PaintGrid(plDisplayGraphics, greenBrush, blackPen, 3, 1);

            plDisplayGraphics.Dispose();
            greenBrush.Dispose();
            blackPen.Dispose();
        }

        //private void PaintGrid(Graphics graphics, Brush cellBrush, Pen gridPen,
        //    int cellSize, int padding)
        //{
        //    int totalSize = cellSize + padding;
        //    int width = state.State.GetLength(0) * totalSize;

        //    graphics.DrawRectangle(gridPen, 0, 0, width / 2, width / 2);

        //    for (int y = state.State.GetLength(0) / 4; y < 3 * state.State.GetLength(0) / 4; y++)
        //    {
        //        for (int x = state.State.GetLength(1) / 4; x < 3 * state.State.GetLength(1) / 4; x++)
        //        {
        //            if (state.State != null && state.State[y, x])
        //            {
        //                graphics.FillRectangle(cellBrush, x * totalSize - width / 4,
        //                    y * totalSize - width / 4, cellSize, cellSize);
        //            }
        //        }
        //    }
        //}

        private void PaintGrid(Graphics graphics, Brush cellBrush, Pen gridPen,
            int cellSize, int padding)
        {
            if (state.State == null)
            {
                return;
            }

            int totalSize = cellSize + padding;
            int width = state.State.GetLength(0) * totalSize;

            graphics.DrawRectangle(gridPen, 0, 0, width, width);
            //graphics.DrawRectangle(gridPen, width / 4, width / 4, width / 2, width / 2);

            for (int y = 0; y < state.State.GetLength(0); y++)
            {
                for (int x = 0; x < state.State.GetLength(1); x++)
                {
                    if (state.State[y, x])
                    {
                        graphics.FillRectangle(cellBrush, x * totalSize,
                            y * totalSize, cellSize, cellSize);
                    }
                }
            }
        }
    }
}
