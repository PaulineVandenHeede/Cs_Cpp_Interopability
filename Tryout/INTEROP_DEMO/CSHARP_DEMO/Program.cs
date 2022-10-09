using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CSHARP_DEMO
{
    internal class Program
    {
        private const string dll_name = @"Libraries\CPPLibrary.dll";

        [StructLayout(LayoutKind.Sequential)]
        public struct Location
        {
            public float x;
            public float y;

            
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public unsafe struct Player
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string name;
            public Location pos;
        }


        static void Main()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            Environment.CurrentDirectory = path;
            Directory.SetCurrentDirectory(path);
            CallPrintToConsole();
            CallFillVector();

            IntPtr element = IntPtr.Zero;
            //CallInitializeLocation(out element, 1, 1);
            
            ulong size = 0;
            CallGetVector(ref element, ref size);

            List<Location> objects = new List<Location>();
            if (size == 0)
                Console.WriteLine("empty list");

            Location temp;
            IntPtr pTemp = Marshal.AllocCoTaskMem(Marshal.SizeOf(element));
            for (ulong i = 0; i < size; i++)
            {
                //Marshal.StructureToPtr(element, pTemp, false);

                unsafe
                {
                    temp = ((Location*)element.ToPointer())[i];
                    //pTemp = IntPtr.Add(element, (int)i * sizeof(Location));
                }
                //temp = (Location)Marshal.PtrToStructure(pTemp, typeof(Location));

                objects.Add(temp);
            }

            foreach (var i in objects)
            {
                Console.WriteLine(CallGetLocationString(i));
            }

            IntPtr elementPlayer = IntPtr.Zero;
            CallFillStructVector();
            CallGetStructVector(ref elementPlayer, ref size);


            List<Player> objectsPlayer = new List<Player>();
            if (size == 0)
                Console.WriteLine("empty list");

            Player player;
            IntPtr tempPlayer;
            //IntPtr pTempPlayer = Marshal.AllocCoTaskMem(Marshal.SizeOf(elementPlayer));
            for (ulong i = 0; i < size; i++)
            {
                //Marshal.StructureToPtr(element, pTemp, false);

                unsafe
                {
                    tempPlayer = ((IntPtr*)elementPlayer.ToPointer())[i];
                    player = (Player)Marshal.PtrToStructure(tempPlayer, typeof(Player));
                }
                //tempPlayer = (Player)Marshal.PtrToStructure(pTempPlayer, typeof(Player));

                objectsPlayer.Add(player);
            }

            foreach (var i in objectsPlayer)
            {
                Console.WriteLine(CallGetPlayerString(i));
            }


            Console.ReadKey();
        }

        [DllImport(dll_name, CallingConvention = CallingConvention.StdCall)] public static extern void CallPrintToConsole();
        [DllImport(dll_name, CallingConvention = CallingConvention.StdCall)] public static extern void CallFillVector();
        [DllImport(dll_name, CallingConvention = CallingConvention.StdCall)] public static extern void CallGetVector(ref IntPtr firstElement, ref ulong length);
        [DllImport(dll_name, CallingConvention = CallingConvention.StdCall)] public static extern void CallInitializeLocation(out Location element, float x, float y);
        [DllImport(dll_name, CallingConvention = CallingConvention.ThisCall)][return: MarshalAs(UnmanagedType.BStr)] private static extern string CallGetLocationString(in Location element);
        [DllImport(dll_name, CallingConvention = CallingConvention.StdCall)] public static extern void CallFillStructVector();
        [DllImport(dll_name, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] public static extern void CallGetStructVector(ref IntPtr firstElement, ref ulong length);
        [DllImport(dll_name, CallingConvention = CallingConvention.ThisCall)][return: MarshalAs(UnmanagedType.BStr)] private static extern string CallGetPlayerString(in Player element);

    }
}

   
