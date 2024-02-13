TODO: add full architecture description

- treba zvazit, aky objem dat bude mat DB
- napr. 2 eventy kazdy den, z toho v priemere 3-5 tisic ticketov sa preda (sportove podujatie ma viac, koncert ovela viac, kino menej)
	- t.j. 3-5 tisic ticketov, 3-5 tisic seats - vsetko x2
	- pravdepodobne 

- ak ma byt milion userov, tak sa da predpokladat, ze sa rozsirila ponuka globalne
- aj S3 ulozisko (MinIO? alebo nejaka Azure sluzba)
- ak sa zmesti DB do RAM, tak je rychlejsie
- kolko poziadaviek za sekundu spracuje redis?
- Elastic Search