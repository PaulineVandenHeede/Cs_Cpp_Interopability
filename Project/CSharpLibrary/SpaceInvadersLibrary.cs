using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CSharpLibrary
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2
    { public float X, Y; };

    [StructLayout(LayoutKind.Sequential)]
    public class GameObject
    {
        public Vector2 Position;
        public Vector2 Size;
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

                this._disposed = true;
            }
        }

        //Public Functions
        public void Update(float deltaTime)
        {
            //** ADD CODE **//
        }

        public List<GameObject> GetGameObjectContainer(ObjectContainerType type)
        {
            List<GameObject> objects = new List<GameObject>();

            //** ADD CODE **//
            //HINT: if used the predetermined signature our first element is received as CppLibrary::GameObject*const*& -> Read: pointer to const pointer as reference
            //HINT: use "pointer arithmetic" to determine the next element (keep in mind we are working in x64)

            return objects;
        }

        public GameObject GetPlayerObject()
        {
            //** ADD CODE **//

            return new GameObject();
        }

        public void SetPlayerName(string name)
        {
            //** ADD CODE **//
        }

        public string GetPlayerName()
        {
            //** ADD CODE **//
            return "";
        }

        public void MovePlayerObject(Vector2 direction, float deltaTime)
        {
            //** ADD CODE **//
        }

        public void SpawnProjectileObject(Vector2 position)
        {
            //** ADD CODE **//
        }

        public void SetShootDelegate(Delegate fnc)
        {
            //** ADD CODE **//
        }

        public void SetKillDelegate(Delegate fnc)
        {
            //** ADD CODE **//
        }

        //Bridge Functions
        //** ADD BRIDGE FUNCTIONS **//
    }
}
