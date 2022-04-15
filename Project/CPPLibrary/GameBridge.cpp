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

//HINT 1: Use pointer to instance of CppLibrary::GameManager* -> Pass as parameter
//HINT 2: To pass the gameObjects containers to Managed Memory (Aliens & Projectiles) use the following signature:
//		  void(CppLibrary::GameManager* pGame, CppLibrary::ObjectContainerType type, CppLibrary::GameObject*const*& pFirstElement, unsigned long* pLength)
//HINT 3: Make sure you pay attention to parameters passed by value (as object or pointer) and by reference
//HINT 4: For the function pointers used to trigger sounds use the defined type: CppLibrary::SoundCallback