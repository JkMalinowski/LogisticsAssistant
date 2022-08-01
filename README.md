# LogisticsAssistant

Temat aplikacji: Asystent logistyka – planowanie wyjazdów ciężarówek \
Technologia wykonania: ASP.NET Core MVC z użyciem bazy danych MS SQL \
Role w systemie: \
• Użytkownik niezalogowany – możliwość zalogowania do systemu; \
• Użytkownik zalogowany – możliwość zarządzania listą zapisanych ciężarówek w systemie i planowania wyjazdów; \
W systemie powinna być możliwość przechowywania listy ciężarówek na wyposażeniu firmy. Każda ciężarówka powinna zawierać: \
• prędkość maksymalną (Vmax), \
• przerwy kierowcy (np. wymagana liczba minut przerwy po zadanym czasie jazdy). \
Użytkownik powinien mieć możliwość edycji właściwości ciężarówek. Zmiany (w Vmax i/lub przerwach kierowcy) wchodzą w życie dopiero dla wyjazdów dodawanych w przyszłości.
Przykład: Użytkownik dodaje ciężarówkę w poniedziałek i ustawia jej przerwę 30 minut co 8h jazdy. Użytkownik tworzy plan wyjazdu na środę (do wyliczeń brane są aktualne na ten moment ustawienia). Następnie, we wtorek, Użytkownik zmienia przerwę na 30 minut co 4h jazdy. Nowe ustawienia zostaną wzięte pod uwagę dopiero przy dodawaniu kolejnego wyjazdu, a ten zaplanowany na środę pozostaje bez zmian. \
Do zaplanowania przejazdu użytkownik będzie musiał podać: \
• datę i godzinę od, \
• odległość do przebycia \
• ciężarówkę. \
Czas potrzebny na przejazd można wyliczyć na podstawie prędkości maksymalnej (zakładamy, że ciężarówka bez przerwy jedzie z prędkością maksymalną) biorąc pod uwagę również wymagane przerwy kierowcy. Planowane wyjazdy ciężarówki nie mogą na siebie nachodzić. \
Każdy zapisany plan wyjazdu powinien zawierać: \
• data i godzina od/do, \
• odległość do przebycia, \
• wybraną ciężarówkę, \
• opis/szczegóły przejazdu, \
• informację o czasie zapisania planu wyjazdu. \
W systemie powinna być możliwość podglądu zaplanowanych przejazdów (w tabeli lub na wykresie Gantta). \
Mile widziane będzie wykorzystanie Entity Framework Core (code first + migracje). \
