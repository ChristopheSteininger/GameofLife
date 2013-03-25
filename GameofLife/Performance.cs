using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

// DO NOT USE THIS

/*
namespace GameofLife
{
    class MethodInfo
    {
        private int counter = 0;
        public int Counter { get { return counter; } }

        private long totalTimeUsed;
        public long TotalTimeUsed { get { return totalTimeUsed; } }

        private string methodName;
        public string MethodName { get { return methodName; } }

        public MethodInfo(string methodName)
        {
            this.methodName = methodName;
        }

        public void Update(long runTime)
        {
            counter++;
            totalTimeUsed += runTime;
        }
    }

    static class Performance
    {
        private static Dictionary<string, MethodInfo> methodInfo
            = new Dictionary<string, MethodInfo>();

        public static void CreateNew(string id, string methodName)
        {
            methodInfo.Add(id, new MethodInfo(methodName));
        }

        public static void Update(string id, long runTime)
        {
            Debug.Assert(methodInfo.ContainsKey(id), id + " is not a valid key.");

            methodInfo[id].Update(runTime);
        }

        public static KeyValuePair<string, MethodInfo>[] GetMethodInfo()
        {
            return methodInfo.ToArray();
        }
    }

    class Timer : IDisposable
    {
        private readonly string id;
        private readonly Stopwatch stopwatch = new Stopwatch();

        public Timer(string id)
        {
            this.id = id;
            stopwatch.Start();
        }

        public void Dispose()
        {
            Performance.Update(id, stopwatch.ElapsedMilliseconds);
        }
    }

}
*/