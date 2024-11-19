import sys


rows_num = int(input("Enter the size:"))
lst = []

filler = input("Enter the filler character:")

if len(filler) != 1:
    print("Eror")
    sys.exit(1)


for i in range(rows_num):
    lst.append([])
    for j in range(rows_num):
        current_filler = '*' if i % 2 == 1 else filler

        if j >= i and j < rows_num - i:
            lst[i].append(current_filler)
        else:
            lst[i].append(" ")
        
        print(lst[i][j], end=" ")
    print()