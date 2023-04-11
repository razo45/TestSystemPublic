# Описание проекта "ASP.NET Core 3.1 Survey Application"  
Данный проект - это веб-приложение, созданное на базе фреймворка ASP.NET Core 3.1, которое позволяет пользователям создавать и проходить опросы.  

## Функциональность
В приложении реализованы следующие функции:

*  **Создание опросов** - пользователь может создавать опросы и задавать вопросы различных типов: вопросы с выбором одного ответа, вопросы с выбором нескольких ответов, вопросы с открытым ответом и другие.  

* **Прохождение опросов** - пользователь может пройти опрос, который был создан другим пользователем. При этом для каждого вопроса будет показано его содержание и список вариантов ответов. Для вопросов с открытым ответом пользователь может написать свой ответ в свободной форме.  

* **Анализ результатов** - создатель опроса может просмотреть результаты прохождения опроса, узнать количество пользователей, прошедших опрос, а также детальную статистику по каждому вопросу.  

## Технологии  
В проекте используется фреймворк ASP.NET Core 3.1, язык программирования C# и база данных Microsoft SQL Server.  

## Запуск проекта  
Для запуска проекта необходимо установить .NET Core 3.1 и Microsoft SQL Server. После клонирования репозитория необходимо настроить строку подключения к базе данных в файле appsettings.json и выполнить миграции командой "dotnet ef database update". После этого можно запустить проект командой "dotnet run".  

## Автор
Этот проект разработал Нерсесян Размик, fullstack-разработчик.  
