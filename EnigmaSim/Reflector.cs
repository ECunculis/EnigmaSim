using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaSim {
    class Reflector {
        char[] wiring = new char[26]; // Input to output wiring 

        public Reflector(char[] wiring) {  //Parameterized constructor
            this.wiring = wiring;
        }

        public char Scramble(char letter) {
            letter = wiring[char.ToUpper(letter) - 65];
            return letter;
        }

        public string toString() {
            return "This is reflector";
        }
    }
}
