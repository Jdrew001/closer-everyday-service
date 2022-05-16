CREATE DATABASE  IF NOT EXISTS `ceddb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `ceddb`;
-- MySQL dump 10.13  Distrib 8.0.26, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: ceddb
-- ------------------------------------------------------
-- Server version	8.0.26

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `comment`
--

DROP TABLE IF EXISTS `comment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comment` (
  `idcomment` varchar(255) NOT NULL,
  `comment_value` varchar(360) NOT NULL,
  `milestone_id` varchar(255) NOT NULL,
  `user_id` varchar(255) NOT NULL,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT NULL,
  PRIMARY KEY (`idcomment`),
  UNIQUE KEY `idcomment_UNIQUE` (`idcomment`),
  UNIQUE KEY `idcomment` (`idcomment`),
  UNIQUE KEY `idcomment_2` (`idcomment`),
  UNIQUE KEY `idcomment_3` (`idcomment`),
  UNIQUE KEY `idcomment_4` (`idcomment`),
  UNIQUE KEY `idcomment_5` (`idcomment`),
  UNIQUE KEY `idcomment_6` (`idcomment`),
  UNIQUE KEY `idcomment_7` (`idcomment`),
  KEY `milestone_comment_id_idx` (`milestone_id`),
  KEY `user_comment_id_idx` (`user_id`),
  CONSTRAINT `milestone_comment_id` FOREIGN KEY (`milestone_id`) REFERENCES `milestone` (`idmilestone`) ON DELETE CASCADE,
  CONSTRAINT `user_comment_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `device`
--

