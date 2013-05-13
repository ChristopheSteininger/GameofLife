using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GameofLife
{
    public partial class MainForm : Form
    {
        private bool isRunning = false;
        private bool isDrawing = false;

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
            while (!lifeWorker.CancellationPending)
            {
                state.Iterate();
                PaintDisplay(plDisplay.CreateGraphics());

            //    if (!isDrawing)
            //    {
                    lifeWorker.ReportProgress(0);
            //    }
            }

            //while (isDrawing)
            //{
            //    System.Threading.Thread.Sleep(5);
            //}

            //lifeWorker.ReportProgress(100);
        }
        void lifeWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            updateStatisticsLabels();
            //PaintDisplay(plDisplay.CreateGraphics());
        }

        #region Boring Event Handlers
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

        private void tbZoom_Scroll(object sender, EventArgs e)
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
            lblGeneration.Text = AddSeperators(state.Generation);
            lblPopulation.Text = AddSeperators(state.Population);
            lblRuntime.Text = AddSeperators((int)runTime.ElapsedMilliseconds) + " ms";

            lblGridSize.Text = state.GridSize + " x " + state.GridSize;
        }

        private string AddSeperators(int number)
        {
            string result = number.ToString();
            for (int i = result.Length - 3; i > 0; i -= 3)
            {
                result = result.Insert(i, ",");
            }

            return result;
        }

        private void PaintDisplay(Graphics plDisplayGraphics)
        {
            Brush greenBrush = new SolidBrush(Color.Green);
            Pen blackPen = new Pen(Color.Black);

            plDisplayGraphics.Clear(plDisplay.BackColor);

            PaintGrid(plDisplayGraphics, greenBrush, blackPen);

            plDisplayGraphics.Dispose();
            greenBrush.Dispose();
            blackPen.Dispose();
        }

        private void PaintGrid(Graphics graphics, Brush cellBrush, Pen gridPen)
        {
            isDrawing = true;

            if (state.State == null)
            {
                return;
            }

            int cellSize = 2;// PowerTwos.Get(tbZoom.Value);
            int padding = 1; // PowerTwos.Get(tbZoom.Value - 2);

            int totalSize = cellSize + padding;
            int width = state.State.GetLength(0) * totalSize;

            int xOffset = -(int)(scrollH.Value * width * 0.01f);
            int yOffset = -(int)(scrollV.Value * width * 0.01f);

            graphics.DrawRectangle(gridPen, xOffset, yOffset, width, width);

            Rectangle[] cells = new Rectangle[state.Population + 50];
            int index = 0;

            for (int y = 0; y < state.State.GetLength(0); y++)
            {
                for (int x = 0; x < state.State.GetLength(1); x++)
                {
                    if (state.State[y, x])
                    {
                        cells[index++] = new Rectangle(x * totalSize + xOffset,
                            y * totalSize + yOffset, cellSize, cellSize);
                    }
                }
            }

            graphics.FillRectangles(cellBrush, cells);

            isDrawing = false;
        }
    }
}
