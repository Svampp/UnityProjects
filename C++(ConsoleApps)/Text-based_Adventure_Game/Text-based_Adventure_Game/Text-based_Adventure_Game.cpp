#include <iostream>
#include <string>

void showRoomDescription(int room)
{
    switch(room) 
    {
    case 1:
        std::cout << "";
		break;


	default:
		std::cout << "Invalid room number";
    }
}

int handleInput(int room)
{
	std::string input;
	std::cout << "What would you like to do? ";
	std::getline(std::cin, input);

	if (room == 1)
	{
		if (input == "///")
		{
			return 2;
		}
	}
}

int main()
{
	int currentRoom = 1;
	bool gameRunnig = true;

	std::cout << "Welcome to the game!";

	while (gameRunnig)
	{
		showRoomDescription(currentRoom);
		currentRoom = handleInput(currentRoom);

		if (currentRoom == 2)
		{
			gameRunnig = false;
		}
	}

	return 0;
}
