// Copyright Epic Games, Inc. All Rights Reserved.

#include "CombatPrototypeGameMode.h"
#include "CombatPrototypeCharacter.h"
#include "UObject/ConstructorHelpers.h"

ACombatPrototypeGameMode::ACombatPrototypeGameMode()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnBPClass(TEXT("/Game/ThirdPerson/Blueprints/BP_ThirdPersonCharacter"));
	if (PlayerPawnBPClass.Class != NULL)
	{
		DefaultPawnClass = PlayerPawnBPClass.Class;
	}
}
