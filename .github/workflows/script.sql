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
  `idcomment` int NOT NULL,
  `comment_value` varchar(360) NOT NULL,
  `milestone_id` int NOT NULL,
  `user_id` int NOT NULL,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT NULL,
  PRIMARY KEY (`idcomment`),
  UNIQUE KEY `idcomment_UNIQUE` (`idcomment`),
  KEY `milestone_comment_id_idx` (`milestone_id`),
  KEY `user_comment_id_idx` (`user_id`),
  CONSTRAINT `milestone_comment_id` FOREIGN KEY (`milestone_id`) REFERENCES `milestone` (`idmilestone`),
  CONSTRAINT `user_comment_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comment`
--

LOCK TABLES `comment` WRITE;
/*!40000 ALTER TABLE `comment` DISABLE KEYS */;
/*!40000 ALTER TABLE `comment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `device`
--

DROP TABLE IF EXISTS `device`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `device` (
  `iddevice` int NOT NULL AUTO_INCREMENT,
  `model` varchar(45) NOT NULL,
  `platform` varchar(45) NOT NULL,
  `uuid` varchar(45) NOT NULL,
  `manufacturer` varchar(45) NOT NULL,
  `user_id` int NOT NULL,
  `active` tinyint NOT NULL,
  PRIMARY KEY (`iddevice`),
  UNIQUE KEY `iddevice_UNIQUE` (`iddevice`),
  KEY `user_device_id_idx` (`user_id`),
  CONSTRAINT `user_device_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `device`
--

LOCK TABLES `device` WRITE;
/*!40000 ALTER TABLE `device` DISABLE KEYS */;
INSERT INTO `device` VALUES (3,'string','string','string','string',16,0),(4,'asdf','asdf','1234','1203',17,1),(5,'Samsung','Android','123456','Samsung',18,1),(7,'Samsung','Android','123456','Samsung',20,1);
/*!40000 ALTER TABLE `device` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `frequency`
--

DROP TABLE IF EXISTS `frequency`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `frequency` (
  `idfrequency` int NOT NULL AUTO_INCREMENT,
  `frequency_val` varchar(45) NOT NULL,
  PRIMARY KEY (`idfrequency`),
  UNIQUE KEY `idfrequency_UNIQUE` (`idfrequency`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `frequency`
--

LOCK TABLES `frequency` WRITE;
/*!40000 ALTER TABLE `frequency` DISABLE KEYS */;
INSERT INTO `frequency` VALUES (1,'Everyday'),(2,'Monday'),(3,'Tuesday'),(4,'Wednesday'),(5,'Thursday'),(6,'Friday'),(7,'Sunday'),(8,'Saturday');
/*!40000 ALTER TABLE `frequency` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `friend_habit`
--

DROP TABLE IF EXISTS `friend_habit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `friend_habit` (
  `idfriend_habit` int NOT NULL AUTO_INCREMENT,
  `friendId` int NOT NULL,
  `habitId` int NOT NULL,
  `ownerId` int NOT NULL,
  PRIMARY KEY (`idfriend_habit`),
  UNIQUE KEY `idfriend_habit_UNIQUE` (`idfriend_habit`),
  KEY `friendId_idx` (`friendId`),
  KEY `friend_habit_id_idx` (`habitId`),
  KEY `owner_id_idx` (`ownerId`),
  CONSTRAINT `friend_habit_id` FOREIGN KEY (`habitId`) REFERENCES `habit` (`idhabit`),
  CONSTRAINT `friend_id` FOREIGN KEY (`friendId`) REFERENCES `user` (`iduser`),
  CONSTRAINT `owner_id` FOREIGN KEY (`ownerId`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `friend_habit`
--

LOCK TABLES `friend_habit` WRITE;
/*!40000 ALTER TABLE `friend_habit` DISABLE KEYS */;
INSERT INTO `friend_habit` VALUES (1,7,1,20);
/*!40000 ALTER TABLE `friend_habit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `habit`
--

DROP TABLE IF EXISTS `habit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `habit` (
  `idhabit` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `icon` blob,
  `reminder` tinyint NOT NULL,
  `reminderAt` datetime NOT NULL,
  `visibleToFriends` tinyint NOT NULL,
  `description` varchar(4000) DEFAULT NULL,
  `status` char(1) NOT NULL,
  `userId` int NOT NULL,
  `scheduleId` int NOT NULL,
  `habitTypeId` int NOT NULL,
  `createdAt` datetime NOT NULL,
  `active_ind` char(1) NOT NULL,
  PRIMARY KEY (`idhabit`),
  UNIQUE KEY `idhabit_UNIQUE` (`idhabit`),
  KEY `userId_idx` (`userId`),
  KEY `habitTypeId_idx` (`habitTypeId`),
  CONSTRAINT `habitUserId` FOREIGN KEY (`userId`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `habit`
--

LOCK TABLES `habit` WRITE;
/*!40000 ALTER TABLE `habit` DISABLE KEYS */;
INSERT INTO `habit` VALUES (1,'Wake Up earlyUPDATED!s',NULL,0,'2021-10-08 22:59:28',1,'this is a habit','P',20,2,1,'2021-10-08 22:59:28','A'),(2,'Wake Up early2',NULL,0,'2021-10-08 22:59:28',1,'this is a habit','P',20,2,1,'2021-10-08 22:59:28','A'),(3,'Wake Up early1',NULL,0,'2021-10-08 22:59:28',1,'this is a habit','P',20,2,1,'2021-10-08 22:59:28','A');
/*!40000 ALTER TABLE `habit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `habit_frequency`
--

DROP TABLE IF EXISTS `habit_frequency`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `habit_frequency` (
  `idhabit_frequency` int NOT NULL AUTO_INCREMENT,
  `freq_habit_id` int NOT NULL,
  `frequency_id` int NOT NULL,
  PRIMARY KEY (`idhabit_frequency`),
  UNIQUE KEY `idhabit_frequency_UNIQUE` (`idhabit_frequency`),
  KEY `freq_habit_id_idx` (`freq_habit_id`),
  KEY `freq_id_idx` (`frequency_id`),
  CONSTRAINT `freq_habit_id` FOREIGN KEY (`freq_habit_id`) REFERENCES `habit` (`idhabit`),
  CONSTRAINT `freq_id` FOREIGN KEY (`frequency_id`) REFERENCES `frequency` (`idfrequency`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `habit_frequency`
--

LOCK TABLES `habit_frequency` WRITE;
/*!40000 ALTER TABLE `habit_frequency` DISABLE KEYS */;
INSERT INTO `habit_frequency` VALUES (2,1,2),(3,1,4),(4,1,6);
/*!40000 ALTER TABLE `habit_frequency` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `habit_log`
--

DROP TABLE IF EXISTS `habit_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `habit_log` (
  `idhabit_log` int NOT NULL AUTO_INCREMENT,
  `log_value` char(1) NOT NULL,
  `user_id` int NOT NULL,
  `habit_id` int NOT NULL,
  `created_at` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`idhabit_log`),
  UNIQUE KEY `idhabit_history_UNIQUE` (`idhabit_log`),
  KEY `history_user_id_idx` (`user_id`),
  KEY `history_habit_id_idx` (`habit_id`),
  CONSTRAINT `history_habit_id` FOREIGN KEY (`habit_id`) REFERENCES `habit` (`idhabit`),
  CONSTRAINT `history_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `habit_log`
--

LOCK TABLES `habit_log` WRITE;
/*!40000 ALTER TABLE `habit_log` DISABLE KEYS */;
INSERT INTO `habit_log` VALUES (2,'C',20,1,'2021-10-11 22:59:28'),(3,'C',20,1,'2021-10-10 22:59:28'),(12,'C',20,1,'2021-10-12 23:13:44'),(17,'P',20,1,'2021-10-13 13:03:49'),(18,'F',20,2,'2021-10-13 21:22:16'),(19,'F',20,3,'2021-10-13 21:22:22');
/*!40000 ALTER TABLE `habit_log` ENABLE KEYS */;
UNLOCK TABLES;

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
-- Dumping data for table `habit_type`
--

