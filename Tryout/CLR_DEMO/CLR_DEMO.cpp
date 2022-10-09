#include "pch.h"

using namespace System;
using namespace System::Runtime::InteropServices;

[DllImport("msvcrt", CharSet = CharSet::Ansi)] extern "C" int puts(String^);

int main()
{
    String^ pStr = "Hello World!";
    puts(pStr);
    return 0;
}
