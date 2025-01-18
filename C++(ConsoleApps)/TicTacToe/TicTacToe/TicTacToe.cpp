#include <iostream> 
using namespace std; 

void displayBoard(char board[3][3]) 
{

	for (int i = 0; i < 3; i++) 
	{
		for (int j = 0; j < 3; j++)
		{
			cout << board[i][j];
			if (j < 2) cout << " | ";
		}

		cout << endl;
		if (i < 2) cout << "--|---|--" << endl;
	}
}

char checkWinner(char board[3][3])
{
	for (int i = 0; i < 3; i++)
	{
		if (board[i][0] == board[i][1] && board[i][1] == board[i][2])
		{
			return board[i][0];
		}
	}

	for (int j = 0; j < 3; j++) {
		if (board[0][j] == board[1][j] && board[1][j] == board[2][j]) 
		{
			return board[0][j]; 
		}
	}

	if (board[0][0] == board[1][1] && board[1][1] == board[2][2]) {
		return board[0][0];
	}
	if (board[0][2] == board[1][1] && board[1][1] == board[2][0]) {
		return board[0][2];
	}

	return ' ';
}

bool makeMove(char board[3][3], int move, char player)
{
	int row = (move - 1) / 3;
	int col = (move - 1) % 3;

	if (board[row][col] != 'X' && board[row][col] != 'O')
	{
		board[row][col] = player;
		return true;
	}
	else
	{
		cout << "You can't move here!" << endl;
		return false;
	}
}

int main()
{
	char board[3][3] =
	{
		{'1', '2', '3'},
		{'4', '5', '6'},
		{'7', '8', '9'}
	};

	cout << "Welcome to Tic Tac Toe game!" << endl;

	char currentPlayer = 'X';
	for (int turn = 0; turn < 9; turn++)
	{
		displayBoard(board);

		char winner = checkWinner(board);
		if (winner != ' ')
		{
			cout << "Player" << winner << " won!" << endl;
		}

		int move;
		cout << "Player" << currentPlayer << ", put number (1-9)";
		cin >> move;

		if (move >= 1 && move <= 9)
		{
			if (makeMove(board, move, currentPlayer))
			{
				currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
			}
			else 
			{
				turn--;
			}
		}
		else
		{
			cout << "False, put number from 1 to 9" << endl;
			turn--;
		}

		winner = checkWinner(board);
		if (winner != ' ')
		{
			displayBoard(board);
			cout << "Player" << winner << " won!" << endl;
			return 0;
		}
	}

	displayBoard(board);
	cout << "End" << endl;

	return 0;
}