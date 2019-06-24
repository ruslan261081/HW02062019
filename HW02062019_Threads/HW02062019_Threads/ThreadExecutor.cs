using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HW02062019_Threads
{
    class ThreadExecutor
    {
        Queue<Thread> _threadsQueue = new Queue<Thread>();
        Thread _everySecondThread;

        public void DoEverySecond()
        {
            while (true)
            {
                lock (this)
                {
                    if (_threadsQueue.Count > 0)
                    {
                        Execute();
                    }
                }
                Thread.Sleep(1000);
            }
        }

        public ThreadExecutor()
        {
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }


        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (this)
            {
                if (_threadsQueue.Count > 0)
                {
                    Execute();

                }
            }
        }
        public void ShutDown()
        {
            _everySecondThread.Abort();
        }

        public void Add(Thread thread)
        {
            lock (this)
            {
                Console.WriteLine($"new thread entered the queue");
                if (thread != null)
                {
                    _threadsQueue.Enqueue(thread);
                }
            }
        }
        public void Execute()
        {
            lock (this)
            {
                while (_threadsQueue.Count > 0)
                {

                    Thread current = _threadsQueue.Dequeue();
                    current.Start();
                }
            }

        }
    }
}
