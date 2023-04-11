# Описание проекта "TestSystemPublic"  
Данный проект - это веб-приложение, созданное на базе фреймворка ASP.NET Core 3.1, которое позволяет пользователям создавать и проходить опросы.  

## Функциональность
В приложении реализованы следующие функции:

*  **Создание опросов** - пользователь может создавать опросы и задавать вопросы различных типов: вопросы с выбором одного ответа, вопросы в виде таблицы, вопросы с открытым ответом и другие. Вопросы помещаются в темы которые позволяют сортировать вопросы  

* **Прохождение опросов** - пользователь может пройти опрос, который был создан другим пользователем. При этом для каждого вопроса будет показано его содержание и список вариантов ответов. Для вопросов с открытым ответом пользователь может написать свой ответ в свободной форме.  

* **Анализ результатов** - создатель опроса может просмотреть результаты прохождения опроса, узнать количество пользователей, прошедших опрос, а также детальную статистику по каждому вопросу.  

## Технологии  
В проекте используется фреймворк ASP.NET Core 3.1, язык программирования C#, база данных Microsoft SQL Server, JavaScript.  

## Установка
Для запуска приложения необходимо:

1. Склонировать репозиторий:
```bash
Copy code
git clone https://github.com/razo45/TestSystemPublic.git
```
2. Открыть проект в Visual Studio или другой IDE для работы с C#.

3. Установить все необходимые зависимости, используя NuGet.

4. Создать базу данных через бэкап `BackUp` или через скрип `script.sql`

5. изменить строку модключения в 'Model1.cs'

4. Запустить приложение, нажав на кнопку "Запуск".  
## Планы по развитию   
:negative_squared_cross_mark: добавить возможность доступа к опросу по паролю или по ссылке.  
:negative_squared_cross_mark: добавить регистрацию и возможность создания опросов каждому пользователю, а не единственному администратору.  
:negative_squared_cross_mark: добавить главную страницу со списком популярных опросов и подписок на авторов.  
:negative_squared_cross_mark: добавить возможность редактировать логику обработки ответов и вывода результата пользователю.  
:negative_squared_cross_mark: переписать и оптимизировать структуру базы данных.  
## Автор
Этот проект разработал Нерсесян Размик, fullstack-разработчик.  
