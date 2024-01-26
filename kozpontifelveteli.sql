-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Jan 17. 11:42
-- Kiszolgáló verziója: 10.4.6-MariaDB
-- PHP verzió: 7.3.8
CREATE DATABASE miniKIFIR DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `kozpontifelveteli`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `felvetelizok`
--

CREATE TABLE `felvetelizok` (
  `azonosito` varchar(11) COLLATE utf8_hungarian_ci NOT NULL,
  `nev` varchar(40) COLLATE utf8_hungarian_ci DEFAULT NULL,
  `email` varchar(50) COLLATE utf8_hungarian_ci DEFAULT NULL,
  `szuletesiDatum` date DEFAULT NULL,
  `ertesitesiCim` varchar(120) COLLATE utf8_hungarian_ci DEFAULT NULL,
  `matekPont` tinyint(4) DEFAULT NULL,
  `magyarPont` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `felvetelizok`
--

INSERT INTO `felvetelizok` (`azonosito`, `nev`, `email`, `szuletesiDatum`, `ertesitesiCim`, `matekPont`, `magyarPont`) VALUES
('12345678901', 'Kovács János', 'kovacsjanos12@gmail.com', '1998-02-15', '123 Kossuth tér, Budapest', 45, 40),
('23456789012', 'Nagy Eszter', 'nagyeszter23@gmail.com', '1995-07-22', '456 Ady Endre utca, Debrecen', 38, 45),
('34567890123', 'Kiss Béla', 'kissbela45@gmail.com', '1996-11-10', '789 Szent István tér, Szeged', 42, 50),
('45678901234', 'Molnár Éva', 'molnareva67@gmail.com', '1997-09-07', '101 Petőfi tér, Pécs', 35, 48),
('56789012345', 'Tóth Gábor', 'tothgabor89@gmail.com', '2000-05-30', '202 Kossuth utca, Székesfehérvár', 48, 42);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `felvetelizok`
--
ALTER TABLE `felvetelizok`
  ADD PRIMARY KEY (`azonosito`),
  ADD KEY `nev` (`nev`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
