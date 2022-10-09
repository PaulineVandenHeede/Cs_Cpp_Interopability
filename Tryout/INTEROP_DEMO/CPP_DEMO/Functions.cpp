#include "pch.h"
#include "Functions.h"
#include <iostream>

namespace Library
{
	std::vector<Location> numbers;
	std::vector<Player*> players;

	void PrintToConsole()
	{
		std::cout << "Hello World!\n";
	}
	void FillVector()
	{
		numbers.push_back(Location{ 7, 3 });
		numbers.push_back(Location{ 8, 4 });
		numbers.push_back(Location{ 9, 5 });
		numbers.push_back(Location{ 10, 6 });

	}
	const std::vector<Location>& GetVector()
	{
		return numbers;
	}
	void FillStructVector()
	{
		Player* pTemp = new Player{};
		std::string name = "Pauline";
		std::fill(std::begin(pTemp->name), std::end(pTemp->name), '\0');
		size_t size = std::min<size_t>(MAX_NAME_LENGTH, name.size());
		for (size_t i = 0; i < size; ++i)
		{
				pTemp->name[i] = name[i];
		}
		pTemp->location = Location{ 7, 3 };
		players.push_back(pTemp);

		pTemp = new Player{};
		name = "Charlotte";
		std::fill(std::begin(pTemp->name), std::end(pTemp->name), '\0');
		size = std::min<size_t>(MAX_NAME_LENGTH, name.size());
		for (size_t i = 0; i < size; ++i)
		{
			pTemp->name[i] = name[i];
		}
		pTemp->location = Location{ 8,4 };
		players.push_back(pTemp);
	}
	const std::vector<Player*>& GetStructVector()
	{
		return players;
	}
	std::string Location::ToString() const
	{
		return "(" + std::to_string(x) + ", " + std::to_string(y) + ")";
	}
	std::string Player::ToString() const
	{
		return "Player " + std::string(name) + "\n\tLocation: " + location.ToString();
	}
}