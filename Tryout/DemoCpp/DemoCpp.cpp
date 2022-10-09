// DemoCpp.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

using namespace System;
using namespace System::Runtime::InteropServices;

[DllImport("msvcrt", CharSet = CharSet::Ansi)] extern "C" int puts(String^);

int main()
{
    String ^ pStr =  "Hello World!";
    puts(pStr);
}

