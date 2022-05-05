<h1 align="center">Enigma I simulator</h1>

## Table of Contents
1. [Description](#description)
2. [About Enigma I machine](#about-enigma-i-machine)

## Description
This is Enigma I encryption machine simulator fully written in C#, which lets you to encrypt and decrypt the messages. Once run, you will be asked to provide three rotor numbers out of five existing ones. They will be fed into model from left to right. For each rotor it is necessary to provide the ring position. After that provide the pluckboard settings. To ignore pluckboard, press enter, otherwise enter the pluckboard setting which comprises of letter pairs separated with space. Finally, enter the text which you want to encrypt. Keep in mind that enigma machine is capable to encrypt only letters, that is why all spaces will be substituted with "X". The encryption and decryption processes are symmetric which means that to decrypt the message that was encrypted you have to use the same rotor and pluckboard settings which was set up before encryption process and then type the encrypted text. 

![image](https://user-images.githubusercontent.com/31374191/166457445-bbe12802-a7d4-4538-8338-821814010360.png)

## About Enigma I machine
The Enigma machine is a cipher device developed and used in the early to mid-20th century to protect commercial, 
diplomatic, and military communication. It was employed extensively by Nazi Germany during World War II, in all branches of the German military. 
The Enigma machine was considered so secure that it was used to encipher the most top-secret messages.

There are many Enigma models, but this application simulates the Enigma I model. Initially, the machine was supplied with three coding wheels, that could be 
inserted in any of 6 possible orders (3 x 2 x 1). In December 1938, two additional wheels were supplied, bringing the total number of possible wheel orders 
to 60 (5 x 4 x 3), a 10-fold increase in cipher security! [[1]](#1)


The machine was used throughout WWII and is known under various names. It is officially known as Enigma I and by its factory designator: Ch.11a.



## References
<a id="1">[1]</a> 
Enigma I The Service Enigma Machine [Internet].
Crypto Museum [updated: 28 April 2022; cited 2022 May 3]
Available from: https://www.cryptomuseum.com/crypto/enigma/index.htm
