#pragma once

#include <iostream>
#include "../CPPCLI.Library/CaculateData.h"

public ref class InvokeCon
{
public:
	InvokeCon();

	int SumCli(int left, int right);
	int SubCli(int left, int right);
	int MultCli(int left, int right);
	int DividedCli(int left, int right);
};

