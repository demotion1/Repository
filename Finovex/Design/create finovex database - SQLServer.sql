
-- -----------------------------------------------------
-- Schema finovex
-- -----------------------------------------------------
CREATE DATABASE [Finovex];

USE FINOVEX
-- -----------------------------------------------------
-- Table `finovex`.`user`
-- -----------------------------------------------------
CREATE TABLE `finovex`.`user` (
  `userid` INT NOT NULL,
  `firstname` VARCHAR(30) NOT NULL,
  `lastname` VARCHAR(30) NOT NULL,
  `email` NVARCHAR(200) NOT NULL,
  `password` NVARCHAR(25) NOT NULL,
  `create_time` DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
  `active` TINYINT(1) NOT NULL DEFAULT 1,
  UNIQUE INDEX `email_UNIQUE` (`email` ASC),
  PRIMARY KEY (`userid`))



-- -----------------------------------------------------
-- Table FileList`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `finovex`.`FileList` (
  `fileid` INT NOT NULL,
  `filename` VARCHAR(45) NOT NULL,
  `creationdDate` DATETIME NOT NULL,
  `modificationDate` DATETIME NOT NULL,
  `filesize` NVARCHAR(50) NOT NULL,
  `mimeType` VARCHAR(45) NOT NULL,
  `fileUser` INT NOT NULL,
  PRIMARY KEY (`fileid`),
  INDEX `fk_FileList_user1_idx` (`fileUser` ASC),
  CONSTRAINT `fk_FileList_user1`
    FOREIGN KEY (`fileUser`)
    REFERENCES `finovex`.`user` (`userid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table DeleteHistory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `finovex`.`DeleteHistory` (
  `filename` VARCHAR(45) NOT NULL,
  `deleteTimestamp` DATETIME NOT NULL,
  `userid` INT NOT NULL,
  PRIMARY KEY (`userid`, `deleteTimestamp`),
  INDEX `fk_DeleteHistory_user_idx` (`userid` ASC),
  CONSTRAINT `fk_DeleteHistory_user`
    FOREIGN KEY (`userid`)
    REFERENCES `finovex`.`user` (`userid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table UpdateHistory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `finovex`.`UpdateHistory` (
  `filename` VARCHAR(45) NOT NULL,
  `prevfilename` VARCHAR(45) NOT NULL,
  `updateTimestamp` DATETIME NOT NULL,
  `userid` INT NOT NULL,
  PRIMARY KEY (`updateTimestamp`, `userid`),
  INDEX `fk_UpdateHistory_user_idx` (`userid` ASC),
  CONSTRAINT `fk_UpdateHistory_user`
    FOREIGN KEY (`userid`)
    REFERENCES `finovex`.`user` (`userid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table DownloadHistory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `finovex`.`DownloadHistory` (
  `filename` VARCHAR(45) NOT NULL,
  `downloadTimestamp` DATETIME NOT NULL,
  `userid` INT NOT NULL,
  PRIMARY KEY (`userid`, `downloadTimestamp`),
  INDEX `fk_DownloadHistory_user_idx` (`userid` ASC),
  CONSTRAINT `fk_DownloadHistory_user`
    FOREIGN KEY (`userid`)
    REFERENCES `finovex`.`user` (`userid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `finovex` ;
COMMIT;


-- -----------------------------------------------------
-- Table [UploadHistory]
-- -----------------------------------------------------
CREATE TABLE [dbo].[UploadHistory](
	[userid] [int] NOT NULL,
	[uploadTimestamp] [datetime] NOT NULL,
	[filename] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UploadHistory] PRIMARY KEY CLUSTERED 
(
	[userid] ASC,
	[uploadTimestamp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[UploadHistory]  WITH CHECK ADD  CONSTRAINT [FK_UploadHistory_user] FOREIGN KEY([userid])
REFERENCES [dbo].[user] ([userid])
GO

ALTER TABLE [dbo].[UploadHistory] CHECK CONSTRAINT [FK_UploadHistory_user]
GO

