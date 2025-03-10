#include <iostream>
#include <string>

using namespace std;

void Room2();  

void Room1() {
    cout << "You are in a large, dark room. It is slightly illuminated only by a few torches on the walls. You hear water dripping from the ceiling.\n";
    cout << "A very strange and somewhat scary place, you think\n";
    cout << "You see two doors, one leads to the south, the other to the north.\n";

    cout << "1. Go to the north door\n";
    cout << "2. Go to the south door\n";

    int choice;
    cin >> choice;

    switch (choice)
    {
    case 1:
        cout << "Damn, there's a lock on the door, you need a key to open it.\n";
        Room1();
        break;
    case 2:
        cout << "Hmm this door seems to be open...\n";
        cout << "What do you want to do?\n";

        cout << "1. Go through the door\n";
        cout << "2. Go back\n";

        int subChoice;
        cin >> subChoice;
        switch (subChoice)
        {
        case 1:
            Room2();
            break;
        case 2:
            Room1();
            break;
        default:
            cout << "Invalid choice. Try again.\n";
            Room1();  
        }
        break;  
    default:
        cout << "Try again.\n";
        Room1();
    }
}

void Room2() {
    cout << "You entered a smaller room, unfortunately it was just as poorly lit as the first one.\n";
    cout << "It also has two doors, one leading to the east and the other to the west.\n";

	cout << "1. Go to the east door\n";
	cout << "2. Go to the west door\n";
    cout << "3. Look around the room carefully\n";
	cout << "4. Go back\n";

    int choice;
	cin >> choice;

    switch (choice)
    {
    case 1:
        cout << "Go inside?";

		cout << "1. Yes\n";
		cout << "2. No\n";

		int subChoice;
		cin >> subChoice;
        switch (subChoice)
        {
        case 1:
            Room3();
            break;

        case 2:
			Room2();
			break;
        }
    }
}

void Room3() 
{

}

int main() {
    cout << "Welcome to the text quest! To select actions, simply enter the number of the desired action\n";
    Room1();
    return 0;
}
