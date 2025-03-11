#include <iostream>
#include <string>
#include <windows.h>

using namespace std;

bool staffTaken = false;
bool hasKey = false;

void Room1();
void Room2();
void Room3();
void Room4();

void SetColor(int color) {
    SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), color);
}

bool ConfirmEntry() {
    cout << "\nGo inside?\n1. Yes\n2. No\n";
    int choice;
    cin >> choice;
    return choice == 1;
}

void Room1() {
    while (true) {
        cout << "\nYou are in a large, dark room. It is slightly illuminated only by a few torches on the walls.\n";
        SetColor(6);
        cout << "A very strange and somewhat scary place, you think.\n\n";
        SetColor(7);
        cout << "You see two doors: one to the ";
        SetColor(4); cout << "south"; SetColor(7);
        cout << " and one to the ";
        SetColor(1); cout << "north\n"; SetColor(7);

        cout << "1. Go to the north door\n2. Go to the south door\n";
        int choice;
        cin >> choice;

        if (choice == 1) {
            SetColor(6);
            cout << "\nDamn, there's a lock on the door, you need a key to open it.\n";
            SetColor(7);
        }
        else if (choice == 2) {
            SetColor(6);
            cout << "\nHmm, this door seems to be open...\n";
            SetColor(7);
            if (ConfirmEntry()) {
                Room2();
                return;
            }
        }
        else {
            cout << "Invalid choice. Try again.\n";
        }
    }
}

void Room2() {
    while (true) {
        cout << "\nYou entered a smaller room, just as poorly lit as the first one.\n\n";
        cout << "It has two doors: one to the ";
		SetColor(5); cout << "east "; SetColor(7);
        cout << "and one to the ";
		SetColor(2); cout << "west\n"; SetColor(7);
        cout << "1. Go to the east door\n2. Go to the west door\n3. Look around the room\n4. Go back\n";

        int choice;
        cin >> choice;

        if (choice == 1 && ConfirmEntry()) {
            Room3();
            return;
        }
        else if (choice == 2 && ConfirmEntry()) {
            Room4();
            return;
        }
        else if (choice == 3) {
            cout << "\nYou see a note on the floor.\nRead it?\n1. Yes\n2. No\n";
            int noteChoice;
            cin >> noteChoice;
			
            if (noteChoice == 1) {
				SetColor(6); cout << "\nThe wind blows to the west\nFive drops are falling from the sky\nOnly eight birds fly through the clouds\nAnd two deer standing in the field\nWaiting for the ninth full moon\n"; SetColor(7);
            }
        }
        else if (choice == 4) {
            Room1();
            return;
        }
        
    }
}

void Room3() {
    while (true)
    {
        if (staffTaken == false) 
        {
            cout << "\nYou walk into the room and see";
            SetColor(4); cout << "dragon\n"; SetColor(7);
            cout << "He doesn't seem to be very happy that you came in,\nHe growls at you and makes such a big flap of his wings that you fly out of the room.\n";
            Room2();
		}
        else if (staffTaken == true)
        {
            cout << "\nYou walk into the room and see";
            SetColor(4); cout << " dragon\n"; SetColor(7);
            cout << "He doesn't seem to be very happy that you came in,\nHe growls at you\nBut you remember that you have";
			SetColor(3); cout << "magic staff\n"; SetColor(7);
            cout << "So you wave it and a bright beam of light comes out of the crystal and the dragon disappears.";
        }
    }
}

void Room4() {
    while (true)
    {
        cout << "\nYou enter a well-lit room.\n";
        SetColor(6); cout << "finally..\n"; SetColor(7);
        cout << "In the middle of the room you see a chest.\n\n";
        cout << "What you wanna do?\n1.Go closer to the chest\n2.Look around the room\n3.Go back\n";
        
        int choice;
        cin >> choice;

        if (choice == 1 && hasKey == false)
        {
			cout << "\nYou come closer to the chest, there is a combination lock on it.\n";
            cout << "To return, enter 1\n";
			cout << "\nEnter the combination: ";
			int combination;
			cin >> combination;
			if (combination == 5829)
			{
				cout << "\nYou opened the chest and found a ";
				SetColor(2); cout << "key"; SetColor(7);
				cout << " inside.\n";
				hasKey = true;
			}
            else if (combination == 1)
            {
                Room4();
            }
			else
			{
				cout << "\nYou entered the wrong combination.\n";
			}
        }
        else if (choice == 1 && hasKey == true) 
        {
            cout << "\nYou have already taken the key.\n";
        }
        else if (choice == 2 && staffTaken == false)
        {
            cout << "\nYou find a ";
            SetColor(3); cout << "magic staff"; SetColor(7);
            cout << ", it's a long wooden stick with a beautiful blue crystal on the end.\n\n";

            cout << "What you wanna do?\n1. Take the staff\n2. Leave it\n";

            int staffChoice;
            cin >> staffChoice;

            if (staffChoice == 1)
            {
                staffTaken = true;
                cout << "\nYou took the staff.\n";
            }
            else if (staffChoice == 2)
            {
                cout << "\nYou left the staff.\n";
            }
        }
        else if (choice == 2 && staffTaken == true) 
        {
            cout << "\nIt looks like there is nothing else in the room.\n";
        }
        else if (choice == 3)
        {
            Room2();
        }
    }
    
}

int main() {
    cout << "Welcome to the text quest! To select actions, enter the number of the desired action.\n";
    Room1();
    return 0;
}
