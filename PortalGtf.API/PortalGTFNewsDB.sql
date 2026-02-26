
CREATE TABLE `Cidade` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `EstadoId` int NOT NULL,
  `RegiaoId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Cidade_Estado` (`EstadoId`),
  KEY `FK_Cidade_Regiao` (`RegiaoId`),
  CONSTRAINT `FK_Cidade_Estado` FOREIGN KEY (`EstadoId`) REFERENCES `Estado` (`Id`),
  CONSTRAINT `FK_Cidade_Regiao` FOREIGN KEY (`RegiaoId`) REFERENCES `Regiao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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

CREATE TABLE `Editorial` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TipoPostagem` varchar(255) NOT NULL,
  `TemaEditorialId` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `EmissoraRegiao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `EmissoraId` int NOT NULL,
  `RegiaoId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_ER_Emissora` (`EmissoraId`),
  KEY `FK_ER_Regiao` (`RegiaoId`),
  CONSTRAINT `FK_ER_Emissora` FOREIGN KEY (`EmissoraId`) REFERENCES `Emissora` (`Id`),
  CONSTRAINT `FK_ER_Regiao` FOREIGN KEY (`RegiaoId`) REFERENCES `Regiao` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Estado` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `Sigla` char(2) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Funcao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TipoFuncao` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `FuncaoPermissao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FuncaoId` int NOT NULL,
  `PermissaoId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_FP_Funcao` (`FuncaoId`),
  KEY `FK_FP_Permissao` (`PermissaoId`),
  CONSTRAINT `FK_FP_Funcao` FOREIGN KEY (`FuncaoId`) REFERENCES `Funcao` (`Id`),
  CONSTRAINT `FK_FP_Permissao` FOREIGN KEY (`PermissaoId`) REFERENCES `Permissao` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Newsletter` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(255) NOT NULL,
  `DataCadastro` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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

CREATE TABLE `Permissao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TipoPermissao` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `TipoPermissao` (`TipoPermissao`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Post` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Titulo` varchar(255) NOT NULL,
  `Subtitulo` varchar(255) NOT NULL,
  `Conteudo` longtext NOT NULL,
  `Imagem` varchar(255) DEFAULT NULL,
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
  PRIMARY KEY (`Id`),
  KEY `FK_Post_Emissora` (`EmissoraId`),
  KEY `FK_Post_Cidade` (`CidadeId`),
  KEY `FK_Post_UserCriacao` (`UsuarioCriacaoId`),
  KEY `FK_Post_UserAprovacao` (`UsuarioAprovacaoId`),
  KEY `IDX_Post_Status` (`StatusPost`),
  KEY `IDX_Post_PublicadoEm` (`PublicadoEm`),
  KEY `IDX_Post_Editorial` (`EditorialId`),
  CONSTRAINT `FK_Post_Cidade` FOREIGN KEY (`CidadeId`) REFERENCES `Cidade` (`Id`),
  CONSTRAINT `FK_Post_Editorial` FOREIGN KEY (`EditorialId`) REFERENCES `Editorial` (`Id`),
  CONSTRAINT `FK_Post_Emissora` FOREIGN KEY (`EmissoraId`) REFERENCES `Emissora` (`Id`),
  CONSTRAINT `FK_Post_UserAprovacao` FOREIGN KEY (`UsuarioAprovacaoId`) REFERENCES `Usuario` (`Id`),
  CONSTRAINT `FK_Post_UserCriacao` FOREIGN KEY (`UsuarioCriacaoId`) REFERENCES `Usuario` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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

CREATE TABLE `PostHistorico` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PostId` int NOT NULL,
  `UsuarioId` int NOT NULL,
  `Acao` varchar(50) NOT NULL,
  `DataAcao` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `FK_PH_Post` (`PostId`),
  KEY `FK_PH_Usuario` (`UsuarioId`),
  CONSTRAINT `FK_PH_Post` FOREIGN KEY (`PostId`) REFERENCES `Post` (`Id`),
  CONSTRAINT `FK_PH_Usuario` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `PostImagem` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PostId` int NOT NULL,
  `UrlPost` varchar(500) NOT NULL,
  `Ordem` int NOT NULL DEFAULT '0',
  `Tipo` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_PostImagem_Post` (`PostId`),
  CONSTRAINT `FK_PostImagem_Post` FOREIGN KEY (`PostId`) REFERENCES `Post` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `PostTag` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PostId` int NOT NULL,
  `TagId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_PostTag_Post` (`PostId`),
  KEY `FK_PostTag_Tag` (`TagId`),
  CONSTRAINT `FK_PostTag_Post` FOREIGN KEY (`PostId`) REFERENCES `Post` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_PostTag_Tag` FOREIGN KEY (`TagId`) REFERENCES `Tag` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `PostVisualizacao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PostId` int NOT NULL,
  `Ip` varchar(45) DEFAULT NULL,
  `DataVisualizacao` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `FK_PV_Post` (`PostId`),
  CONSTRAINT `FK_PV_Post` FOREIGN KEY (`PostId`) REFERENCES `Post` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Regiao` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(150) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Tag` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `Slug` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Slug` (`Slug`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `TemaEditorial` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(255) NOT NULL,
  `CorPrimaria` varchar(255) NOT NULL,
  `CorSecundaria` varchar(255) NOT NULL,
  `CorFonte` varchar(255) NOT NULL,
  `Logo` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
