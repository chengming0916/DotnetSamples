#pragma once

#include <stdio.h>
#include <stdlib.h>
#include <iostream>

using namespace std;

#ifdef CaculateDLL_EXPORTS
#define Calculate_EXPORTS  _declspec(dllexport)
#else
#define Calculate_EXPORTS  _declspec(dllimport)
#endif // CaculateDLL_EXPORTS

extern "C" Calculate_EXPORTS  int Sum(int left, int right);
extern "C" Calculate_EXPORTS  int Sub(int left, int right);
extern "C" Calculate_EXPORTS  int Mult(int left, int right);
extern "C" Calculate_EXPORTS  int Divided(int left, int right);

class Caculate
{
public:
	Caculate();
	~Caculate();
};

