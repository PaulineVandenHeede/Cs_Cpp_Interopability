#include "stdafx.h"
#include "GameLogic.h"
#include <oaidl.h>

//--------- PREPROCESSOR ---------//
#ifdef CPPLIBRARY_EXPORTS
	#define CPPLIBRARY_API __declspec(dllexport)
#else
	#define CPPLIBRARY_API __declspec(dllimport)
#endif

//--------- BRIDGE ---------//
//** ADD "C" BRIDGE FUNCTIONS **//

//GameManager
extern "C"
{
	//Vector2
	CPPLIBRARY_API  CppLibrary::Vector2 __stdcall MarshalVector2Return(float x, float y)
	{
		return CppLibrary::Vector2{ x, y };
	}
	CPPLIBRARY_API void __stdcall MarshalVector2Output(CppLibrary::Vector2& v, float x, float y)
	{
		v = CppLibrary::Vector2{ x, y };
	}

	//GameObject
	CPPLIBRARY_API CppLibrary::GameObject __stdcall MarshalGameObjectReturn(const CppLibrary::Vector2& pos, const CppLibrary::Vector2& size)
	{
		return CppLibrary::GameObject{ pos, size };
	}
	CPPLIBRARY_API void __stdcall MarshalGameObjectOutput(CppLibrary::GameObject& go, const CppLibrary::Vector2& pos, const CppLibrary::Vector2& size)
	{
		go = CppLibrary::GameObject{ pos, size };
	}

	//GameManager
	CPPLIBRARY_API CppLibrary::GameManager* __stdcall CreateGameManager()
	{
		return new CppLibrary::GameManager{};
	}
	CPPLIBRARY_API void __stdcall DestroyGameManager(CppLibrary::GameManager* pGame)
	{
		if (pGame)
		{
			delete pGame;
			pGame = nullptr;
		}
	}
	CPPLIBRARY_API void CallUpdate(CppLibrary::GameManager* pGame, float deltaTime)
	{
		if (pGame)
			pGame->Update(deltaTime);
	}
	CPPLIBRARY_API void CallGetGameObjectContainer(CppLibrary::GameManager* pGame, CppLibrary::ObjectContainerType type,
	CppLibrary::GameObject* const*& pFirstElement, unsigned long& pLength)
	{
		if (!pGame)
			return;

		const std::vector<CppLibrary::GameObject*>& objects = pGame->GetGameObjectContainer(type);
		pLength = static_cast<unsigned long>(objects.size());
		pFirstElement = objects.data();
	}

	CPPLIBRARY_API void CallGetPlayerObject(CppLibrary::GameManager* pGame, CppLibrary::GameObject const*& pGameObject)
	{
		if (!pGame)
			return;

		pGameObject = pGame->GetPlayer();
	}
	CPPLIBRARY_API void CallSetPlayerName(CppLibrary::GameManager* pGame, char* pName)
	{
		if (!pGame)
			return;

		pGame->SetName(pName);
	}

	CPPLIBRARY_API const char* CallGetPlayerName(CppLibrary::GameManager* pGame)
	{
		if (!pGame)
			return nullptr;

		return pGame->GetName();
	}

	CPPLIBRARY_API void CallMovePlayerObject(CppLibrary::GameManager* pGame, CppLibrary::Vector2 direction, float deltaTime)
	{
		if (!pGame)
			return;

		pGame->MovePlayer(direction, deltaTime);
	}

	CPPLIBRARY_API void CallSpawnProjectileObject(CppLibrary::GameManager* pGame, CppLibrary::Vector2 position)
	{
		if (!pGame)
			return;

		pGame->SpawnProjectile(position);
	}

	CPPLIBRARY_API void CallSetShootDelegate(CppLibrary::GameManager* pGame, CppLibrary::SoundCallback pFunction)
	{
		if (!pGame)
			return;

		pGame->SetShootCallback(pFunction);
	}
	CPPLIBRARY_API void CallSetKillDelegate(CppLibrary::GameManager* pGame, CppLibrary::SoundCallback pFunction)
	{
		if (!pGame)
			return;

		pGame->SetKillCallback(pFunction);
	}
}

//extern "C" _declspec(dllexport) const CppLibrary::GameObject* const CallGetPlayerObject(CppLibrary::GameManager * pObject)
//{
//	if (pObject)
//		return pObject->GetPlayer();
//	return nullptr;
//}

//HINT 1: Use pointer to instance of CppLibrary::GameManager* -> Pass as parameter
//HINT 2: To pass the gameObjects containers to Managed Memory (Aliens & Projectiles) use the following signature:
//		  void(CppLibrary::GameManager* pGame, CppLibrary::ObjectContainerType type, CppLibrary::GameObject*const*& pFirstElement, unsigned long* pLength)
//HINT 3: Make sure you pay attention to parameters passed by value (as object or pointer) and by reference
//HINT 4: For the function pointers used to trigger sounds use the defined type: CppLibrary::SoundCallback