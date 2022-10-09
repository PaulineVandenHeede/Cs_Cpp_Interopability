#pragma once
#include <vector>
#include <string>

namespace Library
{
#define MAX_NAME_LENGTH 30

	struct Location
	{
		float x, y;

		std::string ToString() const;
	};

	struct Player
	{
		//std::string name;
		char name[MAX_NAME_LENGTH];
		Location location;

		std::string ToString() const;
	};


	void PrintToConsole();
	void FillVector();
	const std::vector<Location>& GetVector();

	void FillStructVector();
	const std::vector<Player*>& GetStructVector();
}