DROP TABLE IF EXISTS `device`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `device` (
  `iddevice` varchar(255) NOT NULL,
  `model` varchar(45) NOT NULL,
  `platform` varchar(45) NOT NULL,
  `uuid` varchar(45) NOT NULL,
  `manufacturer` varchar(45) NOT NULL,
  `user_id` varchar(255) NOT NULL,
  `active` tinyint NOT NULL,
  PRIMARY KEY (`iddevice`),
  UNIQUE KEY `iddevice_UNIQUE` (`iddevice`),
  UNIQUE KEY `iddevice` (`iddevice`),
  UNIQUE KEY `iddevice_2` (`iddevice`),
  UNIQUE KEY `iddevice_3` (`iddevice`),
  UNIQUE KEY `iddevice_4` (`iddevice`),
  UNIQUE KEY `iddevice_5` (`iddevice`),
  UNIQUE KEY `iddevice_6` (`iddevice`),
  UNIQUE KEY `iddevice_7` (`iddevice`),
  KEY `user_device_id_idx` (`user_id`),
  CONSTRAINT `user_device_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `frequency`
--

DROP TABLE IF EXISTS `frequency`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `frequency` (
  `idfrequency` varchar(255) NOT NULL,
  `frequency_val` varchar(45) NOT NULL,
  PRIMARY KEY (`idfrequency`),
  UNIQUE KEY `idfrequency_UNIQUE` (`idfrequency`),
  UNIQUE KEY `idfrequency` (`idfrequency`),
  UNIQUE KEY `idfrequency_2` (`idfrequency`),
  UNIQUE KEY `idfrequency_3` (`idfrequency`),
  UNIQUE KEY `idfrequency_4` (`idfrequency`),
  UNIQUE KEY `idfrequency_5` (`idfrequency`),
  UNIQUE KEY `idfrequency_6` (`idfrequency`),
  UNIQUE KEY `idfrequency_7` (`idfrequency`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `friend_habit`
--

DROP TABLE IF EXISTS `friend_habit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `friend_habit` (
  `idfriend_habit` varchar(255) NOT NULL,
  `friendId` varchar(255) NOT NULL,
  `habitId` varchar(255) NOT NULL,
  `ownerId` varchar(255) NOT NULL,
  PRIMARY KEY (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_UNIQUE` (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit` (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_2` (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_3` (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_4` (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_5` (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_6` (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_7` (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_8` (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_9` (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_10` (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_11` (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_12` (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_13` (`idfriend_habit`),
  KEY `friendId_idx` (`friendId`),
  KEY `owner_id_idx` (`ownerId`),
  KEY `friend_habit_id_idx` (`habitId`),
  CONSTRAINT `friend_habit_id` FOREIGN KEY (`habitId`) REFERENCES `habit` (`idhabit`) ON DELETE CASCADE,
  CONSTRAINT `friend_id` FOREIGN KEY (`friendId`) REFERENCES `user` (`iduser`),
  CONSTRAINT `owner_id` FOREIGN KEY (`ownerId`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `habit`
--

DROP TABLE IF EXISTS `habit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `habit` (
  `idhabit` varchar(255) NOT NULL,
  `name` varchar(45) NOT NULL,
  `icon` blob,
  `reminder` tinyint NOT NULL,
  `reminderAt` datetime NOT NULL,
  `visibleToFriends` tinyint NOT NULL,
  `description` varchar(4000) DEFAULT NULL,
  `status` char(1) NOT NULL,
  `userId` varchar(255) NOT NULL,
  `scheduleId` varchar(255) NOT NULL,
  `habitTypeId` int NOT NULL,
  `createdAt` datetime NOT NULL,
  `active_ind` char(1) NOT NULL,
  PRIMARY KEY (`idhabit`),
  UNIQUE KEY `idhabit_UNIQUE` (`idhabit`),
  UNIQUE KEY `idhabit` (`idhabit`),
  UNIQUE KEY `idhabit_2` (`idhabit`),
  UNIQUE KEY `idhabit_3` (`idhabit`),
  UNIQUE KEY `idhabit_4` (`idhabit`),
  UNIQUE KEY `idhabit_5` (`idhabit`),
  UNIQUE KEY `idhabit_6` (`idhabit`),
  UNIQUE KEY `idhabit_7` (`idhabit`),
  UNIQUE KEY `idhabit_8` (`idhabit`),
  UNIQUE KEY `idhabit_9` (`idhabit`),
  UNIQUE KEY `idhabit_10` (`idhabit`),
  KEY `habitTypeId_idx` (`habitTypeId`),
  KEY `habit_schedule_idx` (`scheduleId`),
  KEY `userId_idx` (`userId`),
  CONSTRAINT `habitUserId` FOREIGN KEY (`userId`) REFERENCES `user` (`iduser`) ON DELETE CASCADE,
  CONSTRAINT `schedule_habit_id` FOREIGN KEY (`scheduleId`) REFERENCES `schedule` (`idschedule`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `habit_frequency`
--

DROP TABLE IF EXISTS `habit_frequency`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `habit_frequency` (
  `idhabit_frequency` varchar(255) NOT NULL,
  `freq_habit_id` varchar(255) NOT NULL,
  `frequency_id` varchar(255) NOT NULL,
  PRIMARY KEY (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency_UNIQUE` (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency` (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency_2` (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency_3` (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency_4` (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency_5` (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency_6` (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency_7` (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency_8` (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency_9` (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency_10` (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency_11` (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency_12` (`idhabit_frequency`),
  KEY `freq_habit_id_idx` (`freq_habit_id`),
  CONSTRAINT `freq_habit_id` FOREIGN KEY (`freq_habit_id`) REFERENCES `habit` (`idhabit`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `habit_log`
--

DROP TABLE IF EXISTS `habit_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `habit_log` (
  `idhabit_log` varchar(255) NOT NULL,
  `log_value` char(1) NOT NULL,
  `user_id` varchar(255) NOT NULL,
  `habit_id` varchar(255) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`idhabit_log`),
  UNIQUE KEY `idhabit_history_UNIQUE` (`idhabit_log`),
  UNIQUE KEY `idhabit_log` (`idhabit_log`),
  UNIQUE KEY `idhabit_log_2` (`idhabit_log`),
  UNIQUE KEY `idhabit_log_3` (`idhabit_log`),
  UNIQUE KEY `idhabit_log_4` (`idhabit_log`),
  UNIQUE KEY `idhabit_log_5` (`idhabit_log`),
  UNIQUE KEY `idhabit_log_6` (`idhabit_log`),
  UNIQUE KEY `idhabit_log_7` (`idhabit_log`),
  UNIQUE KEY `idhabit_log_8` (`idhabit_log`),
  UNIQUE KEY `idhabit_log_9` (`idhabit_log`),
  UNIQUE KEY `idhabit_log_10` (`idhabit_log`),
  UNIQUE KEY `idhabit_log_11` (`idhabit_log`),
  UNIQUE KEY `idhabit_log_12` (`idhabit_log`),
  KEY `history_user_id_idx` (`user_id`),
  KEY `history_habit_id_idx` (`habit_id`),
  CONSTRAINT `history_habit_id` FOREIGN KEY (`habit_id`) REFERENCES `habit` (`idhabit`) ON DELETE CASCADE,
  CONSTRAINT `history_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `habit_type`
--

DROP TABLE IF EXISTS `habit_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `habit_type` (
  `habitTypeId` int NOT NULL AUTO_INCREMENT,
  `habitTypeValue` varchar(45) NOT NULL,
  `description` varchar(2000) NOT NULL,
  PRIMARY KEY (`habitTypeId`),
  UNIQUE KEY `idnew_table_UNIQUE` (`habitTypeId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `historyhabit`
--

DROP TABLE IF EXISTS `historyhabit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `historyhabit` (
  `idhabit` varchar(255) NOT NULL,
  `name` varchar(45) NOT NULL,
  `icon` blob,
  `reminder` tinyint DEFAULT NULL,
  `reminderAt` datetime DEFAULT NULL,
  `visibleToFriends` tinyint DEFAULT NULL,
  `description` varchar(4000) DEFAULT NULL,
  `status` char(1) NOT NULL,
  `userId` varchar(255) NOT NULL,
  `scheduleId` varchar(255) NOT NULL,
  `habitTypeid` varchar(255) NOT NULL,
  `createdAt` datetime NOT NULL,
  `active_ind` char(1) NOT NULL,
  PRIMARY KEY (`idhabit`),
  UNIQUE KEY `idhabit_UNIQUE` (`idhabit`),
  UNIQUE KEY `idhabit` (`idhabit`),
  UNIQUE KEY `userId` (`userId`),
  UNIQUE KEY `scheduleId` (`scheduleId`),
  UNIQUE KEY `habitTypeid` (`habitTypeid`),
  UNIQUE KEY `idhabit_2` (`idhabit`),
  UNIQUE KEY `idhabit_3` (`idhabit`),
  UNIQUE KEY `idhabit_4` (`idhabit`),
  UNIQUE KEY `idhabit_5` (`idhabit`),
  UNIQUE KEY `idhabit_6` (`idhabit`),
  UNIQUE KEY `idhabit_7` (`idhabit`),
  UNIQUE KEY `idhabit_8` (`idhabit`),
  UNIQUE KEY `idhabit_9` (`idhabit`),
  UNIQUE KEY `idhabit_10` (`idhabit`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `like`
--

DROP TABLE IF EXISTS `like`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `like` (
  `idlike` varchar(255) NOT NULL,
  `user_id` varchar(255) NOT NULL,
  `comment_id` varchar(255) NOT NULL,
  `milestone_id` varchar(255) NOT NULL,
  `created_at` datetime NOT NULL,
  PRIMARY KEY (`idlike`),
  UNIQUE KEY `idlike_UNIQUE` (`idlike`),
  UNIQUE KEY `idlike` (`idlike`),
  UNIQUE KEY `idlike_2` (`idlike`),
  UNIQUE KEY `idlike_3` (`idlike`),
  UNIQUE KEY `idlike_4` (`idlike`),
  UNIQUE KEY `idlike_5` (`idlike`),
  UNIQUE KEY `idlike_6` (`idlike`),
  UNIQUE KEY `idlike_7` (`idlike`),
  KEY `like_comment_id_idx` (`comment_id`),
  KEY `like_milestone_id_idx` (`milestone_id`),
  KEY `like_user_id_idx` (`user_id`),
  CONSTRAINT `like_comment_id` FOREIGN KEY (`comment_id`) REFERENCES `comment` (`idcomment`),
  CONSTRAINT `like_milestone_id` FOREIGN KEY (`milestone_id`) REFERENCES `milestone` (`idmilestone`) ON DELETE CASCADE,
  CONSTRAINT `like_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `milestone`
--

DROP TABLE IF EXISTS `milestone`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `milestone` (
  `idmilestone` varchar(255) NOT NULL,
  `milestone_type_id` int NOT NULL,
  `user_id` varchar(255) NOT NULL,
  `habit_id` varchar(255) NOT NULL,
  `created_at` datetime DEFAULT NULL,
  `value` varchar(45) NOT NULL,
  PRIMARY KEY (`idmilestone`),
  UNIQUE KEY `idmilestone_UNIQUE` (`idmilestone`),
  UNIQUE KEY `idmilestone` (`idmilestone`),
  UNIQUE KEY `idmilestone_2` (`idmilestone`),
  UNIQUE KEY `idmilestone_3` (`idmilestone`),
  UNIQUE KEY `idmilestone_4` (`idmilestone`),
  UNIQUE KEY `idmilestone_5` (`idmilestone`),
  UNIQUE KEY `idmilestone_6` (`idmilestone`),
  UNIQUE KEY `idmilestone_7` (`idmilestone`),
  UNIQUE KEY `idmilestone_8` (`idmilestone`),
  UNIQUE KEY `idmilestone_9` (`idmilestone`),
  UNIQUE KEY `idmilestone_10` (`idmilestone`),
  UNIQUE KEY `idmilestone_11` (`idmilestone`),
  UNIQUE KEY `idmilestone_12` (`idmilestone`),
  UNIQUE KEY `idmilestone_13` (`idmilestone`),
  UNIQUE KEY `idmilestone_14` (`idmilestone`),
  UNIQUE KEY `idmilestone_15` (`idmilestone`),
  KEY `milestone_type_id_idx` (`milestone_type_id`),
  KEY `milestone_user_id_idx` (`user_id`),
  KEY `milestone_habit_id_idx` (`habit_id`),
  CONSTRAINT `milestone_habit_id` FOREIGN KEY (`habit_id`) REFERENCES `habit` (`idhabit`),
  CONSTRAINT `milestone_type_id` FOREIGN KEY (`milestone_type_id`) REFERENCES `milestone_type` (`idmilestone_type`),
  CONSTRAINT `milestone_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `milestone_type`
--

DROP TABLE IF EXISTS `milestone_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `milestone_type` (
  `idmilestone_type` int NOT NULL,
  `name` varchar(45) NOT NULL,
  `description` varchar(2000) DEFAULT NULL,
  PRIMARY KEY (`idmilestone_type`),
  UNIQUE KEY `idmilestone_type_UNIQUE` (`idmilestone_type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `refresh_token`
--

DROP TABLE IF EXISTS `refresh_token`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `refresh_token` (
  `token` varchar(255) NOT NULL,
  `expires` datetime NOT NULL,
  `isExpired` tinyint NOT NULL,
  `created` datetime NOT NULL,
  `revoked` datetime DEFAULT NULL,
  `userId` varchar(255) NOT NULL,
  `is_revoked` tinyint NOT NULL,
  `deviceId` varchar(255) NOT NULL,
  PRIMARY KEY (`token`),
  KEY `user_device_id_idx` (`deviceId`),
  KEY `userId_idx` (`userId`),
  CONSTRAINT `token_device_id` FOREIGN KEY (`deviceId`) REFERENCES `device` (`iddevice`) ON DELETE CASCADE,
  CONSTRAINT `userId` FOREIGN KEY (`userId`) REFERENCES `user` (`iduser`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `schedule`
--

DROP TABLE IF EXISTS `schedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schedule` (
  `idschedule` varchar(255) NOT NULL,
  `schedule_type_id` int NOT NULL,
  `user_id` varchar(255) NOT NULL,
  `schedule_time` datetime DEFAULT NULL,
  PRIMARY KEY (`idschedule`),
  UNIQUE KEY `idschedule_UNIQUE` (`idschedule`),
  KEY `schedule_type_id_idx` (`schedule_type_id`),
  KEY `schedule_user_id_idx` (`user_id`),
  CONSTRAINT `schedule_type_id` FOREIGN KEY (`schedule_type_id`) REFERENCES `schedule_type` (`idschedule_type`) ON DELETE CASCADE,
  CONSTRAINT `schedule_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `schedule_type`
--

DROP TABLE IF EXISTS `schedule_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schedule_type` (
  `idschedule_type` int NOT NULL AUTO_INCREMENT,
  `schedule_value` varchar(255) NOT NULL,
  PRIMARY KEY (`idschedule_type`),
  UNIQUE KEY `idschedule_type_UNIQUE` (`idschedule_type`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `subscription`
--

DROP TABLE IF EXISTS `subscription`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subscription` (
  `idsubscription` varchar(255) NOT NULL,
  `subscription_type_id` int NOT NULL,
  `user_id` varchar(255) NOT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`idsubscription`),
  UNIQUE KEY `idsubscription_history_UNIQUE` (`idsubscription`),
  KEY `sub_type_id_idx` (`subscription_type_id`),
  KEY `sub_user_id_idx` (`user_id`),
  CONSTRAINT `sub_type_id` FOREIGN KEY (`subscription_type_id`) REFERENCES `subscription_type` (`idsubscription_type`),
  CONSTRAINT `sub_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `subscription_type`
--

DROP TABLE IF EXISTS `subscription_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subscription_type` (
  `idsubscription_type` int NOT NULL,
  `subscription_type` varchar(45) NOT NULL,
  PRIMARY KEY (`idsubscription_type`),
  UNIQUE KEY `idsubscription_type_UNIQUE` (`idsubscription_type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `iduser` varchar(255) NOT NULL,
  `firstname` varchar(100) DEFAULT NULL,
  `lastname` varchar(100) DEFAULT NULL,
  `email` varchar(256) NOT NULL,
  `passwordSalt` longtext NOT NULL,
  `lastLogin` datetime DEFAULT NULL,
  `locked` tinyint DEFAULT NULL,
  `dateLocked` datetime DEFAULT NULL,
  `token` longtext,
  `password` longtext NOT NULL,
  PRIMARY KEY (`iduser`),
  UNIQUE KEY `iduser_UNIQUE` (`iduser`),
  UNIQUE KEY `iduser` (`iduser`),
  UNIQUE KEY `iduser_2` (`iduser`),
  UNIQUE KEY `iduser_3` (`iduser`),
  UNIQUE KEY `iduser_4` (`iduser`),
  UNIQUE KEY `iduser_5` (`iduser`),
  UNIQUE KEY `iduser_6` (`iduser`),
  UNIQUE KEY `iduser_7` (`iduser`),
  UNIQUE KEY `iduser_8` (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user_friends`
--

DROP TABLE IF EXISTS `user_friends`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_friends` (
  `iduser_friends` varchar(255) NOT NULL,
  `user_id` varchar(255) NOT NULL,
  `friend_id` varchar(255) NOT NULL,
  `created_at` datetime NOT NULL,
  PRIMARY KEY (`iduser_friends`),
  UNIQUE KEY `iduser_friends_UNIQUE` (`iduser_friends`),
  UNIQUE KEY `iduser_friends` (`iduser_friends`),
  UNIQUE KEY `iduser_friends_2` (`iduser_friends`),
  UNIQUE KEY `iduser_friends_3` (`iduser_friends`),
  UNIQUE KEY `iduser_friends_4` (`iduser_friends`),
  UNIQUE KEY `iduser_friends_5` (`iduser_friends`),
  UNIQUE KEY `iduser_friends_6` (`iduser_friends`),
  UNIQUE KEY `iduser_friends_7` (`iduser_friends`),
  UNIQUE KEY `iduser_friends_8` (`iduser_friends`),
  UNIQUE KEY `iduser_friends_9` (`iduser_friends`),
  KEY `friend_user_id_idx` (`user_id`),
  KEY `user_friend_id_idx` (`friend_id`),
  CONSTRAINT `friend_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`) ON DELETE CASCADE,
  CONSTRAINT `user_friend_id` FOREIGN KEY (`friend_id`) REFERENCES `user` (`iduser`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping routines for database 'ceddb'
--
/*!50003 DROP PROCEDURE IF EXISTS `ActivateDevice` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `ActivateDevice`(
	IN
    DeviceUUID VARCHAR(100)
)
BEGIN
	SET @id = (SELECT de.iddevice FROM `ceddb`.`device` de
				WHERE de.uuid = DeviceUUID);
                
	UPDATE `ceddb`.`device` SET `active` = true WHERE @id = `ceddb`.`device`.`iddevice`;  
    
    SELECT * FROM `ceddb`.`device` de
    WHERE de.uuid = DeviceUUID;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AddFriendToUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `AddFriendToUser`(
	IN 
		UserId VARCHAR(255),
		FriendId VARCHAR(255),
        CreatedAt DATETIME
)
BEGIN
	SET @id = UUID();
	
    INSERT INTO `ceddb`.`user_friends` (`iduser_friends`, `user_id`, `friend_id`, `created_at`)
	VALUES(@id, UserId, FriendId, CreatedAt);
    
    SET @id2 = UUID();
    
    INSERT INTO `ceddb`.`user_friends` (`iduser_friends`, `user_id`, `friend_id`, `created_at`)
	VALUES(@id2, FriendId, UserId, CreatedAt);
    
    SELECT
		uf.iduser_friends AS 'id',
		u.iduser AS 'userId',
        u.firstname AS 'firstName',
        u.lastname AS 'lastName',
        u.email AS 'email'
    FROM user_friends uf
    JOIN user u ON uf.friend_id=u.iduser
    WHERE uf.iduser_friends = @id;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AddHabitLog` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `AddHabitLog`(
	IN
    HabitId VARCHAR(255),
    UserId VARCHAR(255),
    `Value` CHAR(1),
    CreatedAt DATETIME
)
BEGIN
	SET @id = UUID();
    
    INSERT INTO `ceddb`.`habit_log`
		(`idhabit_log`,
        `log_value`,
		`user_id`,
		`habit_id`,
        `created_at`)
	VALUES
		(@id,
        `Value`,
		UserId,
		HabitId,
        CreatedAt);
        
	SELECT 
		hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
    FROM `ceddb`.`habit_log` hl WHERE hl.`idhabit_log`=@id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ClearFrequenciesForHabit` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `ClearFrequenciesForHabit`(
	IN
    HabitId VARCHAR(255)
)
BEGIN
	DELETE from habit_frequency hf WHERE hf.freq_habit_id = HabitId;
    
     SELECT
		f.idfrequency,
        f.frequency_val AS "frequency"
	FROM `ceddb`.`frequency` f
    JOIN habit_frequency h ON f.idfrequency = h.frequency_id
    WHERE h.freq_habit_id=HabitId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ClearFriendHabit` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `ClearFriendHabit`(
	IN
    FriendId VARCHAR(255),
    HabitId VARCHAR(255),
    OwnerId VARCHAR(255)
)
BEGIN
	DELETE from friend_habit fh WHERE
    fh.friendId = FriendId AND 
    fh.habitId = HabitId AND 
    fh.ownerId = OwnerId;
    
    SELECT * FROM `ceddb`.`friend_habit` fh WHERE
    fh.friendId = FriendId AND 
    fh.habitId = HabitId AND 
    fh.ownerId = OwnerId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CreateHabit` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `CreateHabit`(
	IN
    `Name` VARCHAR(145),
    Icon BLOB,
    Reminder TINYINT,
    ReminderAt DateTime,
    VisibleToFriends TINYINT,
    Description VARCHAR(100),
    UserId VARCHAR(255),
    ScheduleId VARCHAR(255),
    HabitTypeId INT,
    CreatedAt DateTime,
    ActiveInd char(1)
)
BEGIN
	SET @id = UUID();

	INSERT INTO `ceddb`.`habit` (`idhabit`, `name`, `icon`, `reminder`, `reminderAt`, `visibleToFriends`, `description`, `status`, `userId`, `scheduleId`, `habitTypeId`, `createdAt`, `active_ind`)
	VALUES(@id, Name, Icon, Reminder, ReminderAt, VisibleToFriends, Description, 'P', UserId, ScheduleId, HabitTypeId, CreatedAt, ActiveInd);
    
    SELECT
		h.idhabit,
		h.name,
		h.icon,
		h.reminder,
		h.reminderAt,
		h.visibleToFriends,
		h.description,
		h.status,
		h.userId,
		h.createdAt,
		h.active_ind,
		s.idSchedule,
		s.schedule_time,
		st.idschedule_type,
		st.schedule_value as "scheduleType",
		ht.habitTypeId,
		ht.habitTypeValue as "habitType",
		ht.description as "habitTypeDescription"
    FROM `ceddb`.`habit` h
    JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId
    WHERE h.idhabit = @id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CreateNewDevice` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `CreateNewDevice`(
	IN
    DeviceGUID VARCHAR(100),
    DeviceModel VARCHAR(100),
    DevicePlatform VARCHAR(100),
    Manufacturer VARCHAR(100),
    UserId INT
)
BEGIN
	INSERT INTO `ceddb`.`device`
	(`model`,`platform`,`uuid`,`manufacturer`, `user_id`)
		VALUES(DeviceModel, DevicePlatform, DeviceGUID, Manufacturer, UserId);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CreateUserDevice` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `CreateUserDevice`(
	IN
    DeviceGUID VARCHAR(100),
    DeviceModel VARCHAR(100),
    DevicePlatform VARCHAR(100),
    Manufacturer VARCHAR(100),
    UserId VARCHAR(255)
)
BEGIN
	SET @id = UUID();
	INSERT INTO `ceddb`.`device`
	(`iddevice`, `model`,`platform`,`uuid`,`manufacturer`, `user_id`, `active`)
		VALUES(@id, DeviceModel, DevicePlatform, DeviceGUID, Manufacturer, UserId, true);
        
	SELECT * FROM `ceddb`.`device` d WHERE d.`iddevice`=@id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeactiveateDevice` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `DeactiveateDevice`(
	IN
    DeviceUUID VARCHAR(100)
)
BEGIN
	SET @id = (SELECT de.iddevice FROM `ceddb`.`device` de
				WHERE de.uuid = DeviceUUID);
                
	UPDATE `ceddb`.`device` SET `active` = false WHERE @id = `ceddb`.`device`.`iddevice`;  
    
    SELECT * FROM `ceddb`.`device` de
    WHERE de.uuid = DeviceUUID;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteRefreshToken` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `DeleteRefreshToken`(
	IN Token VARCHAR(255)
)
BEGIN
  
  DELETE FROM `CEDDB`.`refresh_token` re
  WHERE re.`token` = Token;
  
  SELECT * FROM `CEDDB`.`refresh_token` re
  WHERE re.`token` = Token;
  
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllHabits` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetAllHabits`()
BEGIN
    SELECT 
		h.idhabit,
		h.name,
		h.icon,
		h.reminder,
		h.reminderAt,
		h.visibleToFriends,
		h.description,
		h.status,
		h.userId,
		h.createdAt,
		h.active_ind,
		s.idSchedule,
		s.schedule_time,
		st.idschedule_type,
		st.schedule_value as "scheduleType",
		ht.habitTypeId,
		ht.habitTypeValue as "habitType",
		ht.description as "habitTypeDescription"
    FROM habit h 
	JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllHabitTypes` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetAllHabitTypes`()
BEGIN
    SELECT 
		habitTypeId,
        habitTypeValue as "value",
        description
	FROM `CEDDB`.`habit_type`;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllLogsForHabit` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetAllLogsForHabit`(
	IN
    HabitId VARCHAR(255)
)
BEGIN
    SELECT hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
	FROM `ceddb`.`habit_log` hl
    WHERE hl.`habit_id` = HabitId
    ORDER BY hl.created_at ASC;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllScheduleTypes` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetAllScheduleTypes`()
BEGIN
    SELECT 
		idschedule_type,
        schedule_value as "value"
	FROM `CEDDB`.`schedule_type`;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllUserHabits` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetAllUserHabits`(
	IN
    UserId VARCHAR(255)
)
BEGIN
    SELECT 
		h.idhabit,
		h.name,
		h.icon,
		h.reminder,
		h.reminderAt,
		h.visibleToFriends,
		h.description,
		h.status,
		h.userId,
		h.createdAt,
		h.active_ind,
		s.idSchedule,
		s.schedule_time,
		st.idschedule_type,
		st.schedule_value as "scheduleType",
		ht.habitTypeId,
		ht.habitTypeValue as "habitType",
		ht.description as "habitTypeDescription"
    FROM habit h 
	JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId
    WHERE h.userId = UserId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAvgSuccessLogsForUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetAvgSuccessLogsForUser`(
	IN
    UserId VARCHAR(255)
)
BEGIN
	SELECT (
		(SELECT COUNT(*) FROM HABIT_LOG WHERE user_id = UserId and log_value = 'C') /
		(SELECT COUNT(*) FROM HABIT_LOG WHERE user_id = UserId) * 100) 
	AS 'COMPLETED_PERCENTAGE';
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetCompletedLogsForHabit` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetCompletedLogsForHabit`(
	IN
    HabitId VARCHAR(255)
)
BEGIN
	select * 
    from habit_log hl 
    where hl.habit_id = HabitId AND hl.log_value = "C"
    ORDER BY hl.created_at ASC, hl.habit_id ASC; 
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetCompletedLogsForUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetCompletedLogsForUser`(
	IN
    UserId VARCHAR(255)
)
BEGIN
	select * 
    from habit_log hl 
    where hl.user_id= UserId AND hl.log_value = "C"
    ORDER BY hl.created_at ASC, hl.habit_id ASC; 
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetDeviceByUUID` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetDeviceByUUID`(
	IN
    DeviceUUID VARCHAR(100)
)
BEGIN
	SELECT * FROM `ceddb`.`device` d
    WHERE d.uuid = DeviceUUID;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetDeviceIdByUUID` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetDeviceIdByUUID`(
	IN
    DeviceUUID VARCHAR(100)
)
BEGIN
    SELECT de.iddevice FROM `ceddb`.`device` de
    WHERE de.uuid = DeviceUUID;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetFriendHabits` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetFriendHabits`(
	IN
    UserId INT
)
BEGIN
	SELECT * FROM `ceddb`.`friend_habit` fh
    WHERE fh.friendId = UserId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetHabitById` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetHabitById`(
	IN
    HabitId VARCHAR(255)
)
BEGIN
    SELECT 
		h.idhabit,
		h.name,
		h.icon,
		h.reminder,
		h.reminderAt,
		h.visibleToFriends,
		h.description,
		h.status,
		h.userId,
		h.createdAt,
		h.active_ind,
		s.idSchedule,
		s.schedule_time,
		st.idschedule_type,
		st.schedule_value as "scheduleType",
		ht.habitTypeId,
		ht.habitTypeValue as "habitType",
		ht.description as "habitTypeDescription"
    FROM habit h 
	JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId
	WHERE h.idhabit = HabitId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetHabitFrequencies` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetHabitFrequencies`(
	IN
    HabitId VARCHAR(255)
)
BEGIN
    SELECT
		idfrequency,
		frequency_val as "frequency"
	FROM `ceddb`.`frequency` f
	INNER JOIN habit_frequency hf ON hf.frequency_id = f.idfrequency
    WHERE hf.freq_habit_id=HabitId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetHabitFriends` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetHabitFriends`(
	IN
    HabitId VARCHAR(255)
)
BEGIN
    SELECT 
		fh.idfriend_habit as "id",
        u.iduser as "friendId",
        u.firstname as "FirstName",
        u.lastname as "LastName",
        u.email as "Email",
        fh.ownerId
    FROM `CEDDB`.`friend_habit` fh
	JOIN User u ON fh.friendId=u.iduser
	WHERE fh.habitId = HabitId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetHabitLogById` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetHabitLogById`(
	IN
    HabitId VARCHAR(255)
)
BEGIN
	SET @id = (SELECT hl.idhabit_log FROM `ceddb`.`habit_log` hl
		WHERE hl.habit_id = HabitId);

    SELECT hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
	FROM `ceddb`.`habit_log` hl
    WHERE hl.`idhabit_log` = @id;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetHabitLogByIdAndDate` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetHabitLogByIdAndDate`(
	IN
    HabitId VARCHAR(255),
    DateValue DATETIME
)
BEGIN
	
    SELECT hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
	FROM `ceddb`.`habit_log` hl
    WHERE Date(hl.`created_at`)=Date(DateValue) AND hl.`habit_id` = HabitId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetLogsForUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetLogsForUser`(
	IN
    UserId VARCHAR(255)
)
BEGIN
	SELECT * FROM `ceddb`.`habit_log` hl
    WHERE hl.user_id = UserId ORDER BY hl.created_at ASC;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetRefreshToken` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetRefreshToken`(
	IN Token VARCHAR(256)
)
BEGIN

  SELECT * FROM `CEDDB`.`refresh_token` rt
	WHERE rt.token = Token;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetScheduleByHabitId` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetScheduleByHabitId`(
	IN
    HabitId VARCHAR(255)
)
BEGIN
    SELECT 
		s.idschedule AS "scheduleId",
        s.user_id as "userId",
        s.schedule_time AS "scheduleTime",
        st.idschedule_type AS "idScheduleType",
        st.schedule_value AS "scheduleTypeValue"
    FROM `CEDDB`.`habit` h
	JOIN `CEDDB`.`Schedule` s ON h.scheduleId=s.idschedule
    JOIN `CEDDB`.`Schedule_type` st ON s.schedule_type_id=st.idschedule_type
	WHERE h.idhabit = HabitId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetUserByEmail` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetUserByEmail`(
	IN Email VARCHAR(256)
)
BEGIN
	SELECT u.`iduser`,
		u.`firstname`,
		u.`lastname`,
		u.`email`,
		u.`passwordSalt`,
		u.`lastLogin`,
		u.`locked`,
		u.`dateLocked`,
		u.`token`,
		u.`password`
	FROM `ceddb`.`user` u 
    WHERE u.email = Email;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetUserByRefreshToken` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetUserByRefreshToken`(
	IN Token VARCHAR(256)
)
BEGIN

  SELECT * FROM `CEDDB`.`user` u 
  WHERE u.iduser = (SELECT userId FROM `CEDDB`.`refresh_token` rt
	WHERE rt.token = Token);
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getUserDevices` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `getUserDevices`(
	IN
    UserId INT
)
BEGIN
	SELECT * FROM `ceddb`.`device` d
    WHERE d.user_id = UserId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetUserFriendHabitStats` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetUserFriendHabitStats`(
	IN
    UserId VARCHAR(255)
)
BEGIN
	SELECT (
		(SELECT COUNT(*) FROM `ceddb`.friend_habit WHERE user_id = UserId))
	AS 'FRIEND_STAT';
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetUserFriends` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetUserFriends`(
	IN 
	Id VARCHAR(255)
)
BEGIN
    SELECT
		uf.iduser_friends AS 'id',
		u.iduser AS 'userId',
        u.firstname AS 'firstName',
        u.lastname AS 'lastName',
        u.email AS 'email'
    FROM user_friends uf
    JOIN user u ON uf.friend_id=u.iduser
    WHERE uf.user_id = Id;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetUserRefreshTokenById` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetUserRefreshTokenById`(
	IN UserId VARCHAR(255)
)
BEGIN
	SELECT re.token, re.expires, re.created, re.isExpired, re.revoked, re.deviceId
	FROM `ceddb`.`refresh_token` re 
    WHERE re.userId = UserId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetUsersDevices` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `GetUsersDevices`(
	IN
    UserId VARCHAR(255)
)
BEGIN
	SELECT * FROM `ceddb`.`device` d
    WHERE d.user_id = UserId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `RegisterAccount` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `RegisterAccount`(
	IN
    Firstname VARCHAR(100),
    Lastname VARCHAR(100),
    Email VARCHAR(265),
    UserHash BLOB,
    Salt BLOB,
    DeviceGUID VARCHAR(100),
    DeviceModel VARCHAR(100),
    DevicePlatform VARCHAR(100),
    Manufacturer VARCHAR(100)
)
BEGIN
SET @id = UUID();

INSERT INTO `ceddb`.`user`
(`iduser`, `firstname`,`lastname`,`email`,`passwordSalt`, `locked`, `password`)
	VALUES(@id, Firstname, Lastname, Email, Salt, false, UserHash);
    
call CreateUserDevice(
	DeviceGUID,
    DeviceModel,
    DevicePlatform,
    Manufacturer,
    (SELECT `ceddb`.`user`.`iduser` FROM `ceddb`.`user` WHERE `ceddb`.`user`.`iduser`=@id));
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `RemoveFriendToUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `RemoveFriendToUser`(
	IN 
		UserId VARCHAR(255),
        FriendId VARCHAR(255)
)
BEGIN

	SET @id = (SELECT uf.iduser_friends FROM user_friends uf
    WHERE uf.friend_id = FriendId AND uf.user_id = UserId);
    
    SET @id2 = (SELECT uf.iduser_friends FROM user_friends uf
    WHERE uf.friend_id = UserId AND uf.user_id = FriendId);

	DELETE FROM user_friends f WHERE f.iduser_friends = @id;
    DELETE FROM user_friends f WHERE f.iduser_friends = @id2;
    
    SELECT * FROM user_friends uf
    WHERE uf.iduser_friends = UserId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SaveFriendHabit` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `SaveFriendHabit`(
	IN
    FriendId VARCHAR(255),
    HabitId VARCHAR(255),
    OwnerId VARCHAR(255)
)
BEGIN
	set @id = UUID();
    INSERT INTO `ceddb`.`friend_habit` (`idfriend_habit`, `friendId`, `habitId`, `ownerId`)
	VALUES(@id, FriendId, HabitId, OwnerId);
    
    SELECT
		fh.idfriend_habit AS "id",
        fh.friendId AS "friendId",
        u.firstname AS "FirstName",
        u.lastname AS "LastName",
        u.email AS "Email",
        fh.ownerId AS "ownerId"
	FROM `ceddb`.`friend_habit` fh
    JOIN `ceddb`.`user` u ON fh.friendId = u.iduser
    WHERE f.idfrequency = FrequencyId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SaveHabitFrequency` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `SaveHabitFrequency`(
	IN
    HabitId VARCHAR(255),
    FrequencyId INT
)
BEGIN
	set @id = UUID();
    INSERT INTO `ceddb`.`habit_frequency` (`idhabit_frequency`, `freq_habit_id`, `frequency_id`)
	VALUES(@id, HabitId, FrequencyId);
    
    SELECT
		f.idfrequency,
        f.frequency_val AS "frequency"
	FROM `ceddb`.`frequency` f
    WHERE f.idfrequency = FrequencyId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SaveRefreshToken` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `SaveRefreshToken`(
	IN UserId VARCHAR(255), Token VARCHAR(255), IsExpired Boolean, Expires DATETIME, Created DATETIME, Revoked DATETIME, IsRevoked Boolean, DeviceId VARCHAR(255)
)
BEGIN
  INSERT INTO refresh_token(`token`, `expires`, `isExpired`, `created`, `revoked`, `is_revoked`, `userId`, `deviceId`)
  VALUES(
	Token, Expires, IsExpired, Created, Revoked, IsRevoked, UserId, DeviceId
  );
  
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SaveSchedule` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `SaveSchedule`(
	IN
    ScheduleTypeId INT,
    UserId VARCHAR(255),
    ScheduleTime DATETIME
)
BEGIN
	SET @id = UUID();
    INSERT INTO `ceddb`.`schedule` (`idschedule`, `schedule_type_id`, `user_id`, `schedule_time`)
	VALUES(@id, ScheduleTypeId, UserId, ScheduleTime);
    
    SELECT
    s.idschedule as "Id",
    s.schedule_time as "ScheduleTime",
    st.idschedule_type as "idschedule_type",
    st.schedule_value as "scheduleType",
    s.user_id as "UserId"
    FROM `ceddb`.`schedule` s 
    INNER JOIN schedule_type st ON st.idschedule_type = schedule_type_id
    WHERE s.`idschedule`=@id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SearchForUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `SearchForUser`(
	IN Param VARCHAR(256)
)
BEGIN

  SELECT
	u.iduser,
    u.firstname,
    u.lastname,
    u.email
  FROM user u 
  WHERE u.firstname LIKE CONCAT('%', Param, '%') OR u.lastname LIKE CONCAT('%', Param, '%') OR u.email LIKE CONCAT('%', Param, '%');
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateHabit` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `UpdateHabit`(
	IN
    `Name` VARCHAR(145),
    Icon BLOB,
    Reminder TINYINT,
    ReminderAt DateTime,
    VisibleToFriends TINYINT,
    Description VARCHAR(100),
    UserId VARCHAR(255),
    ScheduleId VARCHAR(255),
    HabitTypeId INT,
    CreatedAt DateTime,
    ActiveInd char(1),
    HabitId VARCHAR(255)
)
BEGIN
    UPDATE `ceddb`.`habit` h SET
		`name`= Name,
        icon = Icon,
        reminder = Reminder,
        reminderAt = ReminderAt,
        visibleToFriends = VisibleToFriends,
        description = Description,
        userid = UserId,
        scheduleId = ScheduleId,
        habitTypeId = HabitTypeId,
        createdAt = CreatedAt,
        active_ind = ActiveInd
	WHERE h.idhabit = HabitId;
    
    SELECT
		h.idhabit,
		h.name,
		h.icon,
		h.reminder,
		h.reminderAt,
		h.visibleToFriends,
		h.description,
		h.status,
		h.userId,
		h.createdAt,
		h.active_ind,
		s.idSchedule,
		s.schedule_time,
		st.idschedule_type,
		st.schedule_value as "scheduleType",
		ht.habitTypeId,
		ht.habitTypeValue as "habitType",
		ht.description as "habitTypeDescription"
    FROM `ceddb`.`habit` h
    JOIN Schedule s ON h.scheduleId=s.idschedule
	JOIN schedule_type st ON s.schedule_type_id = st.idschedule_type
	JOIN habit_type ht ON h.habitTypeId = ht.habitTypeId
    WHERE h.idhabit = HabitId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateHabitLog` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `UpdateHabitLog`(
	IN
    `Value` CHAR(1),
    HabitId VARCHAR(255),
    DateValue DATETIME
)
BEGIN
	SET @id = (SELECT hl.idhabit_log FROM `ceddb`.`habit_log` hl
		WHERE Date(hl.`created_at`)=Date(DateValue) AND hl.habit_id=HabitId);

	UPDATE `ceddb`.`habit_log` SET
		`log_value` = `Value`
		WHERE `idhabit_log` = @id;
        
	select 
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
    from `ceddb`.`habit_log` hl WHERE Date(hl.`created_at`)=Date(DateValue) AND hl.habit_id=HabitId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateSchedule` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`dtatkison`@`%` PROCEDURE `UpdateSchedule`(
	IN
    Id VARCHAR(255),
    ScheduleTypeId INT,
    UserId VARCHAR(255),
    ScheduleTime DATETIME
)
BEGIN
    UPDATE `ceddb`.`schedule` s SET
		`schedule_type_id`= ScheduleTypeId,
        user_id = UserId,
        schedule_time = ScheduleTime
	WHERE s.idschedule = Id;
    
    SELECT
		s.idschedule as "Id",
		s.schedule_time as "ScheduleTime",
		st.idschedule_type as "idschedule_type",
		st.schedule_value as "scheduleType",
		s.user_id as "UserId"
    FROM `ceddb`.`schedule` s 
    INNER JOIN schedule_type st ON st.idschedule_type = schedule_type_id
    WHERE s.`idschedule`=Id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-04-04 18:48:06
