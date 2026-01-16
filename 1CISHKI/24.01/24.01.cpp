#include <iostream>
using namespace std;

// 1. Добавить строку в конец
int** addEnd(int** arr, int& rows, int cols) {
    int** newArr = new int*[rows+1];
    for(int i=0; i<rows; i++) newArr[i] = arr[i];
    newArr[rows] = new int[cols]{0};
    rows++;
    delete[] arr;
    return newArr;
}

// 2. Добавить строку в начало
int** addStart(int** arr, int& rows, int cols) {
    int** newArr = new int*[rows+1];
    newArr[0] = new int[cols]{-1};
    for(int i=0; i<rows; i++) newArr[i+1] = arr[i];
    rows++;
    delete[] arr;
    return newArr;
}

// 3. Добавить строку в позицию pos
int** addPos(int** arr, int& rows, int cols, int pos) {
    int** newArr = new int*[rows+1];
    for(int i=0; i<pos; i++) newArr[i] = arr[i];
    newArr[pos] = new int[cols]{99};
    for(int i=pos; i<rows; i++) newArr[i+1] = arr[i];
    rows++;
    delete[] arr;
    return newArr;
}

// 4. Удалить строку pos
int** delRow(int** arr, int& rows, int cols, int pos) {
    delete[] arr[pos];
    int** newArr = new int*[rows-1];
    for(int i=0, j=0; i<rows; i++) 
        if(i!=pos) newArr[j++] = arr[i];
    rows--;
    delete[] arr;
    return newArr;
}

int main() {
    int rows=3, cols=4;
    int** arr = new int*[rows];
    for(int i=0; i<rows; i++) {
        arr[i] = new int[cols];
        for(int j=0; j<cols; j++) arr[i][j] = i*cols+j;
    }
    
    arr = addEnd(arr, rows, cols);
    arr = addStart(arr, rows, cols);
    arr = addPos(arr, rows, cols, 2);
    arr = delRow(arr, rows, cols, 3);
    
    // Очистка памяти
    for(int i=0; i<rows; i++) delete[] arr[i];
    delete[] arr;
}
