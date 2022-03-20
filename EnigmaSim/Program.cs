using System;
using System.Collections.Generic;

namespace EnigmaSim {
    class Program {
        static void Main(string[] args) {
            char[] rotorDescr10 = new char[26]{ 'A', 'A', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] rotorDescr1 = new char[26] { 'E', 'K', 'M', 'F', 'L', 'G', 'D', 'Q', 'V', 'Z', 'N', 'T', 'O', 'W', 'Y', 'H', 'X', 'U', 'S', 'P', 'A', 'I', 'B', 'R', 'C', 'J' };
            char[] rotorDescr2 = new char[26] { 'E', 'K', 'M', 'F', 'L', 'G', 'D', 'Q', 'V', 'Z', 'N', 'T', 'O', 'W', 'Y', 'H', 'X', 'U', 'S', 'P', 'A', 'I', 'B', 'R', 'C', 'J' };
            char[] rotorDescr3 = new char[26] { 'E', 'K', 'M', 'F', 'L', 'G', 'D', 'Q', 'V', 'Z', 'N', 'T', 'O', 'W', 'Y', 'H', 'X', 'U', 'S', 'P', 'A', 'I', 'B', 'R', 'C', 'J' };

            char[] reflectorDescr = new char[26] { 'E', 'J', 'M', 'Z', 'A', 'L', 'Y', 'X', 'V', 'B', 'W', 'F', 'C', 'R', 'Q', 'U', 'O', 'N', 'T', 'S', 'P', 'I', 'K', 'H', 'G', 'D' };

            // Create enigma machine object
            EnigmaMachine enigma1 = new EnigmaMachine();

            try {
                enigma1.FeedInRotor("I", rotorDescr1, 1, char.ToUpper('Q') - 65);
                enigma1.FeedInRotor("II", rotorDescr2, 1, char.ToUpper('E') - 65);
                enigma1.FeedInRotor("III", rotorDescr3, 1, char.ToUpper('V') - 65);
                enigma1.FeedInReflector(reflectorDescr);
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            string plaintext = "HELLOXWORLD";
            string encrypted = enigma1.Encrypt(plaintext);

            foreach (Rotor rotor in enigma1.rotors) {
                Console.WriteLine(rotor.ToString());
            }
        }

    }

    class EnigmaMachine {
        // Create a list of rotors.
        public List<Rotor> rotors = new List<Rotor>();
        public Reflector reflector;

        public EnigmaMachine() {

        }

        public string Encrypt(string plaintext) {
            string encrypted = "";

            foreach (char letter in plaintext) {
                char encryptedLetter = letter;
                Console.WriteLine("It's letter " + letter);
                // Go throught all rotors from right to left
                for (int i = rotors.Count - 1; i >= 0; i--) {
                    Console.Write("Rotor " + rotors[i].name + "  ");
                    encryptedLetter = rotors[i].Scramble(encryptedLetter, 1);
                    Console.WriteLine("Now it's letter: " + encryptedLetter);
                }
            }

            return encrypted;
        }

        public void FeedInRotor(string name, char[] wiring, int position, int notchPos) {
            int[] flag = new int[26];
            // Need to check whether parameters are correct 
            for (int i = 0; i < 26; i++) {
                int letterPos = char.ToUpper(wiring[i]) - 65;
                if (flag[letterPos] == 0) {
                    flag[letterPos] = 1;
                } else {
                    throw (new Exception("Error: Rotor description is not correct"));
                }
            }
            // Also check the position and notchPos values

            // Create rotor and add to the list
            rotors.Add(new Rotor(name, wiring, position, notchPos));
        }

        public void FeedInReflector(char[] wiring) {
            int[] flag = new int[26];
            // Need to check whether parameters are correct 
            for (int i = 0; i < 26; i++) {
                int letterPos = char.ToUpper(wiring[i]) - 65;
                if (flag[letterPos] == 0 && char.ToUpper(wiring[letterPos]) - 65 == i) {
                    flag[letterPos] = 1;
                } else {
                    throw (new Exception("Error: Reflector description is not correct"));
                }
            }

            reflector = new Reflector(wiring);
        }

        public int GetRotorNum() {
            return rotors.Count;
        }

    }

    class Rotor {
        public string name;
        public char[] wiring = new char[26]; // Input to output wiring 
        public int notchPos; // Notch position
        public int position;   // Rotator's current position

        public Rotor(string name, char[] wiring, int position, int notchPos) {  //Parameterized constructor
            this.name = name;
            this.wiring = wiring;
            this.position = position;
            this.notchPos = notchPos;
        }

        // 1 - from right to left, 0 - opposite
        public char Scramble(char letter, int direction) {
            if (direction == 1) {
                // Get the letter
                letter = wiring[char.ToUpper(letter) - 65]; 
            } else {
                // Find the corresponding letter position
                int letterPosition = Array.IndexOf(wiring, letter);
                letter = (char)(letterPosition + 65); // Convert to char
            }
            return letter;
        }

        public string ToString() {
            return "This is rotor " + name;
        }

    }
    class Reflector {
        char[] wiring = new char[26]; // Input to output wiring 

        public Reflector(char[] wiring) {  //Parameterized constructor
            this.wiring = wiring;
        }

        //public char[] Scramble() {

        //}

        public string toString() {
            return "This is reflector";
        }
    }
}
