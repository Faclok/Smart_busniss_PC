-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Мар 16 2023 г., 15:27
-- Версия сервера: 10.8.4-MariaDB
-- Версия PHP: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `Smart`
--

-- --------------------------------------------------------

--
-- Структура таблицы `buyHistoryPull`
--

CREATE TABLE `buyHistoryPull` (
  `id` int(11) NOT NULL,
  `idClient` int(11) NOT NULL,
  `date` date NOT NULL,
  `state` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `priceConst` decimal(10,0) NOT NULL,
  `idProducts` text COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `clients`
--

CREATE TABLE `clients` (
  `id` int(11) NOT NULL,
  `name` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `firstBuy` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `loginPull`
--

CREATE TABLE `loginPull` (
  `id` int(11) NOT NULL,
  `idUser` int(11) NOT NULL,
  `date` date NOT NULL,
  `nameMachine` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `oc` text COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `machine`
--

CREATE TABLE `machine` (
  `id` int(11) NOT NULL,
  `name` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `dataSet` date NOT NULL,
  `description` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `icon` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `isActive` tinyint(1) NOT NULL,
  `startWorkDate` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `machineWorkPull`
--

CREATE TABLE `machineWorkPull` (
  `id` int(11) NOT NULL,
  `idMachine` int(11) NOT NULL,
  `dateStart` date NOT NULL,
  `dateEnd` date NOT NULL,
  `state` text COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `objectInStock`
--

CREATE TABLE `objectInStock` (
  `id` int(11) NOT NULL,
  `name` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `description` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `dataSet` date NOT NULL,
  `icon` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `typeMeasure` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `valueMeasure` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `count` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `priceChangePull`
--

CREATE TABLE `priceChangePull` (
  `id` int(11) NOT NULL,
  `idProduct` int(11) NOT NULL,
  `date` date NOT NULL,
  `state` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `priceNew` decimal(10,0) NOT NULL,
  `pricePrev` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `product`
--

CREATE TABLE `product` (
  `id` int(11) NOT NULL,
  `name` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `description` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `dataSet` date NOT NULL,
  `price` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `stockPull`
--

CREATE TABLE `stockPull` (
  `id` int(11) NOT NULL,
  `idObject` int(11) NOT NULL,
  `date` date NOT NULL,
  `state` text COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `login` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `access` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `property` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `dataSet` date NOT NULL,
  `lastTimeOnline` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Дамп данных таблицы `users`
--

INSERT INTO `users` (`id`, `login`, `password`, `name`, `access`, `property`, `dataSet`, `lastTimeOnline`) VALUES
(1, 'dsf', 'sdfds', 'sdfsd', 'fdsfsd', 'dfsd', '2023-03-16', '2023-03-16'),
(2, 'dsf', 'sdfds', 'sdfsd', 'fdsfsd', 'dfsd', '2023-03-16', '2023-03-16'),
(3, 'rreg', 'dfsdf', 'sdfsd', 'sdfds', 'sfds', '2023-03-16', '2023-03-16'),
(4, 'rreg', 'dfsdf', 'sdfsd', 'sdfds', 'sfds', '2023-03-16', '2023-03-16');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `buyHistoryPull`
--
ALTER TABLE `buyHistoryPull`
  ADD KEY `id` (`id`);

--
-- Индексы таблицы `clients`
--
ALTER TABLE `clients`
  ADD KEY `id` (`id`);

--
-- Индексы таблицы `loginPull`
--
ALTER TABLE `loginPull`
  ADD KEY `id` (`id`);

--
-- Индексы таблицы `machine`
--
ALTER TABLE `machine`
  ADD KEY `id` (`id`);

--
-- Индексы таблицы `machineWorkPull`
--
ALTER TABLE `machineWorkPull`
  ADD KEY `id` (`id`);

--
-- Индексы таблицы `objectInStock`
--
ALTER TABLE `objectInStock`
  ADD KEY `id` (`id`);

--
-- Индексы таблицы `priceChangePull`
--
ALTER TABLE `priceChangePull`
  ADD KEY `id` (`id`);

--
-- Индексы таблицы `product`
--
ALTER TABLE `product`
  ADD KEY `id` (`id`);

--
-- Индексы таблицы `stockPull`
--
ALTER TABLE `stockPull`
  ADD KEY `id` (`id`);

--
-- Индексы таблицы `users`
--
ALTER TABLE `users`
  ADD KEY `id` (`id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `buyHistoryPull`
--
ALTER TABLE `buyHistoryPull`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `clients`
--
ALTER TABLE `clients`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `loginPull`
--
ALTER TABLE `loginPull`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `machine`
--
ALTER TABLE `machine`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `machineWorkPull`
--
ALTER TABLE `machineWorkPull`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `objectInStock`
--
ALTER TABLE `objectInStock`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `priceChangePull`
--
ALTER TABLE `priceChangePull`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `product`
--
ALTER TABLE `product`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `stockPull`
--
ALTER TABLE `stockPull`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
