-- phpMyAdmin SQL Dump
-- version 4.5.5.1
-- http://www.phpmyadmin.net
--
-- Anamakine: 127.0.0.1
-- Üretim Zamanı: 02 Tem 2016, 23:45:47
-- Sunucu sürümü: 5.7.11
-- PHP Sürümü: 5.6.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Veritabanı: `bechat`
--

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `admin`
--

CREATE TABLE `admin` (
  `kullanici_adi` varchar(15) NOT NULL,
  `parola` varchar(15) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Tablo döküm verisi `admin`
--

INSERT INTO `admin` (`kullanici_adi`, `parola`) VALUES
('bahax41', 'baha123321');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `ana_sayfa`
--

CREATE TABLE `ana_sayfa` (
  `isim` varchar(15) NOT NULL,
  `mesajlar` varchar(255) NOT NULL,
  `saat` varchar(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `grup_sohbet`
--

CREATE TABLE `grup_sohbet` (
  `isim` varchar(15) NOT NULL,
  `mesajlar` varchar(255) NOT NULL,
  `saat` varchar(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `kullanici_bilgileri`
--

CREATE TABLE `kullanici_bilgileri` (
  `kullanici_adi` varchar(15) NOT NULL,
  `parola` varchar(15) NOT NULL,
  `e_mail` varchar(25) NOT NULL,
  `ad` varchar(10) NOT NULL,
  `soyad` varchar(10) NOT NULL,
  `profil_resmi` varchar(255) NOT NULL,
  `yas` varchar(2) NOT NULL,
  `cinsiyet` varchar(5) NOT NULL,
  `telefon` varchar(11) NOT NULL,
  `uyelik_tarihi` varchar(25) NOT NULL,
  `ip_adresi` varchar(15) NOT NULL,
  `ban` varchar(2) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `kullanici_bilgileri`
--

INSERT INTO `kullanici_bilgileri` (`kullanici_adi`, `parola`, `e_mail`, `ad`, `soyad`, `profil_resmi`, `yas`, `cinsiyet`, `telefon`, `uyelik_tarihi`, `ip_adresi`, `ban`) VALUES
('bahadir41', 'baha123', 'kartalm755@gmail.com', 'Bahadircan', 'Top', 'http://radyobaha.pe.hu/KullaniciResimleri/bahadir41.png', '17', 'Bay', '05356848814', '28.06.2016', '85.108.55.243', '0'),
('Bahax41', 'baha123321', 'bahax41@gmail.com', 'Bahadir', 'Düzcan', 'http://radyobaha.pe.hu/KullaniciResimleri/Bahax41.png', '17', 'Bay', '05356011120', '25.06.2016', '82.145.220.234', '0'),
('Nacres', '123', 'lordxangeon@gmail.com', 'Sercan', 'Denoglu', 'http://radyobaha.pe.hu/KullaniciResimleri/Nacres.png', '17', 'Bay', '05413021424', '28.06.2016', '85.108.55.243', '0'),
('test', 'test1', 'test@gmail.com', 'tester', 'tuaster', '', '17', 'Bay', '05356011125', '03.07.2016', '88.245.242.102', '0');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `online`
--

CREATE TABLE `online` (
  `ad` varchar(10) NOT NULL,
  `kullanici_adi` varchar(15) NOT NULL,
  `parola` varchar(15) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Tablo döküm verisi `online`
--

INSERT INTO `online` (`ad`, `kullanici_adi`, `parola`) VALUES
('Bahadir', 'Bahax41', 'baha123321');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `ozel_sohbet`
--

CREATE TABLE `ozel_sohbet` (
  `isim` varchar(15) NOT NULL,
  `mesaj` varchar(255) NOT NULL,
  `saat` varchar(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dökümü yapılmış tablolar için indeksler
--

--
-- Tablo için indeksler `kullanici_bilgileri`
--
ALTER TABLE `kullanici_bilgileri`
  ADD PRIMARY KEY (`kullanici_adi`);

--
-- Tablo için indeksler `online`
--
ALTER TABLE `online`
  ADD PRIMARY KEY (`kullanici_adi`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
