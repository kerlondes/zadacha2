# Разработка программного модуля для учета заявок на ремонт оргтехники

## Описание проекта

Проект направлен на создание программного модуля для учета и обработки заявок на ремонт оргтехники в офисных условиях. Модуль автоматизирует процессы приема заявок, отслеживания их статусов, назначения ответственных и анализа статистики. Это позволяет минимизировать время простоя оборудования и эффективно управлять ремонтом оргтехники.

## Цели проекта

- Учет и управление заявками на ремонт оргтехники.
- Ускорение процесса обработки заявок и выполнения ремонтных работ.
- Предоставление статистики по работам и выявление узких мест в процессе.

## Структура проекта

Проект состоит из следующих частей:

1. **Алгоритм обработки заявок**
2. **Модуль базы данных**
3. **Программный интерфейс для работы с заявками**
4. **Отчеты и статистика**
5. **Тестирование системы**

## Функциональные требования

### 1. Добавление заявок:

- Номер заявки
- Дата добавления
- Вид оргтехники
- Модель
- Описание проблемы
- ФИО клиента
- Номер телефона
- Статус заявки (новая, в процессе, завершена)

### 2. Редактирование заявок:

- Изменение этапа выполнения
- Обновление описания проблемы
- Изменение ответственного исполнителя

### 3. Отслеживание статуса заявки:

- Просмотр списка заявок
- Поиск заявок по номеру или параметрам
- Получение уведомлений о смене статуса

### 4. Назначение мастера:

- Присвоение мастера к заявке
- Отслеживание выполнения работы
- Комментарии и фиксация данных о запчастях

### 5. Статистика работы отдела обслуживания:

- Количество выполненных заявок
- Среднее время выполнения
- Статистика по типам неисправностей

## Нефункциональные требования

1. **Кроссплатформенность**: Поддержка ОС Windows.
2. **Безопасность**: Логин и пароль для доступа, разграничение прав доступа.
3. **Удобство использования**: Простой интерфейс, уведомления, подсказки.

## Алгоритм работы с заявками

1. **Создание заявки**:
   - Пользователь (клиент) оставляет заявку, указывая все необходимые параметры (описание проблемы, данные о технике и т.д.).
   
2. **Регистрация заявки**:
   - Оператор регистрирует заявку в системе, присваивает уникальный идентификатор, сохраняет всю информацию.

3. **Обработка заявки**:
   - Анализ заявки оператором, при необходимости назначение мастера.

4. **Исполнение заявки**:
   - Мастер выполняет ремонт, фиксирует статус выполнения и обновляет информацию о выполненных работах.

5. **Отчетность**:
   - После завершения ремонта генерируется отчет с информацией о времени, материалах и других затратах.


### Диаграмма сущностей

Диаграмма ER, показывающая связи между сущностями, будет включать следующие связи:

- Заявка может быть обработана одним мастером (многие к одному).
- Заявка может быть изменена несколько раз, что фиксируется в истории изменений.

## Алгоритм расчета статистики

Для расчета статистики работы отдела обслуживания можно использовать следующий алгоритм:

1. **Количество выполненных заявок**: суммируем все заявки со статусом "завершена".
2. **Среднее время выполнения**:
   - Для каждой завершенной заявки вычисляется время выполнения как разница между датой завершения и датой добавления.
   - Среднее время — это среднее значение всех временных интервалов.
3. **Статистика по типам неисправностей**:
   - Для каждой заявки подсчитывается количество заявок по каждому типу неисправности.

## Тестирование

Для тестирования системы разработаны следующие условия:

1. **Нормальные условия**:
   - Ввод корректных данных.
   - Обработка стандартных заявок.
   
2. **Экстремальные условия**:
   - Обработка заявок с отсутствующими данными.
   - Работа с большими объемами данных (множество заявок).

3. **Исключительные условия**:
   - Ошибки при подключении к базе данных.
   - Несоответствие формата входных данных (например, неправильный формат даты или телефона).