LOCK TABLES `habit_type` WRITE;
/*!40000 ALTER TABLE `habit_type` DISABLE KEYS */;
INSERT INTO `habit_type` VALUES (1,'Regular','Habit that will need to be checked off'),(2,'Negative','Old habit that you are trying to break');
/*!40000 ALTER TABLE `habit_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `historyhabit`
--

DROP TABLE IF EXISTS `historyhabit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `historyhabit` (
  `idhabit` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `icon` blob,
  `reminder` tinyint DEFAULT NULL,
  `reminderAt` datetime DEFAULT NULL,
  `visibleToFriends` tinyint DEFAULT NULL,
  `description` varchar(4000) DEFAULT NULL,
  `status` char(1) NOT NULL,
  `userId` int NOT NULL,
  `scheduleId` int NOT NULL,
  `habitTypeid` int NOT NULL,
  `createdAt` datetime NOT NULL,
  `active_ind` char(1) NOT NULL,
  PRIMARY KEY (`idhabit`),
  UNIQUE KEY `idhabit_UNIQUE` (`idhabit`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `historyhabit`
--

LOCK TABLES `historyhabit` WRITE;
/*!40000 ALTER TABLE `historyhabit` DISABLE KEYS */;
/*!40000 ALTER TABLE `historyhabit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `like`
--

DROP TABLE IF EXISTS `like`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `like` (
  `idlike` int NOT NULL,
  `user_id` int NOT NULL,
  `comment_id` int DEFAULT NULL,
  `milestone_id` int DEFAULT NULL,
  `created_at` datetime NOT NULL,
  PRIMARY KEY (`idlike`),
  UNIQUE KEY `idlike_UNIQUE` (`idlike`),
  KEY `like_user_id_idx` (`user_id`),
  KEY `like_comment_id_idx` (`comment_id`),
  KEY `like_milestone_id_idx` (`milestone_id`),
  CONSTRAINT `like_comment_id` FOREIGN KEY (`comment_id`) REFERENCES `comment` (`idcomment`),
  CONSTRAINT `like_milestone_id` FOREIGN KEY (`milestone_id`) REFERENCES `milestone` (`idmilestone`),
  CONSTRAINT `like_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `like`
--

LOCK TABLES `like` WRITE;
/*!40000 ALTER TABLE `like` DISABLE KEYS */;
/*!40000 ALTER TABLE `like` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `milestone`
--

DROP TABLE IF EXISTS `milestone`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `milestone` (
  `idmilestone` int NOT NULL,
  `milestone_type_id` int NOT NULL,
  `user_id` int NOT NULL,
  `habit_id` int NOT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`idmilestone`),
  UNIQUE KEY `idmilestone_UNIQUE` (`idmilestone`),
  KEY `milestone_user_id_idx` (`user_id`),
  KEY `milestone_habit_id_idx` (`habit_id`),
  KEY `milestone_type_id_idx` (`milestone_type_id`),
  CONSTRAINT `milestone_habit_id` FOREIGN KEY (`habit_id`) REFERENCES `habit` (`idhabit`),
  CONSTRAINT `milestone_type_id` FOREIGN KEY (`milestone_type_id`) REFERENCES `milestone_type` (`idmilestone_type`),
  CONSTRAINT `milestone_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `milestone`
--

LOCK TABLES `milestone` WRITE;
/*!40000 ALTER TABLE `milestone` DISABLE KEYS */;
/*!40000 ALTER TABLE `milestone` ENABLE KEYS */;
UNLOCK TABLES;

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
-- Dumping data for table `milestone_type`
--

