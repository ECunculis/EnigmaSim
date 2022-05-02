using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaSim {
    class PluckBoard {
        public char[] connections = new char[26]; // letters which are connected

        public PluckBoard(string settings) {
            settings = settings.ToUpper();
            for (int i = 0; i < settings.Length; i += 3) {
                connections[settings[i] - 65] = settings[i + 1];
                connections[settings[i + 1] - 65] = settings[i];
            }
        }

        public char Swap(char letter) {
            // Return the same letter if it is not connected
            return connections[letter - 65] == 0 ? letter : connections[letter - 65];
        }
    }
}
