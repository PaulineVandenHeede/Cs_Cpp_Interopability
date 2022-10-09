using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace CSharpLibrary
{

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2
    { 
        public float X, Y;

        [DllImport("CPPLibrary", CallingConvention = CallingConvention.StdCall)]
        public static extern Vector2 MarshalVector2Return(float x, float y);
        [DllImport("CPPLibrary", CallingConvention = CallingConvention.StdCall)]
        public static extern void MarshalVector2Output(ref Vector2 v, float x, float y);
    }

    

    [StructLayout(LayoutKind.Sequential)]
    public class GameObject
    {
        public Vector2 Position;
        public Vector2 Size;

        [DllImport("CPPLibrary", CallingConvention = CallingConvention.StdCall)]
        public static extern GameObject MarshalGameObjectReturn(in Vector2 pos, in Vector2 size);
        [DllImport("CPPLibrary", CallingConvention = CallingConvention.StdCall)]
        public static extern void MarshalGameObjectOutput(ref GameObject obj, in Vector2 pos, in Vector2 size);
    }


    public enum ObjectContainerType
    {
        Alien,
        Projectile
    }

    public class SpaceInvadersLibrary : IDisposable
    {
        //Datamembers
        private readonly IntPtr _nativePtr = IntPtr.Zero;
        private bool _disposed = false;

        //Constructor - Finalizer
        public SpaceInvadersLibrary()
        {
            //** ADD CODE **//
            _nativePtr = CreateGameManager();
            
        }

        ~SpaceInvadersLibrary()
        { Dispose(false); }

        //IDisposable interface
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            if (!this._disposed)
            {
                //** ADD CODE **//
                if(bDisposing)
                {
                    //TODO
                    //Free any dependent IDisposable objects
                }

                if(_nativePtr != IntPtr.Zero)
                {
                    DestroyGameManager(_nativePtr);
                    //_nativePtr = IntPtr.Zero; -> readonly
                }

                this._disposed = true;
            }
        }

        //Public Functions
        public void Update(float deltaTime)
        {
            //** ADD CODE **//
            CallUpdate(_nativePtr, deltaTime);
        }

        public List<GameObject> GetGameObjectContainer(ObjectContainerType type)
        {
            List<GameObject> objects = new List<GameObject>();

            //** ADD CODE **//
            //HINT: if used the predetermined signature our first element is received as CppLibrary::GameObject*const*& -> Read: pointer to const pointer as reference
            //HINT: use "pointer arithmetic" to determine the next element (keep in mind we are working in x64)
            IntPtr firstObject = IntPtr.Zero;
            ulong size = 0;
            CallGetGameObjectContainer(_nativePtr, type, ref firstObject, ref size);

            if (size == 0)
                return objects;

            GameObject temp = null;
            IntPtr pTemp = IntPtr.Zero;
            for (ulong i = 0; i < size; i++)
            {
                unsafe
                {
                    pTemp = ((IntPtr*)firstObject.ToPointer())[i];
                    temp = (GameObject)Marshal.PtrToStructure(pTemp, typeof(GameObject));
                }

                objects.Add(temp);
            }
            return objects;
        }

        public GameObject GetPlayerObject()
        {
            //** ADD CODE **//
            IntPtr temp = IntPtr.Zero;
            CallGetPlayerObject(_nativePtr, ref temp);

            GameObject player = (GameObject)Marshal.PtrToStructure(temp, typeof(GameObject));

            return player;
        }

        public void SetPlayerName(string name)
        {
            //** ADD CODE **//
            CallSetPlayerName(_nativePtr, name);
        }

        public string GetPlayerName()
        {
            //** ADD CODE **//
            IntPtr pName = CallGetPlayerName(_nativePtr);
            if (pName == IntPtr.Zero)
                return null;
            return Marshal.PtrToStringAnsi(pName);
        }

        public void MovePlayerObject(Vector2 direction, float deltaTime)
        {
            //** ADD CODE **//
            CallMovePlayerObject(_nativePtr, direction, deltaTime);
        }

        public void SpawnProjectileObject(Vector2 position)
        {
            //** ADD CODE **//
            CallSpawnProjectileObject(_nativePtr, position);
        }

        public void SetShootDelegate(Delegate fnc)
        {
            //** ADD CODE **//
            IntPtr pFunction = Marshal.GetFunctionPointerForDelegate(fnc);
            CallSetShootDelegate(_nativePtr, pFunction);
        }

        public void SetKillDelegate(Delegate fnc)
        {
            //** ADD CODE **//
            IntPtr pFunction = Marshal.GetFunctionPointerForDelegate(fnc);
            CallSetKillDelegate(_nativePtr, pFunction);
        }

        //Bridge Functions
        //** ADD BRIDGE FUNCTIONS **//
        private const string dll = "CPPLibrary";
        [DllImport(dll, CallingConvention = CallingConvention.StdCall)]
        static private extern IntPtr CreateGameManager();

        [DllImport(dll, CallingConvention = CallingConvention.StdCall)]
        static private extern void DestroyGameManager(IntPtr pObject);

        [DllImport(dll, CallingConvention = CallingConvention.ThisCall)]
        static private extern void CallUpdate(IntPtr pObject, float deltaTime);

        [DllImport(dll, CallingConvention = CallingConvention.ThisCall)]
        static private extern void CallGetGameObjectContainer(IntPtr pGame, ObjectContainerType type, ref IntPtr pFirstElement, ref ulong pLength);

        [DllImport(dll, CallingConvention = CallingConvention.ThisCall)]
        static private extern void CallGetPlayerObject(IntPtr pGame, ref IntPtr GameObject);

        [DllImport(dll, CallingConvention = CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        static private extern void CallSetPlayerName(IntPtr pGame, [MarshalAs(UnmanagedType.LPStr)]string name);

        [DllImport(dll, CallingConvention = CallingConvention.ThisCall, CharSet = CharSet.Ansi)]
        static private extern IntPtr CallGetPlayerName(IntPtr pGame);

        [DllImport(dll, CallingConvention = CallingConvention.ThisCall)]
        static private extern void CallMovePlayerObject(IntPtr pGame, Vector2 direction, float deltaTime);

        [DllImport(dll, CallingConvention = CallingConvention.ThisCall)]
        static private extern void CallSpawnProjectileObject(IntPtr pGame, Vector2 position);

        [DllImport (dll, CallingConvention = CallingConvention.ThisCall)]
        static private extern void CallSetShootDelegate(IntPtr pGame, IntPtr pFunction);

        [DllImport(dll, CallingConvention = CallingConvention.ThisCall)]
        static private extern void CallSetKillDelegate(IntPtr pGame, IntPtr pFunction);
    }

 
}