LOCK TABLES `milestone_type` WRITE;
/*!40000 ALTER TABLE `milestone_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `milestone_type` ENABLE KEYS */;
UNLOCK TABLES;

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
  `userId` int DEFAULT NULL,
  `is_revoked` tinyint NOT NULL,
  `deviceId` int DEFAULT NULL,
  PRIMARY KEY (`token`),
  KEY `userId_idx` (`userId`),
  KEY `user_device_id_idx` (`deviceId`),
  CONSTRAINT `token_device_id` FOREIGN KEY (`deviceId`) REFERENCES `device` (`iddevice`),
  CONSTRAINT `userId` FOREIGN KEY (`userId`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `refresh_token`
--

LOCK TABLES `refresh_token` WRITE;
/*!40000 ALTER TABLE `refresh_token` DISABLE KEYS */;
INSERT INTO `refresh_token` VALUES ('LNFAN2LXrPy0pqz6cYCkIl4vEndAZcvd/8VtR8wW08Q=','2021-10-17 22:06:18',0,'2021-10-17 20:06:18',NULL,20,0,7);
/*!40000 ALTER TABLE `refresh_token` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `schedule`
--

DROP TABLE IF EXISTS `schedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schedule` (
  `idschedule` int NOT NULL AUTO_INCREMENT,
  `schedule_type_id` int NOT NULL,
  `user_id` int NOT NULL,
  `schedule_time` datetime DEFAULT NULL,
  PRIMARY KEY (`idschedule`),
  UNIQUE KEY `idschedule_UNIQUE` (`idschedule`),
  KEY `schedule_user_id_idx` (`user_id`),
  KEY `schedule_type_id_idx` (`schedule_type_id`),
  CONSTRAINT `schedule_type_id` FOREIGN KEY (`schedule_type_id`) REFERENCES `schedule_type` (`idschedule_type`),
  CONSTRAINT `schedule_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schedule`
--

LOCK TABLES `schedule` WRITE;
/*!40000 ALTER TABLE `schedule` DISABLE KEYS */;
INSERT INTO `schedule` VALUES (2,1,20,'2021-10-08 22:59:28');
/*!40000 ALTER TABLE `schedule` ENABLE KEYS */;
UNLOCK TABLES;

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
-- Dumping data for table `schedule_type`
--

LOCK TABLES `schedule_type` WRITE;
/*!40000 ALTER TABLE `schedule_type` DISABLE KEYS */;
INSERT INTO `schedule_type` VALUES (1,'Morning'),(2,'Afternoon'),(3,'Evening'),(4,'Anytime');
/*!40000 ALTER TABLE `schedule_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subscription`
--

DROP TABLE IF EXISTS `subscription`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subscription` (
  `idsubscription` int NOT NULL,
  `subscription_type_id` int NOT NULL,
  `user_id` int NOT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`idsubscription`),
  UNIQUE KEY `idsubscription_history_UNIQUE` (`idsubscription`),
  KEY `sub_user_id_idx` (`user_id`),
  KEY `sub_type_id_idx` (`subscription_type_id`),
  CONSTRAINT `sub_type_id` FOREIGN KEY (`subscription_type_id`) REFERENCES `subscription_type` (`idsubscription_type`),
  CONSTRAINT `sub_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subscription`
--

LOCK TABLES `subscription` WRITE;
/*!40000 ALTER TABLE `subscription` DISABLE KEYS */;
/*!40000 ALTER TABLE `subscription` ENABLE KEYS */;
UNLOCK TABLES;

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
-- Dumping data for table `subscription_type`
--

LOCK TABLES `subscription_type` WRITE;
/*!40000 ALTER TABLE `subscription_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `subscription_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `iduser` int NOT NULL AUTO_INCREMENT,
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
  UNIQUE KEY `iduser_UNIQUE` (`iduser`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (7,'Drew','Atkison','dtatkison@gmail.com','GaXpbfkua3SfnRyvLupi',NULL,0,NULL,NULL,'DbNumyynMyZBBEmX0ndJgpTWXivx9cx0CvvOTrpccTg9pTLxTzYXDMCQyxgj/jNDtwn13SuGWTZTNISox1n2JA=='),(8,'Drew','Atkison','dtatkisonasdf@gmail.com','ymIoud28Lq3q8gPR5O3H',NULL,0,NULL,NULL,'7jcNCKpeMItHhQ3xVe4KMyvKifE0Wp63w8Mi+LSpmahynlT5VZUrgFRRR5Q0DLTpV8q3+h0EZRNNdnhiTeLUGg=='),(9,'Drew','Atkison','dt@gmail.com','asdfsd',NULL,0,NULL,NULL,'aasdf'),(10,'Drew','Atkison','dtasdf@gmail.com','asdfsd',NULL,0,NULL,NULL,'aasdf'),(11,'Drew','Atkison','dtasd2333f@gmail.com','asdfsd',NULL,0,NULL,NULL,'aasdf'),(12,'Drew','Atkison','dtasd2asd333f@gmail.com','asdfsd',NULL,0,NULL,NULL,'aasdf'),(13,'Drew','Atkison','dastasd2asd333f@gmail.com','asdfsd',NULL,0,NULL,NULL,'aasdf'),(14,'Drew','Atkison','dastasd2asd333f@gmail.com','asdfsd',NULL,0,NULL,NULL,'aasdf'),(15,'Drew','Atkison','da33f@gmail.com','asdfsd',NULL,0,NULL,NULL,'aasdf'),(16,'dREW','Atkison','user@example.com','6EPsscKNvzjrAHL1Jtg/',NULL,0,NULL,NULL,'LYE4W1Y1DlirqO4+2y9IPVoG8IAzbuyUIYGv4hgx9/coRrnNRLzaOqilVUORiDvHdoMIr889v7qPwodQErT6aQ=='),(17,'Drew','Atkison','as@gmail.com','asdfsd',NULL,0,NULL,NULL,'aasdf'),(18,'Drew','Atkison','izzybeth@gmail.com','P521NFhVyv89P6mPuscw',NULL,0,NULL,NULL,'wi/E0m8DEkdIC/WobEn2dAdxfMtp+Ca/xA6W9IM0Sx+VBpCdWfY6k4ImSYQip3ZDP77m36FLYlryW5YP/blnGw=='),(19,'Drew','Atkison','izzybeth1@gmail.com','iJ5SB6RMRQ6X1e9a0dFK',NULL,0,NULL,NULL,'1AZZ77mMMOJvzUg1mD4CnrMHgdJX8V/LRGp8p3Xs6g43vpB3/XIIwNA5hO8FFcf4QDq9Z/1n7cOxlAI7m3M3WA=='),(20,'Drew','Atkison','izzybeth2@gmail.com','8yEw2lkp9K+0a16TEXl2',NULL,0,NULL,NULL,'CpOTUA0rWf+JWv/06HgM1rlzx8iGXQhpt5QtxOh9MMCL2ZfZ7+pdJcOMacmm/QfgcIuey3SsN6zdKe5w0mqmOw==');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_friends`
--

DROP TABLE IF EXISTS `user_friends`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_friends` (
  `iduser_friends` int NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `friend_id` int NOT NULL,
  `created_at` datetime NOT NULL,
  PRIMARY KEY (`iduser_friends`),
  UNIQUE KEY `iduser_friends_UNIQUE` (`iduser_friends`),
  KEY `friend_user_id_idx` (`user_id`),
  KEY `user_friend_id_idx` (`friend_id`),
  CONSTRAINT `friend_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`iduser`),
  CONSTRAINT `user_friend_id` FOREIGN KEY (`friend_id`) REFERENCES `user` (`iduser`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_friends`
--

LOCK TABLES `user_friends` WRITE;
/*!40000 ALTER TABLE `user_friends` DISABLE KEYS */;
INSERT INTO `user_friends` VALUES (1,20,7,'2021-10-08 22:59:28');
/*!40000 ALTER TABLE `user_friends` ENABLE KEYS */;
UNLOCK TABLES;

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
    HabitId INT,
    UserId INT,
    `Value` CHAR(1)
)
BEGIN
    INSERT INTO `ceddb`.`habit_log`
		(`log_value`,
		`user_id`,
		`habit_id`)
	VALUES
		(`Value`,
		UserId,
		HabitId);
        
	SELECT 
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
    FROM `ceddb`.`habit_log` hl WHERE hl.`idhabit_log`=(SELECT last_insert_id());
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
    Status char(1),
    UserId INT,
    ScheduleId INT,
    HabitTypeId INT,
    CreatedAt DateTime,
    ActiveInd char(1)
)
BEGIN
	INSERT INTO `ceddb`.`habit` (`name`, `icon`, `reminder`, `reminderAt`, `visibleToFriends`, `description`, `status`, `userId`, `scheduleId`, `habitTypeId`, `createdAt`, `active_ind`)
	VALUES(Name, Icon, Reminder, ReminderAt, VisibleToFriends, Description, Status, UserId, ScheduleId, HabitTypeId, CreatedAt, ActiveInd);
    
    SELECT * FROM `ceddb`.`habit` d WHERE d.`idhabit`=(SELECT last_insert_id());
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
    UserId INT
)
BEGIN
	INSERT INTO `ceddb`.`device`
	(`model`,`platform`,`uuid`,`manufacturer`, `user_id`, `active`)
		VALUES(DeviceModel, DevicePlatform, DeviceGUID, Manufacturer, UserId, true);
        
	SELECT * FROM `ceddb`.`device` d WHERE d.`iddevice`=(SELECT last_insert_id());
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
    HabitId INT
)
BEGIN
    SELECT hl.`idhabit_log` as `id`,
		hl.`log_value` as `value`,
		hl.`user_id` as `userId`,
		hl.`habit_id` as `habitId`,
		hl.`created_at` as `createdAt`
	FROM `ceddb`.`habit_log` hl
    WHERE hl.`habit_id` = HabitId;

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
    UserId INT
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
    UserId INT
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
    HabitId INT
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
    HabitId INT
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
    HabitId INT
)
BEGIN
    SELECT 
		fh.idfriend_habit as "id",
        u.firstname,
        u.lastname,
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
    HabitId INT
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
    HabitId INT,
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
	IN UserId VARCHAR(256)
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
INSERT INTO `ceddb`.`user`
(`firstname`,`lastname`,`email`,`passwordSalt`, `locked`, `password`)
	VALUES(Firstname, Lastname, Email, Salt, false, UserHash);
    
