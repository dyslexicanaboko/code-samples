//http://www.cplusplus.com/reference/cstring/
#include <stdio.h>
#include <string.h>

//Press F6 to compile
int main()
{
    //You need to specify the string length you will need, anything shorter and there will be a malfunction
    char str[8] = "Alpha";

    //The second parameter is appended to the end of the first parameter and stored in the first parameter
    strcat(str, "bet");

    printf("Concatenated string: %s", str);

    return 0;
}
