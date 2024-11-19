import os
import struct
import sys
import math

def writeResTxt(fName, result):
  
    with open(fName, 'w') as f:
        f.write(str(result))

def readResTxt(fName):
  
    result = 0.0
    try:
        if os.path.exists(fName):
            with open(fName, 'r') as f:
                result = float(f.read())
        else:
            raise FileNotFoundError(f"File {fName} not found.")
    except FileNotFoundError as e:
        print(e)
    return result

def writeResBin(fName, result):

    with open(fName, 'wb') as f:
        f.write(struct.pack('f', result))

def readResBin(fName):
  
    result = 0.0
    try:
        if os.path.exists(fName):
            with open(fName, 'rb') as f:
                result = struct.unpack('f', f.read())[0]
        else:
            raise FileNotFoundError(f"File {fName} not found.")
    except FileNotFoundError as e:
        print(e)
    return result

def calculate(x_deg):
   
    x_deg = x_deg % 360 
    if x_deg == 90 or x_deg == 270:
        print("Error: Division by zero for angles 90 or 270 degrees.")
        return None
    x_rad = math.radians(x_deg) 
    result = math.sin(x_rad) / math.cos(x_rad)
    return result



def writeErrorMsg(fName, message):
    with open(fName, 'w') as f:
        f.write(message)



if __name__ == "__main__":
    try:
        data = float(input("Enter angle in degrees (x): "))
        result = calculate(data)
        if result is not None:
            print(f"Result is: {result}")
            writeResTxt("textRes.txt", result)
            writeResBin("binRes.bin", result)
            print("Result from text file: {0}".format(readResTxt("textRes.txt")))
            print("Result from binary file: {0}".format(readResBin("binRes.bin")))
    except FileNotFoundError as e:
        print(e)
    except ValueError:
        error_msg = "Invalid input."
        print(error_msg)
        writeErrorMsg("textRes.txt",error_msg)