call CreateUserDevice(
	DeviceGUID,
    DeviceModel,
    DevicePlatform,
    Manufacturer,
    (SELECT `ceddb`.`user`.`iduser` FROM `ceddb`.`user` WHERE `ceddb`.`user`.`iduser`=(SELECT last_insert_id())));
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
	IN UserId INT, Token VARCHAR(255), IsExpired Boolean, Expires DATETIME, Created DATETIME, Revoked DATETIME, IsRevoked Boolean, DeviceId INT
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
    Status char(1),
    UserId INT,
    ScheduleId INT,
    HabitTypeId INT,
    CreatedAt DateTime,
    ActiveInd char(1),
    HabitId INT
)
BEGIN
    UPDATE `ceddb`.`habit` h SET
		`name`= Name,
        icon = Icon,
        reminder = Reminder,
        reminderAt = ReminderAt,
        visibleToFriends = VisibleToFriends,
        description = Description,
        status = Status,
        userid = UserId,
        scheduleId = ScheduleId,
        habitTypeId = HabitTypeId,
        createdAt = CreatedAt,
        active_ind = ActiveInd
	WHERE h.idhabit = HabitId;
    
    SELECT * FROM `ceddb`.`habit` h WHERE h.`idhabit`=HabitId;
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
    HabitId INT,
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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-10-18 19:15:33
