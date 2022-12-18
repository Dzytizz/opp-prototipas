(c) Kamuoliai 2022
---
IFF-9/2 Vaiva Šafranavičiūtė, IFF-9/2 Gerda Žvirblytė, IFF-9/2 Paulius Petrauskas, IFF-9/2 Gytis Dokšas  

Sistemos paleidimas:  
1. Padaryti bibliotekos (opp-library) "Build" (VS2022: "Build" -> "Build opp-library").  
2. Pasileisti serverį (opp-server) (VS2022: "Debug" -> "Start Without Debugging").  
3. Pasileisti klientą (opp-client) (VS2022: "Debug" -> "Start Without Debugging").  

Lokalus multiplayer:  
1. opp-server "Program.cs" faile pakeisti 27 eilutėje iš "localhost" į IPv4 IP (Command Prompt -> `ipconfig`)  
2. opp-client "ClientWindow.cs" 35 eilutėje pakeisti iš "localhost" į serveryje įvestą IPv4 IP.  

Valdymas:  
W/A/S/D - judėjimas  
K - kamuolio spyrimas  
