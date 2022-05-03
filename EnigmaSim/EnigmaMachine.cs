using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaSim {
    class EnigmaMachine {
        // Create a list of rotors.
        public List<Rotor> rotors = new List<Rotor>();
        public List<Rotor> rotorsInit = new List<Rotor>();
        public Reflector reflector;
        public PluckBoard pluckBoard;

        private bool _pluckboardExists = false;

        public bool PluckboardExists {
            get => _pluckboardExists;
            set => _pluckboardExists = value;
        }

        public string Encrypt(string plaintext) {
            string encrypted = "";
            int counter = 0;

            foreach (char letter in plaintext) {
                // Advance the right most rotor by 1
                counter++;
                //Console.WriteLine("It's letter number:" + counter);
                // Create the array of rotors which must be rotated next turn:
                bool[] rotorRotate = new bool[rotors.Count];

                // Need to check the position of notches of each rotor,
                // to decide which rotors must be rotated. For instance, if there are 4 rotors
                // positioned in the following way: 1, 2, 3, 4, then if the 3rd rotor's notch
                // is positioned so that the 2nd rotor must be rotated next time,
                // 3rd rotor must be rotated as well.
                for (int i = 1; i < rotors.Count; i++) {
                    if (rotors[i].notchPos == 0) {
                        rotorRotate[i] = rotorRotate[i - 1] = true;
                    }
                }

                rotorRotate[rotors.Count - 1] = true; // should always be rotated

                for (int i = 0; i < rotors.Count; i++) {
                    if (rotorRotate[i]) rotors[i].Rotate();
                }

                char encryptedLetter = letter;
                // The current passes through the plugboard once on its way to the rotors and another time on its way to the light bulbs
                if (PluckboardExists) encryptedLetter = pluckBoard.Swap(encryptedLetter);

                // Go throught all rotors from right to left
                for (int i = rotors.Count - 1; i >= 0; i--) {
                    encryptedLetter = rotors[i].Scramble(encryptedLetter, 1);
                }

                // Go throught reflector
                encryptedLetter = reflector.Scramble(encryptedLetter);
                // Go back thought rotors from left to right
                for (int i = 0; i < 3; i++) {
                    encryptedLetter = rotors[i].Scramble(encryptedLetter, 0);
                }
                // Go throught pluckboard again
                if (PluckboardExists) encryptedLetter = pluckBoard.Swap(encryptedLetter);

                encrypted += encryptedLetter;
                // Advance the rotors one step forward
                // Advance first rotor by 1 step

                Console.WriteLine("");
            }

            return encrypted;
        }

        public void FeedInRotor(string name, char[] wiring, char position, int notchPos) {
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
            rotorsInit.Add(new Rotor(name, wiring, position, notchPos));
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
        public void Reset() {
            this.rotors = rotorsInit.ConvertAll(x => new Rotor(x));
        }

        public void SetPluckBoard(PluckBoard pluckBoard) {
            this.pluckBoard = pluckBoard;
        }

        public int GetRotorNum() {
            return rotors.Count;
        }

    }
}
