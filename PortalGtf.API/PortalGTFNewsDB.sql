-- MySQL dump 10.13  Distrib 9.5.0, for macos15.4 (arm64)
--
-- Host: 127.0.0.1    Database: portal_noticias
-- ------------------------------------------------------
-- Server version	8.0.44

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Cidade`
--

DROP TABLE IF EXISTS `Cidade`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Cidade` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `EstadoId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Cidade_Estado` (`EstadoId`),
  CONSTRAINT `FK_Cidade_Estado` FOREIGN KEY (`EstadoId`) REFERENCES `Estado` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Comentario`
--

DROP TABLE IF EXISTS `Comentario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Comentario` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PostId` int NOT NULL,
  `UsuarioId` int DEFAULT NULL,
  `NomeVisitante` varchar(150) DEFAULT NULL,
  `Conteudo` varchar(1000) NOT NULL,
  `Status` varchar(20) NOT NULL DEFAULT 'Pendente',
  `DataCriacao` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `FK_Comentario_Post` (`PostId`),
  KEY `FK_Comentario_Usuario` (`UsuarioId`),
  KEY `IDX_Comentario_Status` (`Status`),
  CONSTRAINT `FK_Comentario_Post` FOREIGN KEY (`PostId`) REFERENCES `Post` (`Id`),
  CONSTRAINT `FK_Comentario_Usuario` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Editorial`
--

DROP TABLE IF EXISTS `Editorial`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Editorial` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TipoPostagem` varchar(255) NOT NULL,
  `TemaEditorialId` int NOT NULL,
  `EmissoraId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Editorial_EmissoraId` (`EmissoraId`),
  CONSTRAINT `FK_Editorial_Emissora` FOREIGN KEY (`EmissoraId`) REFERENCES `Emissora` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Emissora`
--

