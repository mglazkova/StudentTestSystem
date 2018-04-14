using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace TeacherApp.Helper
{
    public delegate void DoWorkDelegate(object sender, DoWorkEventArgs e);
    public delegate void CompletedDelegate(object sender, RunWorkerCompletedEventArgs e);
    public delegate void ProgressDelegate(object sender, ProgressChangedEventArgs e);

    public class Worker
    {
        public event EventHandler Complited;
        public BackgroundWorker bWork;
        public DoWorkDelegate Action;
        public CompletedDelegate CompletedAction;
        public ProgressDelegate ProgressAction;

        public Worker(object args, DoWorkDelegate action, CompletedDelegate completedAction, ProgressDelegate progressAction, bool canReport, CancellationTokenSource cancellationToken)
        {
            this.bWork = new BackgroundWorker();
            this.bWork.WorkerReportsProgress = canReport;
            this.Action = action;
            this.CompletedAction = completedAction;
            this.ProgressAction = progressAction;
            bWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWork_RunWorkerCompleted);
            bWork.DoWork += new DoWorkEventHandler(bWork_DoWork);
            bWork.ProgressChanged += new ProgressChangedEventHandler(bWork_ProgressChanged);

            if (cancellationToken != null)
            {
                bWork.WorkerSupportsCancellation = true;
            }
            bWork.RunWorkerAsync(args);
        }

        void bWork_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.ProgressAction != null) this.ProgressAction(sender, e);
        }

        void bWork_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Action(sender, e);
        }

        void bWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var worker = (sender as BackgroundWorker);
            worker.DoWork -= bWork_DoWork;
            worker.RunWorkerCompleted -= bWork_RunWorkerCompleted;
            worker.ProgressChanged -= bWork_ProgressChanged;
            if (this.CompletedAction != null) this.CompletedAction(sender, e);
        }
    }

    public static class BackgroundProcessFactory
    {
        /// <summary>
        /// Количество выполняемых фоновых потоков
        /// </summary>
        public static int Count;

        static BackgroundProcessFactory()
        {
            Count = 0;
        }

        /// <summary>
        /// Выполняет операцию в фоновом потоке, не блокируя UI элементы формы. Может быть вызван сколько угодно раз подряд
        /// </summary>
        /// <param name="args">Начальные аргументы для потока</param>
        /// <param name="action">Метод, выполняемый в фоновом потоке</param>
        /// <param name="cancellationToken">Токен для воркеров с возможностью остановки</param>
        public static void RunAsync(object args, DoWorkDelegate action, CancellationTokenSource cancellationToken = null)
        {
            Worker w = new Worker(args, action, null, null, false, cancellationToken);
            w.Complited += new EventHandler(w_Complited);
            Count++;
        }

        /// <summary>
        /// Выполняет операцию в фоновом потоке, не блокируя UI элементы формы. Может быть вызван сколько угодно раз подряд
        /// </summary>
        /// <param name="args">Начальные аргументы для потока</param>
        /// <param name="action">Метод, выполняемый в фоновом потоке</param>
        /// <param name="completed">Метод, выполняемый после окончания фонового потока</param>
        /// <param name="cancellationToken">Токен для воркеров с возможностью остановки</param>
        public static void RunAsync(object args, DoWorkDelegate action, CompletedDelegate completed, CancellationTokenSource cancellationToken = null)
        {
            Worker w = new Worker(args, action, completed, null, false, cancellationToken);
            w.Complited += new EventHandler(w_Complited);
            Count++;
        }

        /// <summary>
        /// Выполняет операцию в фоновом потоке, не блокируя UI элементы формы. Может быть вызван сколько угодно раз подряд
        /// </summary>
        /// <param name="args">Начальные аргументы для потока</param>
        /// <param name="action">Метод, выполняемый в фоновом потоке</param>
        /// <param name="progress">Метод, выполняемый в фоновом потоке, позволяющий откликаться в основной поток</param>
        /// <param name="completed">Метод, выполняемый после окончания фонового потока</param>
        /// <param name="cancellationToken">Токен для воркеров с возможностью остановки</param>
        public static void RunAsync(object args, DoWorkDelegate action, ProgressDelegate progress, CompletedDelegate completed, CancellationTokenSource cancellationToken = null)
        {
            Worker w = new Worker(args, action, completed, progress, progress != null, cancellationToken);
            w.Complited += new EventHandler(w_Complited);
            Count++;
        }

        static void w_Complited(object sender, EventArgs e)
        {
            Count--;
            (sender as Worker).Complited -= w_Complited;
        }
    }

    public class DoWorkProcessEventArgs : DoWorkEventArgs
    {
        public BackgroundWorker Worker { get; set; }

        public DoWorkProcessEventArgs(object argument, BackgroundWorker worker)
            : base(argument)
        {
            this.Worker = worker;
        }
    }
}
