#include "pch.h"
#include "Functions.h"

#include <oaidl.h>

#ifdef CPPLIBRARY_EXPORTS
#define CPPLIBRARY_API __declspec(dllexport)
#else
#define CPPLIBRARY_API __declspec(dllimport)
#endif

extern "C"
{
	CPPLIBRARY_API void __stdcall CallPrintToConsole()
	{
		Library::PrintToConsole();
	}

	CPPLIBRARY_API void __stdcall CallFillVector()
	{
		Library::FillVector();
	}

	CPPLIBRARY_API void __stdcall CallGetVector(Library::Location const** firstElement, unsigned long& length)
	{
		const std::vector<Library::Location>& numbers = Library::GetVector();
		length = static_cast<unsigned long>(numbers.size());
		*firstElement = numbers.data();
	}

	CPPLIBRARY_API void __stdcall CallInitializeLocation(Library::Location* location, float x, float y)
	{
		location->x = x;
		location->y = y;
	}

	CPPLIBRARY_API BSTR CallGetLocationString(const Library::Location& location)
	{
		std::string output = location.ToString();
		std::wstring woutput{ output.cbegin(), output.cend() };
		return ::SysAllocString(woutput.c_str());
	}

	CPPLIBRARY_API void __stdcall CallFillStructVector()
	{
		Library::FillStructVector();
	}

	CPPLIBRARY_API void __stdcall CallGetStructVector(Library::Player *const*& firstElement, unsigned long& length)
	{
		const std::vector<Library::Player*>& players = Library::GetStructVector();
		length = static_cast<unsigned long>(players.size());
		firstElement = players.data();
	}

	CPPLIBRARY_API BSTR CallGetPlayerString(const Library::Player& player)
	{
		std::string output = player.ToString();
		std::wstring woutput{ output.cbegin(), output.cend() };
		return ::SysAllocString(woutput.c_str());
	}
}