DROP TABLE IF EXISTS `Emissora`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Emissora` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `NomeSocial` varchar(255) NOT NULL,
  `RazaoSocial` varchar(255) NOT NULL,
  `Cep` varchar(20) NOT NULL,
  `Endereco` varchar(255) NOT NULL,
  `Numero` varchar(100) NOT NULL,
  `Bairro` varchar(255) NOT NULL,
  `Estado` varchar(255) NOT NULL,
  `Cidade` varchar(255) NOT NULL,
  `Slug` varchar(100) NOT NULL,
  `Logo` varchar(255) NOT NULL,
  `LogoSmall` varchar(255) NOT NULL,
  `TemaPrincipal` varchar(255) NOT NULL,
  `Ativa` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Emissora_Ativa` (`Ativa`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `EmissoraRegiao`
--

DROP TABLE IF EXISTS `EmissoraRegiao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `EmissoraRegiao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `EmissoraId` int NOT NULL,
  `RegiaoId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_EmissoraRegiao_RegiaoId` (`RegiaoId`),
  KEY `IX_EmissoraRegiao_EmissoraId` (`EmissoraId`),
  CONSTRAINT `FK_ER_Emissora` FOREIGN KEY (`EmissoraId`) REFERENCES `Emissora` (`Id`),
  CONSTRAINT `FK_ER_Regiao` FOREIGN KEY (`RegiaoId`) REFERENCES `Regiao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Estado`
--

DROP TABLE IF EXISTS `Estado`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Estado` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `Sigla` char(2) NOT NULL,
  `RegiaoId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Estado_Regiao` (`RegiaoId`),
  CONSTRAINT `FK_Estado_Regiao` FOREIGN KEY (`RegiaoId`) REFERENCES `Regiao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Funcao`
--

DROP TABLE IF EXISTS `Funcao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Funcao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TipoFuncao` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `FuncaoPermissao`
--

DROP TABLE IF EXISTS `FuncaoPermissao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `FuncaoPermissao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FuncaoId` int NOT NULL,
  `PermissaoId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_FP_Funcao` (`FuncaoId`),
  KEY `FK_FP_Permissao` (`PermissaoId`),
  CONSTRAINT `FK_FP_Funcao` FOREIGN KEY (`FuncaoId`) REFERENCES `Funcao` (`Id`),
  CONSTRAINT `FK_FP_Permissao` FOREIGN KEY (`PermissaoId`) REFERENCES `Permissao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Midia`
--

DROP TABLE IF EXISTS `Midia`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Midia` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `NomeOriginal` varchar(255) NOT NULL,
  `NomeArquivo` varchar(255) NOT NULL,
  `Url` varchar(500) NOT NULL,
  `Tipo` int NOT NULL,
  `DataUpload` datetime NOT NULL,
  `UsuarioUploadId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Midia_Tipo` (`Tipo`),
  KEY `IX_Midia_DataUpload` (`DataUpload`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `BannerInstitucional`
--

DROP TABLE IF EXISTS `BannerInstitucional`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `BannerInstitucional` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Titulo` varchar(255) NOT NULL,
  `EmissoraId` int NOT NULL,
  `MidiaId` int NOT NULL,
  `LinkUrl` varchar(500) NOT NULL,
  `NovaAba` tinyint(1) NOT NULL DEFAULT '1',
  `Posicao` varchar(100) NOT NULL DEFAULT 'home',
  `Ordem` int NOT NULL DEFAULT '1',
  `Ativo` tinyint(1) NOT NULL DEFAULT '1',
  `DataCriacao` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `IX_BannerInstitucional_EmissoraId` (`EmissoraId`),
  KEY `IX_BannerInstitucional_MidiaId` (`MidiaId`),
  KEY `IX_BannerInstitucional_Ativo` (`Ativo`),
  KEY `IX_BannerInstitucional_Posicao` (`Posicao`),
  CONSTRAINT `FK_BannerInstitucional_Emissora` FOREIGN KEY (`EmissoraId`) REFERENCES `Emissora` (`Id`),
  CONSTRAINT `FK_BannerInstitucional_Midia` FOREIGN KEY (`MidiaId`) REFERENCES `Midia` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Newsletter`
--

DROP TABLE IF EXISTS `Newsletter`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Newsletter` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(255) NOT NULL,
  `DataCadastro` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Notificacao`
--

DROP TABLE IF EXISTS `Notificacao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Notificacao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UsuarioId` int NOT NULL,
  `Titulo` varchar(200) NOT NULL,
  `Mensagem` varchar(500) NOT NULL,
  `Lida` tinyint(1) NOT NULL DEFAULT '0',
  `DataCriacao` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `FK_Notificacao_Usuario` (`UsuarioId`),
  CONSTRAINT `FK_Notificacao_Usuario` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Permissao`
--

DROP TABLE IF EXISTS `Permissao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Permissao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TipoPermissao` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `TipoPermissao` (`TipoPermissao`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Post`
--

DROP TABLE IF EXISTS `Post`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Post` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Titulo` varchar(255) NOT NULL,
  `Subtitulo` varchar(255) NOT NULL,
  `Conteudo` longtext NOT NULL,
  `Slug` varchar(255) NOT NULL,
  `EditorialId` int NOT NULL,
  `EmissoraId` int NOT NULL,
  `CidadeId` int NOT NULL,
  `UsuarioCriacaoId` int NOT NULL,
  `UsuarioAprovacaoId` int DEFAULT NULL,
  `StatusPost` int NOT NULL,
  `DataCriacao` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DataAprovacao` datetime DEFAULT NULL,
  `DataEdicao` datetime DEFAULT NULL,
  `PublicadoEm` datetime DEFAULT NULL,
  `SubcategoriaId` int DEFAULT NULL,
  `Destaque` tinyint(1) DEFAULT '0',
  `ImagemCapaId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Post_Cidade` (`CidadeId`),
  KEY `FK_Post_UserCriacao` (`UsuarioCriacaoId`),
  KEY `FK_Post_UserAprovacao` (`UsuarioAprovacaoId`),
  KEY `IDX_Post_Status` (`StatusPost`),
  KEY `IDX_Post_PublicadoEm` (`PublicadoEm`),
  KEY `IDX_Post_Editorial` (`EditorialId`),
  KEY `IX_Post_SubcategoriaId` (`SubcategoriaId`),
  KEY `IX_Post_EmissoraId` (`EmissoraId`),
  KEY `IX_Post_StatusPost` (`StatusPost`),
  KEY `IX_Post_PublicadoEm` (`PublicadoEm`),
  KEY `IX_Post_ImagemCapaId` (`ImagemCapaId`),
  CONSTRAINT `FK_Post_Cidade` FOREIGN KEY (`CidadeId`) REFERENCES `Cidade` (`Id`),
  CONSTRAINT `FK_Post_Editorial` FOREIGN KEY (`EditorialId`) REFERENCES `Editorial` (`Id`),
  CONSTRAINT `FK_Post_Emissora` FOREIGN KEY (`EmissoraId`) REFERENCES `Emissora` (`Id`),
  CONSTRAINT `FK_Post_ImagemCapa` FOREIGN KEY (`ImagemCapaId`) REFERENCES `Midia` (`Id`) ON DELETE SET NULL,
  CONSTRAINT `FK_Post_Subcategoria` FOREIGN KEY (`SubcategoriaId`) REFERENCES `Subcategoria` (`Id`) ON DELETE SET NULL,
  CONSTRAINT `FK_Post_UserAprovacao` FOREIGN KEY (`UsuarioAprovacaoId`) REFERENCES `Usuario` (`Id`),
  CONSTRAINT `FK_Post_UserCriacao` FOREIGN KEY (`UsuarioCriacaoId`) REFERENCES `Usuario` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `PostComentario`
--

DROP TABLE IF EXISTS `PostComentario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PostComentario` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PostId` int NOT NULL,
  `ComentarioId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_PostComentario_Post` (`PostId`),
  KEY `FK_PostComentario_Comentario` (`ComentarioId`),
  CONSTRAINT `FK_PostComentario_Comentario` FOREIGN KEY (`ComentarioId`) REFERENCES `Comentario` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_PostComentario_Post` FOREIGN KEY (`PostId`) REFERENCES `Post` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `PostHistorico`
--

DROP TABLE IF EXISTS `PostHistorico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PostHistorico` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PostId` int NOT NULL,
  `UsuarioId` int NOT NULL,
  `Acao` varchar(50) NOT NULL,
  `Mensagem` varchar(1000) DEFAULT NULL,
  `DataAcao` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `FK_PH_Post` (`PostId`),
  KEY `FK_PH_Usuario` (`UsuarioId`),
  CONSTRAINT `FK_PH_Post` FOREIGN KEY (`PostId`) REFERENCES `Post` (`Id`),
  CONSTRAINT `FK_PH_Usuario` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `PostImagem`
--

DROP TABLE IF EXISTS `PostImagem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PostImagem` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PostId` int NOT NULL,
  `Ordem` int NOT NULL DEFAULT '0',
  `Tipo` int DEFAULT NULL,
  `MidiaId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_PostImagem_Post` (`PostId`),
  KEY `FK_PostImagem_Midia` (`MidiaId`),
  KEY `IX_PostImagem_Ordem` (`Ordem`),
  CONSTRAINT `FK_PostImagem_Midia` FOREIGN KEY (`MidiaId`) REFERENCES `Midia` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_PostImagem_Post` FOREIGN KEY (`PostId`) REFERENCES `Post` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `PostTag`
--

DROP TABLE IF EXISTS `PostTag`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PostTag` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PostId` int NOT NULL,
  `TagId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_PostTag_Post` (`PostId`),
  KEY `FK_PostTag_Tag` (`TagId`),
  CONSTRAINT `FK_PostTag_Post` FOREIGN KEY (`PostId`) REFERENCES `Post` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_PostTag_Tag` FOREIGN KEY (`TagId`) REFERENCES `Tag` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `PostVisualizacao`
--

DROP TABLE IF EXISTS `PostVisualizacao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PostVisualizacao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PostId` int NOT NULL,
  `Ip` varchar(45) DEFAULT NULL,
  `DataVisualizacao` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `FK_PV_Post` (`PostId`),
  CONSTRAINT `FK_PV_Post` FOREIGN KEY (`PostId`) REFERENCES `Post` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ProgramacaoRadio`
--

DROP TABLE IF EXISTS `ProgramacaoRadio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ProgramacaoRadio` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `EmissoraId` int NOT NULL,
  `NomePrograma` varchar(255) NOT NULL,
  `Apresentador` varchar(255) DEFAULT NULL,
  `Descricao` varchar(500) DEFAULT NULL,
  `DiaSemana` int NOT NULL,
  `HoraInicio` time NOT NULL,
  `HoraFim` time NOT NULL,
  `Imagem` varchar(255) DEFAULT NULL,
  `Ativo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`),
  KEY `IX_ProgramacaoRadio_EmissoraId` (`EmissoraId`),
  KEY `IX_ProgramacaoRadio_Ativo` (`Ativo`),
  CONSTRAINT `FK_ProgramacaoRadio_Emissora` FOREIGN KEY (`EmissoraId`) REFERENCES `Emissora` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Regiao`
--

DROP TABLE IF EXISTS `Regiao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Regiao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(150) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Streaming`
--

DROP TABLE IF EXISTS `Streaming`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Streaming` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `EmissoraId` int NOT NULL,
  `Url` varchar(500) NOT NULL,
  `Porta` varchar(255) NOT NULL,
  `TipoStream` varchar(50) NOT NULL,
  `LinkApi` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Streaming_Emissora` (`EmissoraId`),
  CONSTRAINT `FK_Streaming_Emissora` FOREIGN KEY (`EmissoraId`) REFERENCES `Emissora` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Subcategoria`
--

DROP TABLE IF EXISTS `Subcategoria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Subcategoria` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(150) NOT NULL,
  `Slug` varchar(150) NOT NULL,
  `EditorialId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Subcategoria_EditorialId` (`EditorialId`),
  CONSTRAINT `FK_Subcategoria_Editorial` FOREIGN KEY (`EditorialId`) REFERENCES `Editorial` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Tag`
--

DROP TABLE IF EXISTS `Tag`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Tag` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `Slug` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Slug` (`Slug`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `TemaEditorial`
--

DROP TABLE IF EXISTS `TemaEditorial`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TemaEditorial` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(255) NOT NULL,
  `CorPrimaria` varchar(255) NOT NULL,
  `CorSecundaria` varchar(255) NOT NULL,
  `CorFonte` varchar(255) NOT NULL,
  `Logo` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Usuario`
--

DROP TABLE IF EXISTS `Usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Usuario` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(255) NOT NULL,
  `NomeCompleto` varchar(200) NOT NULL,
  `SenhaHash` varchar(255) NOT NULL,
  `StatusUsuario` int NOT NULL,
  `FuncaoId` int NOT NULL,
  `DataCriacao` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`),
  KEY `FK_Usuario_Funcao` (`FuncaoId`),
  CONSTRAINT `FK_Usuario_Funcao` FOREIGN KEY (`FuncaoId`) REFERENCES `Funcao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `UsuarioEmissora`
--

DROP TABLE IF EXISTS `UsuarioEmissora`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `UsuarioEmissora` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UsuarioId` int NOT NULL,
  `EmissoraId` int NOT NULL,
  `FuncaoId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IDX_UE_Usuario` (`UsuarioId`),
  KEY `IDX_UE_Emissora` (`EmissoraId`),
  KEY `IDX_UE_Funcao` (`FuncaoId`),
  CONSTRAINT `FK_UE_Emissora` FOREIGN KEY (`EmissoraId`) REFERENCES `Emissora` (`Id`),
  CONSTRAINT `FK_UE_Funcao` FOREIGN KEY (`FuncaoId`) REFERENCES `Funcao` (`Id`),
  CONSTRAINT `FK_UE_Usuario` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping routines for database 'portal_noticias'
--
--
-- WARNING: can't read the INFORMATION_SCHEMA.libraries table. It's most probably an old server 8.0.44.
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-04-06  9:07:17
