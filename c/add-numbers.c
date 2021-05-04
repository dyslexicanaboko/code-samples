int AddNumbers(int x, int y);

//Press F6 to compile
int main()
{
    int z = AddNumbers(1, 6);

    printf("Blah: %i\n", z);
 
    return 0;
}

int AddNumbers(int x, int y)
{
    int z = x + y;

    return z;
}