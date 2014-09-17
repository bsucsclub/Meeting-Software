using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotteryNumberGenerator {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        /// <summary>
        /// Random number generator.
        /// </summary>
        static Random rGen = new Random();
     
        #region Lottery Bounds
        /// <summary>
        /// Lower bound, inclusive
        /// </summary>
        static int lowerBound = 1000;
        /// <summary>
        /// Upper bound, exclusive
        /// </summary>
        static int upperBound = 1200;
        #endregion       
    
        #region Sequence Control
        // Task that the lottery runs on.
        static Task task;
        // Task cancellation token.
        static CancellationTokenSource cts;
        // Delay between number updates.
        static int delay = 25;

        /// <summary>
        /// Starts the lottery sequence.
        /// </summary>
        private void startSequence() {
            delay = 25;
            cts = new CancellationTokenSource();
            task = new Task(sequence);
            task.Start();
        }
        /// <summary>
        /// Stops hte lottery sequence.
        /// </summary>
        private void stopSequence() {
            if (task != null && task.Status == TaskStatus.Running) {
                cts.Cancel();
            }
        }
        #endregion

        #region Main Sequence
        // How many updates have been completed.
        int updates = 0;
        /// <summary>
        /// The main lottery sequence.
        /// </summary>
        private void sequence() {
            int n = rGen.Next(lowerBound, upperBound);
            while (!cts.IsCancellationRequested) {
                n = rGen.Next(lowerBound, upperBound);
                label1.BeginInvoke(new Action(() => {
                    label1.Text = n.ToString();
                    if (updates > 5) {
                        updates = 0;
                        toggleTextColor();                       
                    }
                    else {
                        updates++;
                    }
                }));
                delay = rGen.Next(0, 100);
                Thread.Sleep(delay);
            }
            int genStop = rGen.Next(800, 2000);
            while (delay < genStop) {
                n = rGen.Next(lowerBound, upperBound);
                label1.BeginInvoke(new Action(() => {
                    label1.Text = n.ToString();
                    toggleTextColor();
                }));
                delay += rGen.Next(0, delay);
                Thread.Sleep(delay);
                
            }
            n = rGen.Next(lowerBound, upperBound);
            label1.BeginInvoke(new Action(() => {
                label1.ForeColor = Color.Green;
                label1.Text = string.Format("Winning Number:\n{0}", n);
            }));
        }
        #endregion

        #region Full Screen
        // Whether or not we are full screen.
        bool isFullScreen = false;
        // Toggles the applications full screen state.
        private void toggleFullScreen() {
            if (!isFullScreen) {
                Bounds = Screen.PrimaryScreen.Bounds;
            }
            else {
                int x = (Screen.PrimaryScreen.Bounds.Width - 800) / 2;
                int y = (Screen.PrimaryScreen.Bounds.Height - 600) / 2;
                Bounds = new Rectangle(x, y, 800, 600);
            }
            isFullScreen = !isFullScreen;
        }
        #endregion

        #region Color Changing
        // Whether or not the current text color is red.
        bool isRed = true;
        /// <summary>
        /// Toggles the current text color.
        /// </summary>
        private void toggleTextColor() {
            if (isRed) {
                label1.ForeColor = Color.Red;
            }
            else {
                label1.ForeColor = Color.Blue;
            }
            isRed = !isRed;
        }
        #endregion

        /// <summary>
        /// Handle key press events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e) {
            int x = 0;
            e.Handled = true;
            switch (e.KeyChar) {
                case 'b':
                    startSequence();
                    break;
                case 'e':
                    stopSequence();
                    break;
                case 'f':
                    toggleFullScreen();
                    break;
                case 'p':
                    stopSequence();
                    Close();
                    break;
                default:
                    e.Handled = false;
                    break;
            }
        }
    }
}
