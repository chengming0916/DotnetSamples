#include "CaculateData.h"

Calculate_EXPORTS  int Sum(int left, int right)
{
	return left + right;
}

Calculate_EXPORTS  int Sub(int left, int right)
{
	return left - right;
}

Calculate_EXPORTS  int Mult(int left, int right)
{
	return left * right;
}

Calculate_EXPORTS  int Divided(int left, int right)
{
	if (right == 0) {
		std::cout << "除数不能为0" << std::endl;
		return 0;
	}

	return left / right;
}

Caculate::Caculate() {}
Caculate::~Caculate() {}