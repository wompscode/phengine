using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace phengine;

class Program
{
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();

    public static bool debug;
    static void Main(string[] args){
#if DEBUG
            debug = true;
#endif

        string[] argv = Environment.GetCommandLineArgs();

        if(argv.Length > 1) if (argv[1] == "--debug")
            debug = true;

        if (debug) AllocConsole();

        Demo demo = new Demo();
    }

}