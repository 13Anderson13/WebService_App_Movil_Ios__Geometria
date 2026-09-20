-- ===== Indicaciones generales =====
CREATE DATABASE geometria;
USE geometria;

-- ===== Tablas del proyecto =====
CREATE TABLE Usuarios(
	id_usuarios INT PRIMARY KEY AUTO_INCREMENT,
    login VARCHAR(50) NOT NULL,
    password VARCHAR(250) NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50),
    email VARCHAR(100),
    telefono VARCHAR(20),
    fecha_creacion DATE,
    estado BOOLEAN DEFAULT TRUE
);

CREATE TABLE Categoria_Minijuego(
	id_minijuego INT PRIMARY KEY AUTO_INCREMENT,
    nombre_minijuego VARCHAR(50) NOT NULL,
    icono VARCHAR(50),
    descripcion TEXT,
    estado BOOLEAN DEFAULT TRUE
);

INSERT INTO Categoria_Minijuego (nombre_minijuego, icono, descripcion)VALUES("Relacionar Figuras","circle", "Aprende y relaciona figuras");

CREATE TABLE Logros
(
	id_logro INT PRIMARY KEY AUTO_INCREMENT,
    nombre_logro VARCHAR(50),
    descripcion_logro TEXT,
    icono VARCHAR(50)
);

INSERT INTO Logros (nombre_logro, descripcion_logro, icono) VALUES ("INICIO SESION","Regristrate","circle");
-- ===== Procedimientos almacenados =====
DELIMITER //
CREATE DEFINER=`Bruce`@`%` PROCEDURE `SP_Insertar_Usuario`
(
	IN sp_login varchar(50),
    IN sp_password varchar(250),
    IN sp_nombre varchar(50),
    IN sp_apellido varchar(50),
    IN sp_email varchar(100),
    IN sp_telefono varchar(20)
)
BEGIN
	INSERT INTO geometria.Usuarios
    (
		login,
		password,
        nombre,
        apellido,
        email,
        telefono,
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
        sp_telefono,
        NOW(),
        1
    );
END //

DELIMITER ;

DELIMITER //
CREATE DEFINER=`Bruce`@`%` PROCEDURE `SP_Validar_Usuario`
(
	IN sp_login VARCHAR(50)
)BEGIN
    SELECT 
		id_usuarios,
        login,
        password,
        nombre,
        apellido
    FROM geometria.Usuarios
    WHERE login = sp_login
    LIMIT 1;
END //

DELIMITER ;

-- ===== VISTAS DISPONIBLES =====

CREATE VIEW V_Consultar_Minijuego
AS
	SELECT id_minijuego, nombre_minijuego, icono, descripcion, estado from Categoria_Minijuego
END;

-- SELECT * FROM v_consultar_minijuego;