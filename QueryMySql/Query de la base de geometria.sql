-- ===== Indicaciones generales =====
CREATE DATABASE geometria;
USE geometria;

-- ===== Tablas del proyecto =====
CREATE TABLE Usuarios(
	id_usuarios INT PRIMARY KEY AUTO_INCREMENT,
    login varchar(50) NOT NULL,
    password varchar(250) NOT NULL,
    nombre varchar(50) NOT NULL,
    apellido varchar(50),
    email varchar(100),
    fecha_creacion date,
    estado bit
);

-- ===== Procedimientos almacenados =====
DELIMITER //
CREATE DEFINER=`Bruce`@`%` PROCEDURE `SP_Insertar_Usuario`
(
	IN sp_login varchar(50),
    IN sp_password varchar(50),
    IN sp_nombre varchar(50),
    IN sp_apellido varchar(50),
    IN sp_email varchar(100)
)
BEGIN
	INSERT INTO geometria.Usuarios
    (
		login,
		password,
        nombre,
        apellido,
        email,
        fecha_creacion,
        estado
    )
    VALUES
    (
		sp_login,
        sp_password,
        sp_nombre,
        sp_apellido,
        sp_email,
        NOW(),
        1
    );
END //

DELIMITER ;
