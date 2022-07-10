//http://www.cplusplus.com/reference/cstring/
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

void StringConcatenationNoReturn();
char *StringConcatenationWithReturn();

//Press F6 to compile
int main()
{
    StringConcatenationNoReturn();
    
    char *str = StringConcatenationWithReturn();

    printf("Returned concatenated string: %s", str);

    free(str);

    return 0;
}

//Concatenating a string is easy if you don't plan on returning it from a function
void StringConcatenationNoReturn()
{
    //You need to specify the string length you will need, anything shorter and there will be a malfunction
    char str[8] = "Alpha";

    //The second parameter is appended to the end of the first parameter and stored in the first parameter
    strcat(str, "bet");

    printf("Concatenated string: %s\n", str);
}

//It gets more complicated when you need to return the string
//https://stackoverflow.com/a/25799033/603807
char *StringConcatenationWithReturn()
{
    char str[9] = "Alpha";

    strcat(str, "bet2");

    //printf("String length: %i\n", strlen(str));

    //Allocating space on the heap for the string so it can be returned as a pointer
    char *strReturn = (char*)malloc((strlen(str) + 1)*sizeof(char));
    
    strReturn = &str;

    return strReturn;
}
