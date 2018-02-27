﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PMapTestApp
{
    public class MemoryFailPointTest
    {
        // Allocate in chunks of 64 megabytes.
        private const uint chunkSize = 64 << 20;
        // Use more than the total user-available address space (on 32 bit machines)
        // to drive towards getting an InsufficientMemoryException.
        private const uint numWorkItems = 1 + ((1U << 31) / chunkSize);
        static Queue workQueue = new Queue(50);

        // This value can be computed separately and hard-coded into the application.
        // The method is included to illustrate the technique.
        private static int EstimateMemoryUsageInMB()
        {
            int memUsageInMB = 0;

            long memBefore = GC.GetTotalMemory(true);
            int numGen0Collections = GC.CollectionCount(0);
            // Execute a test version of the method to estimate memory requirements.
            // This test method only exists to determine the memory requirements.
            ThreadMethod();
            // Includes garbage generated by the worker function.
            long memAfter = GC.GetTotalMemory(false);
            // If a garbage collection occurs during the measuring, you might need a greater memory requirement.
            Console.WriteLine("Did a GC occur while measuring?  {0}", numGen0Collections == GC.CollectionCount(0));
            // Set the field used as the parameter for the MemoryFailPoint constructor.
            long memUsage = (memAfter - memBefore);
            if (memUsage < 0)
            {
                Console.WriteLine("GC's occurred while measuring memory usage.  Try measuring again.");
                memUsage = 1 << 20;
            }

            // Round up to the nearest MB.
            memUsageInMB = (int)(1 + (memUsage >> 20));
            Console.WriteLine("Memory usage estimate: {0} bytes, rounded to {1} MB", memUsage, memUsageInMB);
            return memUsageInMB;
        }

        public static void EntryPoint()
        {
            Console.WriteLine("Attempts to allocate more than 2 GB of memory across worker threads.");
            int memUsageInMB = EstimateMemoryUsageInMB();

            // For a production application consider using the threadpool instead.
            Thread[] threads = new Thread[numWorkItems];
            // Create a work queue to be processed by multiple threads.
            int n = 0;
            for (n = 0; n < numWorkItems; n++)
                workQueue.Enqueue(n);
            // Continue to launch threads until the work queue is empty.
            while (workQueue.Count > 0)
            {
                Console.WriteLine(" GC heap (live + garbage): {0} MB", GC.GetTotalMemory(false) >> 20);
                MemoryFailPoint memFailPoint = null;
                try
                {
                    // Check for available memory.
                    memFailPoint = new MemoryFailPoint(memUsageInMB);
                    n = (int)workQueue.Dequeue();
                    threads[n] =
                        new Thread(new ParameterizedThreadStart(ThreadMethod));
                    WorkerState state = new WorkerState(n, memFailPoint);
                    threads[n].Start(state);
                    Thread.Sleep(10);
                }
                catch (InsufficientMemoryException e)
                {
                    // MemoryFailPoint threw an exception, handle by sleeping for a while,  then 
                    // continue processing the queue.
                    Console.WriteLine("Expected InsufficientMemoryException thrown.  Message: " + e.Message);
                    // We could optionally sleep until a running worker thread 
                    // has finished, like this:  threads[joinCount++].Join();
                    Thread.Sleep(1000);
                }
            }

            Console.WriteLine("WorkQueue is empty - blocking to ensure all threads quit (each thread sleeps for 10 seconds)");
            foreach (Thread t in threads)
                t.Join();
            Console.WriteLine("All worker threads are finished - exiting application.");
        }

        // Test version of the working code to determine memory requirements.
        static void ThreadMethod()
        {
            byte[] bytes = new byte[chunkSize];
        }

        internal class WorkerState
        {
            internal int _threadNumber;
            internal MemoryFailPoint _memFailPoint;

            internal WorkerState(int threadNumber, MemoryFailPoint memoryFailPoint)
            {
                _threadNumber = threadNumber;
                _memFailPoint = memoryFailPoint;
            }

            internal int ThreadNumber
            {
                get { return _threadNumber; }
            }

            internal MemoryFailPoint MemoryFailPoint
            {
                get { return _memFailPoint; }
            }
        }

        // The method that does the work.
        static void ThreadMethod(Object o)
        {
            WorkerState state = (WorkerState)o;
            Console.WriteLine("Executing ThreadMethod, " +
                "thread number {0}.", state.ThreadNumber);
            byte[] bytes = null;
            try
            {
                bytes = new byte[chunkSize];
                // Allocated all the memory needed for this workitem.
                // Now dispose of the MemoryFailPoint, then process the workitem.
                state.MemoryFailPoint.Dispose();
            }
            catch (OutOfMemoryException oom)
            {
                Console.Beep();
                Console.WriteLine("Unexpected OutOfMemory exception thrown: " + oom);
            }

            // Do work here, possibly taking a lock if this app needs 
            // synchronization between worker threads and/or the main thread.

            // Keep the thread alive for awhile to simulate a running thread.
            Thread.Sleep(10000);

            // A real thread would use the byte[], but to be an illustrative sample,
            // explicitly keep the byte[] alive to help exhaust the memory.
            GC.KeepAlive(bytes);
            Console.WriteLine("Thread {0} is finished.", state.ThreadNumber);

        }
    }
}
