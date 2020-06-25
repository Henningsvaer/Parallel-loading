# Parallel-loading project


## ParallelLoading


* **ParallelLoading** - основной проект.
* **WriteReadClassLib** - вспомогательный проект с библиотекой классов.


* Количество потоков задается в папке **AppConfigure -> customConfig.txt**
* Параметры подключения к базе данных в **App.config**.


## WriteReadClassLib


* **DataManagement -> InputData -> input.txt** Текстовый файл с входными данными.
* **WriteReadClassLib/bin/Debug/DataManagement/OutputData/outputLog.txt** Текстовый файл с логами.


* **DataManager.cs** -> Управление вводом и выводом.
* **DbWriter.cs** -> запись в бд.


* **Person.cs** -> Класс для десериализации данных.
* **Report** -> Классы и интерфейсы, отвечающие за вывод информации в разном виде. (Управление выводом происходит в **DataManager**)
