#include "InvokeCon.h"

InvokeCon::InvokeCon()
{

}

int InvokeCon::SumCli(int left, int right)
{
	return Sum(left, right);
}

int InvokeCon::SubCli(int left, int right)
{
	return Sub(left, right);
}

int InvokeCon::MultCli(int left, int right)
{
	return Mult(left, right);
}

int InvokeCon::DividedCli(int left, int right)
{
	return Divided(left, right);
}
