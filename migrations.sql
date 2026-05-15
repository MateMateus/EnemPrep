CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;
DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `Conquistas` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `Titulo` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        `Descricao` varchar(500) CHARACTER SET utf8mb4 NOT NULL,
        `Icone` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `PontosZ` int NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_Conquistas` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `Materias` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `Nome` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
        `Descricao` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_Materias` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `PerfisUsuario` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `Tipo` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `NomeApresentacao` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_PerfisUsuario` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `Simulados` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `Titulo` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
        `AnoReferencia` int NULL,
        `DuracaoMaxima` time(6) NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_Simulados` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `Assuntos` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `Nome` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
        `Descricao` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
        `MateriaId` char(36) COLLATE ascii_general_ci NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_Assuntos` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Assuntos_Materias_MateriaId` FOREIGN KEY (`MateriaId`) REFERENCES `Materias` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `Livros` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `Titulo` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
        `Descricao` varchar(2000) CHARACTER SET utf8mb4 NULL,
        `UrlCapa` varchar(500) CHARACTER SET utf8mb4 NULL,
        `MateriaId` char(36) COLLATE ascii_general_ci NOT NULL,
        `TipoConteudo` int NOT NULL,
        `DataCriacao` datetime(6) NOT NULL,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_Livros` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Livros_Materias_MateriaId` FOREIGN KEY (`MateriaId`) REFERENCES `Materias` (`Id`) ON DELETE RESTRICT
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `Usuarios` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `Nome` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
        `Email` varchar(250) CHARACTER SET utf8mb4 NOT NULL,
        `SenhaHash` varchar(500) CHARACTER SET utf8mb4 NOT NULL,
        `PerfilUsuarioId` char(36) COLLATE ascii_general_ci NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_Usuarios` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Usuarios_PerfisUsuario_PerfilUsuarioId` FOREIGN KEY (`PerfilUsuarioId`) REFERENCES `PerfisUsuario` (`Id`) ON DELETE RESTRICT
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `VideoAulas` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `Titulo` varchar(300) CHARACTER SET utf8mb4 NOT NULL,
        `UrlVideo` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
        `DuracaoSegundos` int NOT NULL,
        `AssuntoId` char(36) COLLATE ascii_general_ci NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_VideoAulas` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_VideoAulas_Assuntos_AssuntoId` FOREIGN KEY (`AssuntoId`) REFERENCES `Assuntos` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `LivrosPaginas` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `LivroId` char(36) COLLATE ascii_general_ci NOT NULL,
        `NumeroProprio` int NOT NULL,
        `UrlImagem` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
        `DataCriacao` datetime(6) NOT NULL,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_LivrosPaginas` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_LivrosPaginas_Livros_LivroId` FOREIGN KEY (`LivroId`) REFERENCES `Livros` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `LivrosTemas` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `LivroId` char(36) COLLATE ascii_general_ci NOT NULL,
        `Nome` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
        `PaginaInicial` int NOT NULL,
        `PaginaFinal` int NOT NULL,
        `DataCriacao` datetime(6) NOT NULL,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_LivrosTemas` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_LivrosTemas_Livros_LivroId` FOREIGN KEY (`LivroId`) REFERENCES `Livros` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `PlanosEstudo` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `Titulo` varchar(300) CHARACTER SET utf8mb4 NOT NULL,
        `DataInicio` datetime(6) NOT NULL,
        `DataFim` datetime(6) NOT NULL,
        `UsuarioId` char(36) COLLATE ascii_general_ci NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_PlanosEstudo` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_PlanosEstudo_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `StreaksUsuario` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `UsuarioId` char(36) COLLATE ascii_general_ci NOT NULL,
        `DiasConsecutivos` int NOT NULL,
        `MaiorStreak` int NOT NULL,
        `UltimaAtividade` datetime(6) NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_StreaksUsuario` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_StreaksUsuario_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `TentativasSimulado` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `UsuarioId` char(36) COLLATE ascii_general_ci NOT NULL,
        `SimuladoId` char(36) COLLATE ascii_general_ci NOT NULL,
        `DataInicio` datetime(6) NOT NULL,
        `DataFim` datetime(6) NULL,
        `NotaTotalBruta` int NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_TentativasSimulado` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_TentativasSimulado_Simulados_SimuladoId` FOREIGN KEY (`SimuladoId`) REFERENCES `Simulados` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_TentativasSimulado_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `UsuarioConquistas` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `UsuarioId` char(36) COLLATE ascii_general_ci NOT NULL,
        `ConquistaId` char(36) COLLATE ascii_general_ci NOT NULL,
        `DataObtencao` datetime(6) NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_UsuarioConquistas` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_UsuarioConquistas_Conquistas_ConquistaId` FOREIGN KEY (`ConquistaId`) REFERENCES `Conquistas` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_UsuarioConquistas_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `Questoes` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `Enunciado` varchar(5000) CHARACTER SET utf8mb4 NOT NULL,
        `Dificuldade` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `Explicacao` varchar(5000) CHARACTER SET utf8mb4 NULL,
        `VideoExplicacaoUrl` varchar(500) CHARACTER SET utf8mb4 NULL,
        `AssuntoId` char(36) COLLATE ascii_general_ci NOT NULL,
        `LivroId` char(36) COLLATE ascii_general_ci NULL,
        `LivroTemaId` char(36) COLLATE ascii_general_ci NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_Questoes` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Questoes_Assuntos_AssuntoId` FOREIGN KEY (`AssuntoId`) REFERENCES `Assuntos` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_Questoes_LivrosTemas_LivroTemaId` FOREIGN KEY (`LivroTemaId`) REFERENCES `LivrosTemas` (`Id`),
        CONSTRAINT `FK_Questoes_Livros_LivroId` FOREIGN KEY (`LivroId`) REFERENCES `Livros` (`Id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `PlanosEstudoItens` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `PlanoEstudoId` char(36) COLLATE ascii_general_ci NOT NULL,
        `AssuntoId` char(36) COLLATE ascii_general_ci NOT NULL,
        `DataPrevista` datetime(6) NOT NULL,
        `Status` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_PlanosEstudoItens` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_PlanosEstudoItens_Assuntos_AssuntoId` FOREIGN KEY (`AssuntoId`) REFERENCES `Assuntos` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_PlanosEstudoItens_PlanosEstudo_PlanoEstudoId` FOREIGN KEY (`PlanoEstudoId`) REFERENCES `PlanosEstudo` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `Alternativas` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `Texto` varchar(2000) CHARACTER SET utf8mb4 NOT NULL,
        `Correta` tinyint(1) NOT NULL,
        `QuestaoId` char(36) COLLATE ascii_general_ci NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_Alternativas` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Alternativas_Questoes_QuestaoId` FOREIGN KEY (`QuestaoId`) REFERENCES `Questoes` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `DesafiosDiarios` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `Titulo` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
        `DataDesafio` datetime(6) NOT NULL,
        `QuestaoId` char(36) COLLATE ascii_general_ci NOT NULL,
        `XPRecompensa` int NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_DesafiosDiarios` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_DesafiosDiarios_Questoes_QuestaoId` FOREIGN KEY (`QuestaoId`) REFERENCES `Questoes` (`Id`) ON DELETE RESTRICT
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `SimuladosQuestoes` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `SimuladoId` char(36) COLLATE ascii_general_ci NOT NULL,
        `QuestaoId` char(36) COLLATE ascii_general_ci NOT NULL,
        `Ordem` int NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_SimuladosQuestoes` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_SimuladosQuestoes_Questoes_QuestaoId` FOREIGN KEY (`QuestaoId`) REFERENCES `Questoes` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_SimuladosQuestoes_Simulados_SimuladoId` FOREIGN KEY (`SimuladoId`) REFERENCES `Simulados` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `RespostasSimulado` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `TentativaSimuladoId` char(36) COLLATE ascii_general_ci NOT NULL,
        `QuestaoId` char(36) COLLATE ascii_general_ci NOT NULL,
        `AlternativaId` char(36) COLLATE ascii_general_ci NULL,
        `Correta` tinyint(1) NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_RespostasSimulado` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_RespostasSimulado_Alternativas_AlternativaId` FOREIGN KEY (`AlternativaId`) REFERENCES `Alternativas` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_RespostasSimulado_Questoes_QuestaoId` FOREIGN KEY (`QuestaoId`) REFERENCES `Questoes` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_RespostasSimulado_TentativasSimulado_TentativaSimuladoId` FOREIGN KEY (`TentativaSimuladoId`) REFERENCES `TentativasSimulado` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE TABLE `TentativasQuestao` (
        `Id` char(36) COLLATE ascii_general_ci NOT NULL,
        `UsuarioId` char(36) COLLATE ascii_general_ci NOT NULL,
        `QuestaoId` char(36) COLLATE ascii_general_ci NOT NULL,
        `AlternativaSelecionadaId` char(36) COLLATE ascii_general_ci NULL,
        `Acertou` tinyint(1) NOT NULL,
        `TempoGastoSegundos` int NOT NULL,
        `DataCriacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
        `DataAtualizacao` datetime(6) NULL,
        CONSTRAINT `PK_TentativasQuestao` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_TentativasQuestao_Alternativas_AlternativaSelecionadaId` FOREIGN KEY (`AlternativaSelecionadaId`) REFERENCES `Alternativas` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_TentativasQuestao_Questoes_QuestaoId` FOREIGN KEY (`QuestaoId`) REFERENCES `Questoes` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `FK_TentativasQuestao_Usuarios_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuarios` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_Alternativas_QuestaoId` ON `Alternativas` (`QuestaoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_Assuntos_MateriaId` ON `Assuntos` (`MateriaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_DesafiosDiarios_QuestaoId` ON `DesafiosDiarios` (`QuestaoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_Livros_MateriaId` ON `Livros` (`MateriaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_Livros_TipoConteudo` ON `Livros` (`TipoConteudo`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE UNIQUE INDEX `IX_LivrosPaginas_LivroId_NumeroProprio` ON `LivrosPaginas` (`LivroId`, `NumeroProprio`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_LivrosTemas_LivroId` ON `LivrosTemas` (`LivroId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_PlanosEstudo_UsuarioId` ON `PlanosEstudo` (`UsuarioId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_PlanosEstudoItens_AssuntoId` ON `PlanosEstudoItens` (`AssuntoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_PlanosEstudoItens_PlanoEstudoId` ON `PlanosEstudoItens` (`PlanoEstudoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_Questoes_AssuntoId` ON `Questoes` (`AssuntoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_Questoes_LivroId` ON `Questoes` (`LivroId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_Questoes_LivroTemaId` ON `Questoes` (`LivroTemaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_RespostasSimulado_AlternativaId` ON `RespostasSimulado` (`AlternativaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_RespostasSimulado_QuestaoId` ON `RespostasSimulado` (`QuestaoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_RespostasSimulado_TentativaSimuladoId` ON `RespostasSimulado` (`TentativaSimuladoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_SimuladosQuestoes_QuestaoId` ON `SimuladosQuestoes` (`QuestaoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_SimuladosQuestoes_SimuladoId` ON `SimuladosQuestoes` (`SimuladoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE UNIQUE INDEX `IX_StreaksUsuario_UsuarioId` ON `StreaksUsuario` (`UsuarioId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_TentativasQuestao_AlternativaSelecionadaId` ON `TentativasQuestao` (`AlternativaSelecionadaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_TentativasQuestao_QuestaoId` ON `TentativasQuestao` (`QuestaoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_TentativasQuestao_UsuarioId` ON `TentativasQuestao` (`UsuarioId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_TentativasQuestao_UsuarioId_QuestaoId` ON `TentativasQuestao` (`UsuarioId`, `QuestaoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_TentativasSimulado_SimuladoId` ON `TentativasSimulado` (`SimuladoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_TentativasSimulado_UsuarioId` ON `TentativasSimulado` (`UsuarioId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_UsuarioConquistas_ConquistaId` ON `UsuarioConquistas` (`ConquistaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE UNIQUE INDEX `IX_UsuarioConquistas_UsuarioId_ConquistaId` ON `UsuarioConquistas` (`UsuarioId`, `ConquistaId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE UNIQUE INDEX `IX_Usuarios_Email` ON `Usuarios` (`Email`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_Usuarios_PerfilUsuarioId` ON `Usuarios` (`PerfilUsuarioId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    CREATE INDEX `IX_VideoAulas_AssuntoId` ON `VideoAulas` (`AssuntoId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260505192328_InitialMysql') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20260505192328_InitialMysql', '9.0.2');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

