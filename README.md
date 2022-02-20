# Alaktu

    𒀭

## Общее описание

    Алакту (древне ассирийский - дорога) - это селф-сервис система по перекачке данных

    Система оперирует различными источниками и местами назнаянеия. Система может преобразовывать неструктурированные данные в реляционный вид,
    осуществялть загрузку с учетом ключей, версионностью строк.

    Система загружает данные в многопоточном режиме.

## Компоненты

1. Alaktu Manager

    Веб приложение для управления и настройки пайплайнов перекачки данных

2. CoreDB

    Основная БД системы

3. EventDeliveryService

    сервис многопоточной перекачки данных
